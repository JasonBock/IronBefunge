using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class TurnInstructionHandlerTests
	: InstructionHandlerTests
{
	protected override ImmutableArray<char> GetExpectedHandledInstructions() =>
		[
		   TurnInstructionHandler.CompareInstruction,
		   TurnInstructionHandler.LeftRightInstruction,
		   TurnInstructionHandler.ReverseInstruction,
		   TurnInstructionHandler.TurnLeftInstruction,
		   TurnInstructionHandler.TurnRightInstruction,
		   TurnInstructionHandler.UpDownInstruction,
		];

	protected override Type GetHandlerType() => typeof(TurnInstructionHandler);

	[TestCase(0, 1, Direction.Right, Direction.Up)]
	[TestCase(0, 1, Direction.Up, Direction.Left)]
	[TestCase(0, 1, Direction.Left, Direction.Down)]
	[TestCase(0, 1, Direction.Down, Direction.Right)]
	[TestCase(1, 0, Direction.Right, Direction.Down)]
	[TestCase(1, 0, Direction.Down, Direction.Left)]
	[TestCase(1, 0, Direction.Left, Direction.Up)]
	[TestCase(1, 0, Direction.Up, Direction.Right)]
	[TestCase(0, 0, Direction.Right, Direction.Right)]
	[TestCase(0, 0, Direction.Down, Direction.Down)]
	[TestCase(0, 0, Direction.Left, Direction.Left)]
	[TestCase(0, 0, Direction.Up, Direction.Up)]
	public static void HandleCompare(int a, int b, Direction currentDiection, Direction expectedDirection)
	{
		var cells = new List<Cell>() { new(new Point(0, 0), TurnInstructionHandler.CompareInstruction) };
		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(a);
			context.Values.Push(b);
			context.Direction = currentDiection;
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 2), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(expectedDirection), nameof(context.Direction));
			});
		});
	}

	[TestCase(Direction.Right, Direction.Right)]
	[TestCase(Direction.Down, Direction.Down)]
	[TestCase(Direction.Left, Direction.Left)]
	[TestCase(Direction.Up, Direction.Up)]
	public static void HandleCompareWithEmptyStack(Direction currentDiection, Direction expectedDirection)
	{
		var cells = new List<Cell>() { new(new Point(0, 0), TurnInstructionHandler.CompareInstruction) };
		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			context.Direction = currentDiection;
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(expectedDirection), nameof(context.Direction));
			});
		});
	}

	[TestCase(TurnInstructionHandler.ReverseInstruction, Direction.Right, Direction.Left)]
	[TestCase(TurnInstructionHandler.ReverseInstruction, Direction.Down, Direction.Up)]
	[TestCase(TurnInstructionHandler.ReverseInstruction, Direction.Left, Direction.Right)]
	[TestCase(TurnInstructionHandler.ReverseInstruction, Direction.Up, Direction.Down)]
	public static void HandleReverse(char instruction, Direction currentDiection, Direction expectedDirection)
	{
		var cells = new List<Cell>() { new(new Point(0, 0), instruction) };
		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			context.Direction = currentDiection;
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(expectedDirection), nameof(context.Direction));
			});
		});
	}

	[TestCase(TurnInstructionHandler.TurnRightInstruction, Direction.Right, Direction.Down)]
	[TestCase(TurnInstructionHandler.TurnRightInstruction, Direction.Down, Direction.Left)]
	[TestCase(TurnInstructionHandler.TurnRightInstruction, Direction.Left, Direction.Up)]
	[TestCase(TurnInstructionHandler.TurnRightInstruction, Direction.Up, Direction.Right)]
	[TestCase(TurnInstructionHandler.TurnLeftInstruction, Direction.Right, Direction.Up)]
	[TestCase(TurnInstructionHandler.TurnLeftInstruction, Direction.Up, Direction.Left)]
	[TestCase(TurnInstructionHandler.TurnLeftInstruction, Direction.Left, Direction.Down)]
	[TestCase(TurnInstructionHandler.TurnLeftInstruction, Direction.Down, Direction.Right)]
	public static void HandleTurnRightAndLeft(char instruction, Direction currentDiection, Direction expectedDirection)
	{
		var cells = new List<Cell>() { new(new Point(0, 0), instruction) };
		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			context.Direction = currentDiection;
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(expectedDirection), nameof(context.Direction));
			});
		});
	}

	[Test]
	public static void HandleGoingDown()
	{
		var cells = new List<Cell>() { new(
				new Point(0, 0), TurnInstructionHandler.UpDownInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(0);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(Direction.Down), nameof(context.Direction));
			});
		});
	}

	[Test]
	public static void HandleGoingDownOrUpWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), TurnInstructionHandler.UpDownInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(Direction.Down), nameof(context.Direction));
			});
		});
	}

	[Test]
	public static void HandleGoingLeft()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), TurnInstructionHandler.LeftRightInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(3);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(Direction.Left), nameof(context.Direction));
			});
		});
	}

	[Test]
	public static void HandleGoingLeftOrRightWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), TurnInstructionHandler.LeftRightInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(Direction.Right), nameof(context.Direction));
			});
		});
	}

	[Test]
	public static void HandleGoingRight()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), TurnInstructionHandler.LeftRightInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(0);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(Direction.Right), nameof(context.Direction));
			});
		});
	}

	[Test]
	public static void HandleGoingUp()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), TurnInstructionHandler.UpDownInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new TurnInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(3);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(Direction.Up), nameof(context.Direction));
			});
		});
	}
}