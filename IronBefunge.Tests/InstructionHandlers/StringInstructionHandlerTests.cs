using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Globalization;

namespace IronBefunge.Tests.InstructionHandlers;

public static class StringInstructionHandlerTests
{
	[Test]
	public static void HandleInStringMode()
	{
		//"   Hi  "
		var cells = new List<Cell>()
		{
			new(new Point(0, 0), StringInstructionHandler.StringModeInstruction),
			new(new Point(4, 0), 'H'),
			new(new Point(5, 0), 'i'),
			new(new Point(8, 0), StringInstructionHandler.StringModeInstruction),
		};

		InstructionHandlerRunner.Run(new StringInstructionHandler(), cells, null,
			(context, result) =>
			{
				Assert.Multiple(() =>
				{
					var count = context.Values.Count;

					Assert.That(count, Is.EqualTo(7), nameof(context.Values.Count));

					for (var i = 0; i < count; i++)
					{
						var character = Convert.ToChar(context.Values.Pop());

						if (i >= 0 && i <= 1 || i >= 4 && i <= 6)
						{
							Assert.That(character, Is.EqualTo(' '), i.ToString(CultureInfo.CurrentCulture));
						}
						else if (i == 2)
						{
							Assert.That(character, Is.EqualTo('i'), i.ToString(CultureInfo.CurrentCulture));
						}
						else if (i == 3)
						{
							Assert.That(character, Is.EqualTo('H'), i.ToString(CultureInfo.CurrentCulture));
						}
					}
				});
			});
	}

	[Test]
	public static void HandleInStringModeNoWhitespace()
	{
		//"Hi"
		var cells = new List<Cell>()
		{
			new(new Point(0, 0), StringInstructionHandler.StringModeInstruction),
			new(new Point(1, 0), 'H'),
			new(new Point(2, 0), 'i'),
			new(new Point(3, 0), StringInstructionHandler.StringModeInstruction),
		};

		InstructionHandlerRunner.Run(new StringInstructionHandler(), cells, null,
			(context, result) =>
			{
				Assert.Multiple(() =>
				{
					var count = context.Values.Count;
					Assert.That(count, Is.EqualTo(2), nameof(context.Values.Count));

					for (var i = 0; i < count; i++)
					{
						var character = Convert.ToChar(context.Values.Pop());

						if (i == 0)
						{
							Assert.That(character, Is.EqualTo('i'), i.ToString(CultureInfo.CurrentCulture));
						}
						else if (i == 1)
						{
							Assert.That(character, Is.EqualTo('H'), i.ToString(CultureInfo.CurrentCulture));
						}
					}
				});
			});
	}

	[Test]
	public static void HandleFetchCharacterInstruction()
	{
		//'Q,
		var cells = new List<Cell>()
		{
			new(new Point(0, 0), StringInstructionHandler.FetchCharacterInstruction),
			new(new Point(1, 0), 'Q'),
			new(new Point(2, 0), ','),
		};

		InstructionHandlerRunner.Run(new StringInstructionHandler(), cells, null,
			(context, result) =>
			{
				Assert.Multiple(() =>
				{
					Assert.That(context.Current.Value, Is.EqualTo('Q'), nameof(context.Current.Value));
					var count = context.Values.Count;
					Assert.That(count, Is.EqualTo(1), nameof(context.Values.Count));
					var character = Convert.ToChar(context.Values.Pop());
					Assert.That(character, Is.EqualTo('Q'), nameof(character));
				});
			});
	}

	[Test]
	public static void HandleStoreCharacterInstruction()
	{
		//Qs,
		var cells = new List<Cell>()
		{
			new(new Point(0, 0), 'Q'),
			new(new Point(1, 0), StringInstructionHandler.StoreCharacterInstruction),
			new(new Point(2, 0), ','),
		};

		InstructionHandlerRunner.Run(new StringInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(Convert.ToInt32('Q'));
				context.Move();
				context.Next();
			}, (context, result) =>
			{
				Assert.Multiple(() =>
				{
					Assert.That(context.Cells[2].Value, Is.EqualTo('Q'));
					var count = context.Values.Count;
					Assert.That(count, Is.EqualTo(0), nameof(context.Values.Count));
				});
			});
	}
}