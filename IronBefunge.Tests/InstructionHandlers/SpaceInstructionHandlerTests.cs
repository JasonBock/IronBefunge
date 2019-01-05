using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
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
				new Cell(new Point(0, 0), SpaceInstructionHandler.GetInstruction),
				new Cell(new Point(2, 3), 'w') };

			var stackCount = 0;

			InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(2);
					context.Values.Push(3);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That((int)'w', Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
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
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That((int)' ', Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				});
		}

		[Test]
		public static void HandleGetWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { 
				new Cell(new Point(0, 0), SpaceInstructionHandler.GetInstruction),
				new Cell(new Point(2, 0), 'w') };

			var stackCount = 0;

			InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(2);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That((int)'w', Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				});
		}

		[Test]
		public static void HandleGetWithEmptyStack()
		{
			var cells = new List<Cell>() { 
				new Cell(new Point(0, 0), SpaceInstructionHandler.GetInstruction),
				new Cell(new Point(2, 2), 'w') };

			var stackCount = 0;

			InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount + 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That((int)SpaceInstructionHandler.GetInstruction, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				});
		}

		[Test]
		public static void HandlePut()
		{
			var cells = new List<Cell>() { 
				new Cell(new Point(0, 0), SpaceInstructionHandler.PutInstruction),
				new Cell(new Point(2, 3), 'w') };
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
					Assert.That(stackCount - 3, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(cellCount, Is.EqualTo(context.Cells.Count), nameof(context.Cells.Count));
					Assert.That('X', Is.EqualTo(context.Find(new Point(2, 3)).Value), nameof(Cell.Value));
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
					Assert.That(stackCount - 3, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(cellCount + 1, Is.EqualTo(context.Cells.Count), nameof(context.Cells.Count));
					Assert.That('X', Is.EqualTo(context.Find(new Point(2, 3)).Value), nameof(Cell.Value));
				});
		}

		[Test]
		public static void HandlePutWithOnlyTwoValuesOnTheStack()
		{
			var cells = new List<Cell>() { 
				new Cell(new Point(0, 0), SpaceInstructionHandler.PutInstruction),
				new Cell(new Point(2, 0), 'w') };
			var cellCount = cells.Count;
			var stackCount = 0;

			InstructionHandlerTests.Run(new SpaceInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(88);
					context.Values.Push(2);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount - 2, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(cellCount, Is.EqualTo(context.Cells.Count), nameof(context.Cells.Count));
					Assert.That('X', Is.EqualTo(context.Find(new Point(2, 0)).Value), nameof(Cell.Value));
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
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(cellCount, Is.EqualTo(context.Cells.Count), nameof(context.Cells.Count));
					Assert.That('X', Is.EqualTo(context.Find(new Point(0, 0)).Value), nameof(Cell.Value));
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
					Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(cellCount, Is.EqualTo(context.Cells.Count), nameof(context.Cells.Count));
					Assert.That('\0', Is.EqualTo(context.Find(new Point(0, 0)).Value), nameof(Cell.Value));
				});
		}
	}
}
