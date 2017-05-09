using IronBefunge.InstructionHandlers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Xunit;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class LogicalInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(LogicalInstructionHandler.GreaterThanInstruction,
				LogicalInstructionHandler.NotInstruction);

		internal override Type GetHandlerType() => typeof(LogicalInstructionHandler);

		[Fact]
		public static void HandleGreaterThanWithAGreaterThanB()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(78);
				context.Values.Push(87);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount - 1, context.Values.Count);
				Assert.Equal(0, context.Values.Pop());
			});
		}

		[Fact]
		public static void HandleGreaterThanWithBGreaterThanA()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(87);
				context.Values.Push(78);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount - 1, context.Values.Count);
				Assert.Equal(1, context.Values.Pop());
			});
		}

		[Fact]
		public static void HandleGreaterThanWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(78);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount, context.Values.Count);
				Assert.Equal(1, context.Values.Pop());
			});
		}

		[Fact]
		public static void HandleGreaterThanWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount + 1, context.Values.Count);
				Assert.Equal(0, context.Values.Pop());
			});
		}

		[Fact]
		public static void HandleNotWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount + 1, context.Values.Count);
					Assert.Equal(1, context.Values.Pop());
				});
		}

		[Fact]
		public static void HandleNotWithNonzeroOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(87);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount, context.Values.Count);
					Assert.Equal(0, context.Values.Pop());
				});
		}

		[Fact]
		public static void HandleNotWithZeroOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(0);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount, context.Values.Count);
					Assert.Equal(1, context.Values.Pop());
				});
		}
	}
}
