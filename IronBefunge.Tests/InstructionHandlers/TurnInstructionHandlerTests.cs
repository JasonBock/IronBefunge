using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class TurnInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(TurnInstructionHandler.LeftRightInstruction,
				TurnInstructionHandler.TurnLeftInstruction, TurnInstructionHandler.TurnRightInstruction,
				TurnInstructionHandler.UpDownInstruction);

		internal override Type GetHandlerType() => typeof(TurnInstructionHandler);

		[TestCase(TurnInstructionHandler.TurnRightInstruction, Direction.Right, Direction.Down)]
		[TestCase(TurnInstructionHandler.TurnRightInstruction, Direction.Down, Direction.Left)]
		[TestCase(TurnInstructionHandler.TurnRightInstruction, Direction.Left, Direction.Up)]
		[TestCase(TurnInstructionHandler.TurnRightInstruction, Direction.Up, Direction.Right)]
		[TestCase(TurnInstructionHandler.TurnLeftInstruction, Direction.Right, Direction.Up)]
		[TestCase(TurnInstructionHandler.TurnLeftInstruction, Direction.Up, Direction.Left)]
		[TestCase(TurnInstructionHandler.TurnLeftInstruction, Direction.Left, Direction.Down)]
		[TestCase(TurnInstructionHandler.TurnLeftInstruction, Direction.Down, Direction.Right)]
		public static void HandleTurnRightAndLeft(char instruction, Direction currentDiection, Direction expectedDirection)
		{
			var cells = new List<Cell>() { new Cell(new Point(0, 0), instruction) };
			var stackCount = 0;

			InstructionHandlerTests.Run(new TurnInstructionHandler(), cells, (context) =>
			{
				context.Direction = currentDiection;
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.That(context.Values.Count, Is.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Direction, Is.EqualTo(expectedDirection), nameof(context.Direction));
			});
		}

		[Test]
		public static void HandleGoingDown()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), TurnInstructionHandler.UpDownInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new TurnInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(0);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount - 1), nameof(context.Values.Count));
					Assert.That(context.Direction, Is.EqualTo(Direction.Down), nameof(context.Direction));
				});
		}

		[Test]
		public static void HandleGoingDownOrUpWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), TurnInstructionHandler.UpDownInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new TurnInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount), nameof(context.Values.Count));
					Assert.That(context.Direction, Is.EqualTo(Direction.Down), nameof(context.Direction));
				});
		}

		[Test]
		public static void HandleGoingLeft()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), TurnInstructionHandler.LeftRightInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new TurnInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(3);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount - 1), nameof(context.Values.Count));
					Assert.That(context.Direction, Is.EqualTo(Direction.Left), nameof(context.Direction));
				});
		}

		[Test]
		public static void HandleGoingLeftOrRightWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), TurnInstructionHandler.LeftRightInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new TurnInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount), nameof(context.Values.Count));
					Assert.That(context.Direction, Is.EqualTo(Direction.Right), nameof(context.Direction));
				});
		}

		[Test]
		public static void HandleGoingRight()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), TurnInstructionHandler.LeftRightInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new TurnInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(0);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount - 1), nameof(context.Values.Count));
					Assert.That(context.Direction, Is.EqualTo(Direction.Right), nameof(context.Direction));
				});
		}

		[Test]
		public static void HandleGoingUp()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), TurnInstructionHandler.UpDownInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new TurnInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(3);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount - 1), nameof(context.Values.Count));
					Assert.That(context.Direction, Is.EqualTo(Direction.Up), nameof(context.Direction));
				});
		}
	}
}