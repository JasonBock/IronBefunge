using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class NumberInstructionHandlerTests
	 : InstructionHandlerTests
{
	internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
		ImmutableArray.Create(NumberInstructionHandler.ZeroInstruction,
			NumberInstructionHandler.OneInstruction, NumberInstructionHandler.TwoInstruction,
			NumberInstructionHandler.ThreeInstruction, NumberInstructionHandler.FourInstruction,
			NumberInstructionHandler.FiveInstruction, NumberInstructionHandler.SixInstruction,
			NumberInstructionHandler.SevenInstruction, NumberInstructionHandler.EightInstruction,
			NumberInstructionHandler.NineInstruction, NumberInstructionHandler.TenInstruction,
			NumberInstructionHandler.ElevenInstruction, NumberInstructionHandler.TwelveInstruction,
			NumberInstructionHandler.ThirteenInstruction, NumberInstructionHandler.FourteenInstruction,
			NumberInstructionHandler.FifteenInstruction);

	internal override Type GetHandlerType() => typeof(NumberInstructionHandler);

	[TestCase(NumberInstructionHandler.ZeroInstruction, 0)]
	[TestCase(NumberInstructionHandler.OneInstruction, 1)]
	[TestCase(NumberInstructionHandler.TwoInstruction, 2)]
	[TestCase(NumberInstructionHandler.ThreeInstruction, 3)]
	[TestCase(NumberInstructionHandler.FourInstruction, 4)]
	[TestCase(NumberInstructionHandler.FiveInstruction, 5)]
	[TestCase(NumberInstructionHandler.SixInstruction, 6)]
	[TestCase(NumberInstructionHandler.SevenInstruction, 7)]
	[TestCase(NumberInstructionHandler.EightInstruction, 8)]
	[TestCase(NumberInstructionHandler.NineInstruction, 9)]
	[TestCase(NumberInstructionHandler.TenInstruction, 10)]
	[TestCase(NumberInstructionHandler.ElevenInstruction, 11)]
	[TestCase(NumberInstructionHandler.TwelveInstruction, 12)]
	[TestCase(NumberInstructionHandler.ThirteenInstruction, 13)]
	[TestCase(NumberInstructionHandler.FourteenInstruction, 14)]
	[TestCase(NumberInstructionHandler.FifteenInstruction, 15)]
	public static void Handle(char instruction, int expectedValue)
	{
		var cells = new List<Cell>()
			{
				new Cell(new(0, 0), instruction)
			};
		var stackCount = 0;

		InstructionHandlerTests.Run(new NumberInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
				 {
					 Assert.That(context.Values.Count, Is.EqualTo(stackCount + 1), nameof(context.Values.Count));
					 Assert.That(context.Values.Peek(), Is.EqualTo(expectedValue), nameof(context.Values.Peek));
				 });
		});
	}
}