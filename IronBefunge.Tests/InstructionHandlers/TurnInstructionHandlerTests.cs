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
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(Direction.Down, Is.EqualTo(context.Direction), nameof(context.Direction));
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
					Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(Direction.Down, Is.EqualTo(context.Direction), nameof(context.Direction));
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
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(Direction.Left, Is.EqualTo(context.Direction), nameof(context.Direction));
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
					Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(Direction.Right, Is.EqualTo(context.Direction), nameof(context.Direction));
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
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(Direction.Right, Is.EqualTo(context.Direction), nameof(context.Direction));
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
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(Direction.Up, Is.EqualTo(context.Direction), nameof(context.Direction));
				});
		}
	}
}
