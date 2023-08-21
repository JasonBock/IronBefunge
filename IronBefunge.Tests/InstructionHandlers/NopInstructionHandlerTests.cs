using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class NopInstructionHandlerTests
	: InstructionHandlerTests
{
	protected override ImmutableArray<char> GetExpectedHandledInstructions() =>
		ImmutableArray.Create(NopInstructionHandler.NopInstruction);

	protected override Type GetHandlerType() => typeof(NopInstructionHandler);

	[Test]
	public static void HandleGet()
	{
		var cells = new List<Cell>()
			{
				new Cell(new(0, 0), NopInstructionHandler.NopInstruction)
			};

		var stackCount = 0;
		var direction = Direction.Down;

		InstructionHandlerRunner.Run(new NopInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
			direction = context.Direction;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
				 {
					 Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
					 Assert.That(context.Direction, Is.EqualTo(direction), nameof(context.Direction));
				 });
		});
	}
}