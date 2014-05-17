using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IronBefunge.Core.InstructionHandlers;
using Xunit;

namespace IronBefunge.Core.Tests.InstructionHandlers
{
	public sealed class NumberInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ReadOnlyCollection<char> GetExpectedHandledInstructions()
		{
			return new List<char>() { NumberInstructionHandler.ZeroInstruction, 
				NumberInstructionHandler.OneInstruction, NumberInstructionHandler.TwoInstruction,
				NumberInstructionHandler.ThreeInstruction, NumberInstructionHandler.FourInstruction,
				NumberInstructionHandler.FiveInstruction, NumberInstructionHandler.SixInstruction,
				NumberInstructionHandler.SevenInstruction, NumberInstructionHandler.EightInstruction,
				NumberInstructionHandler.NineInstruction }.AsReadOnly();
		}

		internal override Type GetHandlerType()
		{
			return typeof(NumberInstructionHandler);
		}

		[Fact]
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
					Assert.Equal(stackCount + 1, context.Values.Count);
					Assert.Equal(2, context.Values.Peek());
				});
		}
	}
}
