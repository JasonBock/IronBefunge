using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class LogicalInstructionHandlerTests
	: InstructionHandlerTests
{
	protected override ImmutableArray<char> GetExpectedHandledInstructions() =>
		[LogicalInstructionHandler.GreaterThanInstruction, LogicalInstructionHandler.NotInstruction];

	protected override Type GetHandlerType() => typeof(LogicalInstructionHandler);

	[Test]
	public static void HandleGreaterThanWithAGreaterThanB()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new LogicalInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(78);
			context.Values.Push(87);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(0), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleGreaterThanWithBGreaterThanA()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new LogicalInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(87);
			context.Values.Push(78);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(1), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleGreaterThanWithOnlyOneValueOnTheStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new LogicalInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(78);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(1), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleGreaterThanWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new LogicalInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount + 1), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(0), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleNotWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Multiple(() =>
				{
					Assert.That(context.Values, Has.Count.EqualTo(stackCount + 1), nameof(context.Values.Count));
					Assert.That(context.Values.Pop(), Is.EqualTo(1), nameof(context.Values.Pop));
				});
			});
	}

	[Test]
	public static void HandleNotWithNonzeroOnTheStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new LogicalInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(87);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(0), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleNotWithZeroOnTheStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new LogicalInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(0);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(1), nameof(context.Values.Pop));
			});
		});
	}
}