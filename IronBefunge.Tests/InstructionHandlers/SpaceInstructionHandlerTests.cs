using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class SpaceInstructionHandlerTests
	: InstructionHandlerTests
{
	internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
		ImmutableArray.Create(SpaceInstructionHandler.GetInstruction,
			SpaceInstructionHandler.PutInstruction);

	internal override Type GetHandlerType() => typeof(SpaceInstructionHandler);

	[Test]
	public static void HandleGet()
	{
		var cells = new List<Cell>() {
			new Cell(new(0, 0), SpaceInstructionHandler.GetInstruction),
			new Cell(new(2, 3), 'w') };

		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(2);
			context.Values.Push(3);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo((int)'w'), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleGetWhenCellDoesNotExist()
	{
		var cells = new List<Cell>() { new Cell(
			new Point(0, 0), SpaceInstructionHandler.GetInstruction) };

		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(2);
			context.Values.Push(3);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo((int)' '), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleGetWithOnlyOneValueOnTheStack()
	{
		var cells = new List<Cell>() {
			new Cell(new(0, 0), SpaceInstructionHandler.GetInstruction),
			new Cell(new(2, 0), 'w') };

		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(2);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo((int)'w'), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleGetWithEmptyStack()
	{
		var cells = new List<Cell>() {
			new Cell(new(0, 0), SpaceInstructionHandler.GetInstruction),
			new Cell(new(2, 2), 'w') };

		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount + 1), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo((int)SpaceInstructionHandler.GetInstruction), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandlePut()
	{
		var cells = new List<Cell>() {
			new Cell(new(0, 0), SpaceInstructionHandler.PutInstruction),
			new Cell(new(2, 3), 'w') };
		var cellCount = cells.Count;
		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(88);
			context.Values.Push(2);
			context.Values.Push(3);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount - 3), nameof(context.Values.Count));
				Assert.That(context.Cells.Count, Is.EqualTo(cellCount), nameof(context.Cells.Count));
				Assert.That(context.Find(new Point(2, 3)).Value, Is.EqualTo('X'), nameof(Cell.Value));
			});
		});
	}

	[Test]
	public static void HandlePutWhenCellDoesNotExist()
	{
		var cells = new List<Cell>() { new Cell(
			new Point(0, 0), SpaceInstructionHandler.PutInstruction) };
		var cellCount = cells.Count;
		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(88);
			context.Values.Push(2);
			context.Values.Push(3);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount - 3), nameof(context.Values.Count));
				Assert.That(context.Cells.Count, Is.EqualTo(cellCount + 1), nameof(context.Cells.Count));
				Assert.That(context.Find(new Point(2, 3)).Value, Is.EqualTo('X'), nameof(Cell.Value));
			});
		});
	}

	[Test]
	public static void HandlePutWithOnlyTwoValuesOnTheStack()
	{
		var cells = new List<Cell>() {
			new Cell(new(0, 0), SpaceInstructionHandler.PutInstruction),
			new Cell(new(2, 0), 'w') };
		var cellCount = cells.Count;
		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(88);
			context.Values.Push(2);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount - 2), nameof(context.Values.Count));
				Assert.That(context.Cells.Count, Is.EqualTo(cellCount), nameof(context.Cells.Count));
				Assert.That(context.Find(new Point(2, 0)).Value, Is.EqualTo('X'), nameof(Cell.Value));
			});
		});
	}

	[Test]
	public static void HandlePutWithOnlyOneValueOnTheStack()
	{
		var cells = new List<Cell>() { new Cell(
			new Point(0, 0), SpaceInstructionHandler.PutInstruction) };
		var cellCount = cells.Count;
		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(88);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(context.Cells.Count, Is.EqualTo(cellCount), nameof(context.Cells.Count));
				Assert.That(context.Find(new Point(0, 0)).Value, Is.EqualTo('X'), nameof(Cell.Value));
			});
		});
	}

	[Test]
	public static void HandlePutWithEmptyStack()
	{
		var cells = new List<Cell>() { new Cell(
			new Point(0, 0), SpaceInstructionHandler.PutInstruction) };
		var cellCount = cells.Count;
		var stackCount = 0;

		InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Cells.Count, Is.EqualTo(cellCount), nameof(context.Cells.Count));
				Assert.That(context.Find(new Point(0, 0)).Value, Is.EqualTo('\0'), nameof(Cell.Value));
			});
		});
	}
}