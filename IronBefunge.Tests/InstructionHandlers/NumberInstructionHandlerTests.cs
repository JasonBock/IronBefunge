using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class NumberInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(NumberInstructionHandler.ZeroInstruction, 
				NumberInstructionHandler.OneInstruction, NumberInstructionHandler.TwoInstruction,
				NumberInstructionHandler.ThreeInstruction, NumberInstructionHandler.FourInstruction,
				NumberInstructionHandler.FiveInstruction, NumberInstructionHandler.SixInstruction,
				NumberInstructionHandler.SevenInstruction, NumberInstructionHandler.EightInstruction,
				NumberInstructionHandler.NineInstruction);

		internal override Type GetHandlerType() => typeof(NumberInstructionHandler);

		[Test]
		public static void Handle()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), NumberInstructionHandler.TwoInstruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new NumberInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount + 1), nameof(context.Values.Count));
					Assert.That(context.Values.Peek(), Is.EqualTo(2), nameof(context.Values.Peek));
				});
		}
	}
}
