using IronBefunge.InstructionHandlers;
using IronBefunge.Tests.Mocks;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class DirectionalInstructionHandlerTests
	: InstructionHandlerTests
{
	protected override ImmutableArray<char> GetExpectedHandledInstructions() =>
		[
		   DirectionalInstructionHandler.DownInstruction,
		   DirectionalInstructionHandler.LeftInstruction,
		   DirectionalInstructionHandler.RandomInstruction,
		   DirectionalInstructionHandler.RightInstruction,
		   DirectionalInstructionHandler.TrampolineInstruction,
		   DirectionalInstructionHandler.UpInstruction,
		];

	protected override Type GetHandlerType() => typeof(DirectionalInstructionHandler);

	private static void Handle(char instruction, Direction direction)
	{
		var cells = new List<Cell>() { new(new Point(0, 0), instruction) };
		var stackCount = 0;

		InstructionHandlerRunner.Run(new DirectionalInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(direction), nameof(context.Direction));
			});
		});
	}

	[Test]
	public static void HandleDown() =>
		DirectionalInstructionHandlerTests.Handle(
			DirectionalInstructionHandler.DownInstruction, Direction.Down);

	[Test]
	public static void HandleLeft() =>
		DirectionalInstructionHandlerTests.Handle(
			DirectionalInstructionHandler.LeftInstruction, Direction.Left);

	[Test]
	public static void HandleRandomDown() =>
		DirectionalInstructionHandlerTests.Randomizer(Direction.Down);

	[Test]
	public static void HandleRandomLeft() =>
		DirectionalInstructionHandlerTests.Randomizer(Direction.Left);

	[Test]
	public static void HandleRandomRight() =>
		DirectionalInstructionHandlerTests.Randomizer(Direction.Right);

	[Test]
	public static void HandleRandomUp() =>
		DirectionalInstructionHandlerTests.Randomizer(Direction.Up);

	[Test]
	public static void HandleRight() =>
		DirectionalInstructionHandlerTests.Handle(
			DirectionalInstructionHandler.RightInstruction, Direction.Right);

	[Test]
	public static void HandleTrampoline()
	{
		var cells = new List<Cell>()
			{
				new(new Point(0, 0), DirectionalInstructionHandler.TrampolineInstruction),
				new(new Point(1, 0), '3')
			};

		InstructionHandlerRunner.Run(new DirectionalInstructionHandler(), cells, null,
			(context, result) =>
			{
				Assert.That(context.CurrentPosition, Is.EqualTo(new Point(1, 0)), nameof(context.CurrentPosition));
			});
	}

	[Test]
	public static void HandleUp() =>
		DirectionalInstructionHandlerTests.Handle(
			DirectionalInstructionHandler.UpInstruction, Direction.Up);

	private static void Randomizer(Direction direction)
	{
		var cells = new List<Cell>() { new(
				new Point(0, 0), DirectionalInstructionHandler.RandomInstruction) };

		var random = new MockSecureRandom(direction);
		InstructionHandlerRunner.Run(new DirectionalInstructionHandler(), cells, null,
			(context, result) =>
			{
				Assert.That(context.Direction, Is.EqualTo(direction), nameof(context.Direction));
			}, random);
	}
}