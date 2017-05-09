using IronBefunge.InstructionHandlers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Xunit;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class OutputInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(OutputInstructionHandler.AsciiInstruction,
				OutputInstructionHandler.NumericInstruction);

		internal override Type GetHandlerType() => typeof(OutputInstructionHandler);

		[Fact]
		public static void HandleAscii()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), OutputInstructionHandler.AsciiInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new OutputInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(87);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.Equal(stackCount - 1, context.Values.Count);
					Assert.Equal("W", result);
				});
		}

		[Fact]
		public static void HandleAsciiWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), OutputInstructionHandler.AsciiInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new OutputInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount, context.Values.Count);
				Assert.Equal("\0", result);
			});
		}

		[Fact]
		public static void HandleInteger()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), OutputInstructionHandler.NumericInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new OutputInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(87);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount - 1, context.Values.Count);
				Assert.Equal("87", result);
			});
		}

		[Fact]
		public static void HandleIntegerWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), OutputInstructionHandler.NumericInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new OutputInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount, context.Values.Count);
				Assert.Equal("0", result);
			});
		}
	}
}
