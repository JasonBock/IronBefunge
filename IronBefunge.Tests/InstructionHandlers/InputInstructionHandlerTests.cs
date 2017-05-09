using IronBefunge.InstructionHandlers;
using IronBefunge.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Xunit;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class InputInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(InputInstructionHandler.AsciiInstruction,
				InputInstructionHandler.NumericInstruction);

		internal override Type GetHandlerType() => typeof(InputInstructionHandler);

		[Fact]
		public static void HandleAscii()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), InputInstructionHandler.AsciiInstruction) };

			using(var reader = new MockAsciiTextReader(88))
			{
				var stackCount = 0;

				InstructionHandlerTests.Run(new InputInstructionHandler(), cells, null,
					(context, result) =>
					{
						Assert.Equal(stackCount + 1, context.Values.Count);
						Assert.Equal(88, context.Values.Peek());
					}, reader);
			}
		}

		[Fact]
		public static void HandleNumeric()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), InputInstructionHandler.NumericInstruction) };

			using(var reader = new MockNumericTextReader("123456"))
			{
				var stackCount = 0;

				InstructionHandlerTests.Run(new InputInstructionHandler(), cells, null,
					(context, result) =>
					{
						Assert.Equal(stackCount + 1, context.Values.Count);
						Assert.Equal(123456, context.Values.Peek());
					}, reader);
			}
		}

		[Fact]
		public static void HandleNumericWithInvalidData()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), InputInstructionHandler.NumericInstruction) };

			using(var reader = new MockNumericTextReader("quux"))
			{
				var stackCount = 0;

				InstructionHandlerTests.Run(new InputInstructionHandler(), cells, null,
					(context, result) =>
					{
						Assert.Equal(stackCount, context.Values.Count);
					}, reader);
			}
		}
	}
}
