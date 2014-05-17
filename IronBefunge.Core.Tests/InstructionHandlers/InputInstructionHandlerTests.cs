using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IronBefunge.Core.InstructionHandlers;
using IronBefunge.Core.Tests.Mocks;
using Xunit;

namespace IronBefunge.Core.Tests.InstructionHandlers
{
	public sealed class InputInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ReadOnlyCollection<char> GetExpectedHandledInstructions()
		{
			return new List<char>() { InputInstructionHandler.AsciiInstruction,
				InputInstructionHandler.NumericInstruction }.AsReadOnly();
		}

		internal override Type GetHandlerType()
		{
			return typeof(InputInstructionHandler);
		}

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
