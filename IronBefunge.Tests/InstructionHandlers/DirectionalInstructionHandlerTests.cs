using IronBefunge.InstructionHandlers;
using IronBefunge.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class DirectionalInstructionHandlerTests
		: InstructionHandlerTests
	{
		private static void Handle(char instruction, Direction direction)
		{
			var cells = new List<Cell>() { new Cell(new(0, 0), instruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new DirectionalInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Multiple(() =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount), nameof(context.Values.Count));
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
				new Cell(new(0, 0), DirectionalInstructionHandler.TrampolineInstruction),
				new Cell(new(1, 0), '3')
			};

			InstructionHandlerTests.Run(new DirectionalInstructionHandler(), cells, null,
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
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), DirectionalInstructionHandler.RandomInstruction) };

			using var random = new MockSecureRandom(direction);
			InstructionHandlerTests.Run(new DirectionalInstructionHandler(), cells, null,
				(context, result) =>
				{
					Assert.That(context.Direction, Is.EqualTo(direction), nameof(context.Direction));
				}, random);
		}

		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(DirectionalInstructionHandler.DownInstruction,
				DirectionalInstructionHandler.LeftInstruction, DirectionalInstructionHandler.RandomInstruction,
				DirectionalInstructionHandler.RightInstruction, DirectionalInstructionHandler.TrampolineInstruction,
				DirectionalInstructionHandler.UpInstruction);

		internal override Type GetHandlerType() => typeof(DirectionalInstructionHandler);
	}
}