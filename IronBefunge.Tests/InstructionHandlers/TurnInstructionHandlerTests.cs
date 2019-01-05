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
				TurnInstructionHandler.UpDownInstruction);

		internal override Type GetHandlerType() => typeof(TurnInstructionHandler);

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