using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class StackInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(StackInstructionHandler.DuplicateInstruction,
				StackInstructionHandler.PopInstruction, StackInstructionHandler.SwapInstruction);

		internal override Type GetHandlerType() => typeof(StackInstructionHandler);

		[Test]
		public static void HandleDuplicate()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.DuplicateInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(33);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.That(stackCount + 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That(33, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				Assert.That(33, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
			});
		}

		[Test]
		public static void HandleDuplicateWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.DuplicateInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.That(stackCount + 2, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That(0, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				Assert.That(0, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
			});
		}

		[Test]
		public static void HandlePop()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.PopInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(87);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				});
		}

		[Test]
		public static void HandlePopWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.PopInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				});
		}

		[Test]
		public static void HandleSwap()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.SwapInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(87);
					context.Values.Push(78);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(87, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
					Assert.That(78, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				});
		}

		[Test]
		public static void HandleSwapWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.SwapInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(78);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount + 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(78, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
					Assert.That(0, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				});
		}

		[Test]
		public static void HandleSwapWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), StackInstructionHandler.SwapInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new StackInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.That(stackCount + 2, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That(0, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				Assert.That(0, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
			});
		}
	}
}
