using IronBefunge.InstructionHandlers;
using IronBefunge.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class InputInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(InputInstructionHandler.AsciiInstruction,
				InputInstructionHandler.NumericInstruction);

		internal override Type GetHandlerType() => typeof(InputInstructionHandler);

		[Test]
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
						Assert.That(stackCount + 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
						Assert.That(88, Is.EqualTo(context.Values.Peek()), nameof(context.Values.Peek));
					}, reader);
			}
		}

		[Test]
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
						Assert.That(stackCount + 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
						Assert.That(123456, Is.EqualTo(context.Values.Peek()), nameof(context.Values.Peek));
					}, reader);
			}
		}

		[Test]
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
						Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					}, reader);
			}
		}
	}
}