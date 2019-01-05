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
					Assert.That(stackCount + 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(2, Is.EqualTo(context.Values.Peek()), nameof(context.Values.Peek));
				});
		}
	}
}
