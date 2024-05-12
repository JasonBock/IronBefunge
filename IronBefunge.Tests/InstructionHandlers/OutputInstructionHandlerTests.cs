using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class OutputInstructionHandlerTests
	: InstructionHandlerTests
{
	protected override ImmutableArray<char> GetExpectedHandledInstructions() =>
		[OutputInstructionHandler.AsciiInstruction, OutputInstructionHandler.NumericInstruction];

	protected override Type GetHandlerType() => typeof(OutputInstructionHandler);

	[Test]
	public static void HandleAscii()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), OutputInstructionHandler.AsciiInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new OutputInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(87);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(result, Is.EqualTo("W"), nameof(result));
			});
		});
	}

	[Test]
	public static void HandleAsciiWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), OutputInstructionHandler.AsciiInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new OutputInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(result, Is.EqualTo("\0"), nameof(result));
			});
		});
	}

	[Test]
	public static void HandleInteger()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), OutputInstructionHandler.NumericInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new OutputInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(87);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
				Assert.That(result, Is.EqualTo("87"), nameof(result));
			});
		});
	}

	[Test]
	public static void HandleIntegerWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new Point(0, 0), OutputInstructionHandler.NumericInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new OutputInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(result, Is.EqualTo("0"), nameof(result));
			});
		});
	}
}