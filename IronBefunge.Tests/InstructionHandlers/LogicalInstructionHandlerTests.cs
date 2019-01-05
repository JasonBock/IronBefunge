using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class LogicalInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(LogicalInstructionHandler.GreaterThanInstruction,
				LogicalInstructionHandler.NotInstruction);

		internal override Type GetHandlerType() => typeof(LogicalInstructionHandler);

		[Test]
		public static void HandleGreaterThanWithAGreaterThanB()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(78);
				context.Values.Push(87);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That(0, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
			});
		}

		[Test]
		public static void HandleGreaterThanWithBGreaterThanA()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(87);
				context.Values.Push(78);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.That(stackCount - 1, Is.EqualTo(context.Values.Count));
				Assert.That(1, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
			});
		}

		[Test]
		public static void HandleGreaterThanWithOnlyOneValueOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				context.Values.Push(78);
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That(1, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
			});
		}

		[Test]
		public static void HandleGreaterThanWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.GreaterThanInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
			}, (context, result) =>
			{
				Assert.That(stackCount + 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
				Assert.That(0, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
			});
		}

		[Test]
		public static void HandleNotWithEmptyStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
				{
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount + 1, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(1, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				});
		}

		[Test]
		public static void HandleNotWithNonzeroOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(87);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(0, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				});
		}

		[Test]
		public static void HandleNotWithZeroOnTheStack()
		{
			var cells = new List<Cell>() { new Cell(
				new Point(0, 0), LogicalInstructionHandler.NotInstruction) };

			var stackCount = 0;

			InstructionHandlerTests.Run(new LogicalInstructionHandler(), cells, (context) =>
				{
					context.Values.Push(0);
					stackCount = context.Values.Count;
				}, (context, result) =>
				{
					Assert.That(stackCount, Is.EqualTo(context.Values.Count), nameof(context.Values.Count));
					Assert.That(1, Is.EqualTo(context.Values.Pop()), nameof(context.Values.Pop));
				});
		}
	}
}
