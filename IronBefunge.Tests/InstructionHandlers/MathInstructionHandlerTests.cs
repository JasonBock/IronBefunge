using IronBefunge.InstructionHandlers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Xunit;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class MathInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(MathInstructionHandler.AddInstruction,
				MathInstructionHandler.DivideInstruction, MathInstructionHandler.ModInstruction,
				MathInstructionHandler.MultiplyInstruction, MathInstructionHandler.SubtractInstruction);

		internal override Type GetHandlerType() => typeof(MathInstructionHandler);

		[Fact]
		public static void HandleAdd()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.AddInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(3);
					context.Values.Push(7);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount - 1, context.Values.Count);
					Assert.Equal(10, context.Values.Peek());
				});
		}

		[Fact]
		public static void HandleAddWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.AddInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(3);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount, context.Values.Count);
					Assert.Equal(3, context.Values.Peek());
				});
		}

		[Fact]
		public static void HandleAddWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.AddInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount + 1, context.Values.Count);
					Assert.Equal(0, context.Values.Peek());
				});
		}

		[Fact]
		public static void HandleDivide()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.DivideInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(7);
				context.Values.Push(3);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount - 1, context.Values.Count);
				Assert.Equal(2, context.Values.Peek());
			});
		}

		[Fact]
		public static void HandleDivideWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.DivideInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(3);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount, context.Values.Count);
				Assert.Equal(0, context.Values.Peek());
			});
		}

		[Fact]
		public static void HandleDivideWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.DivideInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount + 1, context.Values.Count);
				Assert.Equal(0, context.Values.Peek());
			});
		}

		[Fact]
		public static void HandleMod()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.ModInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(7);
				context.Values.Push(3);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount - 1, context.Values.Count);
				Assert.Equal(1, context.Values.Peek());
			});
		}

		[Fact]
		public static void HandleModWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.ModInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(3);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount, context.Values.Count);
				Assert.Equal(0, context.Values.Peek());
			});
		}

		[Fact]
		public static void HandleModWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.ModInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount + 1, context.Values.Count);
				Assert.Equal(0, context.Values.Peek());
			});
		}

		[Fact]
		public static void HandleMultiply()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.MultiplyInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(3);
					context.Values.Push(7);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount - 1, context.Values.Count);
					Assert.Equal(21, context.Values.Peek());
				});
		}

		[Fact]
		public static void HandleMultiplyWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.MultiplyInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(3);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount, context.Values.Count);
					Assert.Equal(0, context.Values.Peek());
				});
		}

		[Fact]
		public static void HandleMultiplyWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.MultiplyInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount + 1, context.Values.Count);
					Assert.Equal(0, context.Values.Peek());
				});
		}

		[Fact]
		public static void HandleSubtract()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.SubtractInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(3);
					context.Values.Push(7);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount - 1, context.Values.Count);
					Assert.Equal(-4, context.Values.Peek());
				});
		}

		[Fact]
		public static void HandleSubtractWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.SubtractInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(3);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount, context.Values.Count);
					Assert.Equal(3, context.Values.Peek());
				});
		}

		[Fact]
		public static void HandleSubtractWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), MathInstructionHandler.SubtractInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new MathInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount + 1, context.Values.Count);
					Assert.Equal(0, context.Values.Peek());
				});
		}
	}
}
