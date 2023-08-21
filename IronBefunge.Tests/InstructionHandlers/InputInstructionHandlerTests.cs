using IronBefunge.InstructionHandlers;
using IronBefunge.Tests.Mocks;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class InputInstructionHandlerTests
	: InstructionHandlerTests
{
	protected override ImmutableArray<char> GetExpectedHandledInstructions() =>
		ImmutableArray.Create(InputInstructionHandler.AsciiInstruction,
			InputInstructionHandler.NumericInstruction);

	protected override Type GetHandlerType() => typeof(InputInstructionHandler);

	[Test]
	public static void HandleAscii()
	{
		var cells = new List<Cell>() { new Cell(
				new Point(0, 0), InputInstructionHandler.AsciiInstruction) };

		using var reader = new MockAsciiTextReader(88);
		var stackCount = 0;

		InstructionHandlerRunner.Run(new InputInstructionHandler(), cells, null,
			(context, result) =>
			{
				Assert.Multiple(() =>
					 {
						 Assert.That(context.Values, Has.Count.EqualTo(stackCount + 1), nameof(context.Values.Count));
						 Assert.That(context.Values.Peek(), Is.EqualTo(88), nameof(context.Values.Peek));
					 });
			}, reader);
	}

	[Test]
	public static void HandleNumeric()
	{
		var cells = new List<Cell>() { new Cell(
				new Point(0, 0), InputInstructionHandler.NumericInstruction) };

		using var reader = new MockNumericTextReader("123456");
		var stackCount = 0;

		InstructionHandlerRunner.Run(new InputInstructionHandler(), cells, null,
			(context, result) =>
			{
				Assert.Multiple(() =>
					 {
						 Assert.That(context.Values, Has.Count.EqualTo(stackCount + 1), nameof(context.Values.Count));
						 Assert.That(context.Values.Peek(), Is.EqualTo(123456), nameof(context.Values.Peek));
					 });
			}, reader);
	}

	[Test]
	public static void HandleNumericWithInvalidData()
	{
		var cells = new List<Cell>() { new Cell(
				new Point(0, 0), InputInstructionHandler.NumericInstruction) };

		using var reader = new MockNumericTextReader("quux");
		var stackCount = 0;

		InstructionHandlerRunner.Run(new InputInstructionHandler(), cells, null,
			(context, result) =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
			}, reader);
	}
}