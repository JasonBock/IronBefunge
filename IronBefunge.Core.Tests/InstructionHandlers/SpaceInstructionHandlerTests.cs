using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IronBefunge.Core.InstructionHandlers;
using Xunit;

namespace IronBefunge.Core.Tests.InstructionHandlers
{
	public sealed class SpaceInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ReadOnlyCollection<char> GetExpectedHandledInstructions()
		{
			return new List<char>() { SpaceInstructionHandler.GetInstruction,
				SpaceInstructionHandler.PutInstruction }.AsReadOnly();
		}

		internal override Type GetHandlerType()
		{
			return typeof(SpaceInstructionHandler);
		}

		[Fact]
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
					Assert.Equal(stackCount - 1, context.Values.Count);
					Assert.Equal((int)'w', context.Values.Pop());
				});
		}

		[Fact]
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
					Assert.Equal(stackCount - 1, context.Values.Count);
					Assert.Equal((int)' ', context.Values.Pop());
				});
		}

		[Fact]
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
					Assert.Equal(stackCount, context.Values.Count);
					Assert.Equal((int)'w', context.Values.Pop());
				});
		}

		[Fact]
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
					Assert.Equal(stackCount + 1, context.Values.Count);
					Assert.Equal((int)SpaceInstructionHandler.GetInstruction, context.Values.Pop());
				});
		}

		[Fact]
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
					Assert.Equal(stackCount - 3, context.Values.Count);
					Assert.Equal(cellCount, context.Cells.Count);
					Assert.Equal('X', context.Find(new Point(2, 3)).Value);
				});
		}

		[Fact]
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
					Assert.Equal(stackCount - 3, context.Values.Count);
					Assert.Equal(cellCount + 1, context.Cells.Count);
					Assert.Equal('X', context.Find(new Point(2, 3)).Value);
				});
		}

		[Fact]
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
					Assert.Equal(stackCount - 2, context.Values.Count);
					Assert.Equal(cellCount, context.Cells.Count);
					Assert.Equal('X', context.Find(new Point(2, 0)).Value);
				});
		}

		[Fact]
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
					Assert.Equal(stackCount - 1, context.Values.Count);
					Assert.Equal(cellCount, context.Cells.Count);
					Assert.Equal('X', context.Find(new Point(0, 0)).Value);
				});
		}

		[Fact]
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
					Assert.Equal(stackCount, context.Values.Count);
					Assert.Equal(cellCount, context.Cells.Count);
					Assert.Equal('\0', context.Find(new Point(0, 0)).Value);
				});
		}
	}
}
