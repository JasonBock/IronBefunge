using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers;

public sealed class StackInstructionHandlerTests
	: InstructionHandlerTests
{
	protected override ImmutableArray<char> GetExpectedHandledInstructions() =>
		[
		   StackInstructionHandler.ClearInstruction,
		   StackInstructionHandler.DuplicateInstruction,
		   StackInstructionHandler.PopInstruction,
		   StackInstructionHandler.SwapInstruction,
		];

	protected override Type GetHandlerType() => typeof(StackInstructionHandler);

	[Test]
	public static void HandleClear()
	{
		var cells = new List<Cell>() { new(new(0, 0), StackInstructionHandler.ClearInstruction) };

		InstructionHandlerRunner.Run(new StackInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(33);
			context.Values.Push(33);
		}, (context, result) =>
		{
			Assert.That(context.Values, Is.Empty, nameof(context.Values.Count));
		});
	}

	[Test]
	public static void HandleDuplicate()
	{
		var cells = new List<Cell>() { new(new(0, 0), StackInstructionHandler.DuplicateInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new StackInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(33);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount + 1), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(33), nameof(context.Values.Pop));
				Assert.That(context.Values.Pop(), Is.EqualTo(33), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleDuplicateWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new(0, 0), StackInstructionHandler.DuplicateInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new StackInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount + 2), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(0), nameof(context.Values.Pop));
				Assert.That(context.Values.Pop(), Is.EqualTo(0), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandlePop()
	{
		var cells = new List<Cell>() { new(new(0, 0), StackInstructionHandler.PopInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new StackInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(87);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.That(context.Values, Has.Count.EqualTo(stackCount - 1), nameof(context.Values.Count));
		});
	}

	[Test]
	public static void HandlePopWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new(0, 0), StackInstructionHandler.PopInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new StackInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
		});
	}

	[Test]
	public static void HandleSwap()
	{
		var cells = new List<Cell>() { new(new(0, 0), StackInstructionHandler.SwapInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new StackInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(87);
			context.Values.Push(78);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(87), nameof(context.Values.Pop));
				Assert.That(context.Values.Pop(), Is.EqualTo(78), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleSwapWithOnlyOneValueOnTheStack()
	{
		var cells = new List<Cell>() { new(new(0, 0), StackInstructionHandler.SwapInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new StackInstructionHandler(), cells, (context) =>
		{
			context.Values.Push(78);
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount + 1), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(78), nameof(context.Values.Pop));
				Assert.That(context.Values.Pop(), Is.EqualTo(0), nameof(context.Values.Pop));
			});
		});
	}

	[Test]
	public static void HandleSwapWithEmptyStack()
	{
		var cells = new List<Cell>() { new(new(0, 0), StackInstructionHandler.SwapInstruction) };

		var stackCount = 0;

		InstructionHandlerRunner.Run(new StackInstructionHandler(), cells, (context) =>
		{
			stackCount = context.Values.Count;
		}, (context, result) =>
		{
			Assert.Multiple(() =>
			{
				Assert.That(context.Values, Has.Count.EqualTo(stackCount + 2), nameof(context.Values.Count));
				Assert.That(context.Values.Pop(), Is.EqualTo(0), nameof(context.Values.Pop));
				Assert.That(context.Values.Pop(), Is.EqualTo(0), nameof(context.Values.Pop));
			});
		});
	}
}