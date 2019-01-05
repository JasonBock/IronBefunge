using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class OutputInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(OutputInstructionHandler.AsciiInstruction,
				OutputInstructionHandler.NumericInstruction);

		internal override Type GetHandlerType() => typeof(OutputInstructionHandler);

		[Test]
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
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That("W", Is.EqualTo(result), nameof(result));
				});
		}

		[Test]
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
				Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That("\0", Is.EqualTo(result), nameof(result));
			});
		}

		[Test]
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
				Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That("87", Is.EqualTo(result), nameof(result));
			});
		}

		[Test]
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
				Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That("0", Is.EqualTo(result), nameof(result));
			});
		}
	}
}
