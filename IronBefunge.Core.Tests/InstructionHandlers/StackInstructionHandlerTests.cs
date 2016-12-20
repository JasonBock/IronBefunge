using IronBefunge.Core.InstructionHandlers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Xunit;

namespace IronBefunge.Core.Tests.InstructionHandlers
{
	public sealed class StackInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions()
		{
			return ImmutableArray.Create(StackInstructionHandler.DuplicateInstruction,
				StackInstructionHandler.PopInstruction, StackInstructionHandler.SwapInstruction);
		}

		internal override Type GetHandlerType()
		{
			return typeof(StackInstructionHandler);
		}

		[Fact]
		public static void HandleDuplicate()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.DuplicateInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(33);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount + 1, context.Values.Count);
				Assert.Equal(33, context.Values.Pop());
				Assert.Equal(33, context.Values.Pop());
			});
		}

		[Fact]
		public static void HandleDuplicateWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.DuplicateInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount + 2, context.Values.Count);
				Assert.Equal(0, context.Values.Pop());
				Assert.Equal(0, context.Values.Pop());
			});
		}

		[Fact]
		public static void HandlePop()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.PopInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(87);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount - 1, context.Values.Count);
				});
		}

		[Fact]
		public static void HandlePopWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.PopInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount, context.Values.Count);
				});
		}

		[Fact]
		public static void HandleSwap()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.SwapInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(87);
					context.Values.Push(78);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount, context.Values.Count);
					Assert.Equal(87, context.Values.Pop());
					Assert.Equal(78, context.Values.Pop());
				});
		}

		[Fact]
		public static void HandleSwapWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.SwapInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(78);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount + 1, context.Values.Count);
					Assert.Equal(78, context.Values.Pop());
					Assert.Equal(0, context.Values.Pop());
				});
		}

		[Fact]
		public static void HandleSwapWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.SwapInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount + 2, context.Values.Count);
				Assert.Equal(0, context.Values.Pop());
				Assert.Equal(0, context.Values.Pop());
			});
		}
	}
}
