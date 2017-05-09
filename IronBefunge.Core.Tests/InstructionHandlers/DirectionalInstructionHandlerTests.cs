using IronBefunge.Core.InstructionHandlers;
using IronBefunge.Core.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Xunit;

namespace IronBefunge.Core.Tests.InstructionHandlers
{
	public sealed class DirectionalInstructionHandlerTests
		: InstructionHandlerTests
	{
		private static void Handle(char instruction, Direction direction)
		{
			var cells = new List<Cell>() { new Cell(new Point(0, 0), instruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new DirectionalInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.Equal(stackCount, context.Values.Count);
				Assert.Equal(direction, context.Direction);
			});
		}

		[Fact]
		public static void HandleDown() =>
			DirectionalInstructionHandlerTests.Handle(
				DirectionalInstructionHandler.DownInstruction, Direction.Down);

		[Fact]
		public static void HandleLeft() =>
			DirectionalInstructionHandlerTests.Handle(
				DirectionalInstructionHandler.LeftInstruction, Direction.Left);

		[Fact]
		public static void HandleRandomDown() =>
			DirectionalInstructionHandlerTests.Randomizer(Direction.Down);

		[Fact]
		public static void HandleRandomLeft() =>
			DirectionalInstructionHandlerTests.Randomizer(Direction.Left);

		[Fact]
		public static void HandleRandomRight() =>
			DirectionalInstructionHandlerTests.Randomizer(Direction.Right);

		[Fact]
		public static void HandleRandomUp() =>
			DirectionalInstructionHandlerTests.Randomizer(Direction.Up);

		[Fact]
		public static void HandleRight() =>
			DirectionalInstructionHandlerTests.Handle(
				DirectionalInstructionHandler.RightInstruction, Direction.Right);

		[Fact]
		public static void HandleTrampoline()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), DirectionalInstructionHandler.TrampolineInstruction),
				new Cell(new Point(0, 1), '3')};

			InstructionHandlerTests.Run(new DirectionalInstructionHandler(), cells, null,
				(context, result) =>
				{
					Assert.Equal(new Point(0, 1), context.CurrentPosition);
				});
		}

		[Fact]
		public static void HandleUp() =>
			DirectionalInstructionHandlerTests.Handle(
				DirectionalInstructionHandler.UpInstruction, Direction.Up);

		private static void Randomizer(Direction direction)
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), DirectionalInstructionHandler.RandomInstruction) };

			using (var random = new MockSecureRandom(direction))
			{
				InstructionHandlerTests.Run(new DirectionalInstructionHandler(), cells, null,
					(context, result) =>
					{
						Assert.Equal(direction, context.Direction);
					}, random);
			}
		}

		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(DirectionalInstructionHandler.DownInstruction,
				DirectionalInstructionHandler.LeftInstruction, DirectionalInstructionHandler.RandomInstruction,
				DirectionalInstructionHandler.RightInstruction, DirectionalInstructionHandler.TrampolineInstruction,
				DirectionalInstructionHandler.UpInstruction);

		internal override Type GetHandlerType() => typeof(DirectionalInstructionHandler);
	}
}
