using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers;

internal sealed class StackInstructionHandler
	: InstructionHandler
{
	internal const char ClearInstruction = 'n';
	internal const char DuplicateInstruction = ':';
	internal const char PopInstruction = '$';
	internal const char SwapInstruction = '\\';

	internal override ImmutableArray<char> GetInstructions() =>
		ImmutableArray.Create(StackInstructionHandler.ClearInstruction,
			StackInstructionHandler.DuplicateInstruction,
			StackInstructionHandler.PopInstruction,
			StackInstructionHandler.SwapInstruction);

	private static void HandleClear(ExecutionContext context) => context.Values.Clear();

	private static void HandleDuplicate(ExecutionContext context)
	{
		context.EnsureStack(1);
		context.Values.Push(context.Values.Peek());
	}

	private static void HandlePop(ExecutionContext context)
	{
		if (context.Values.Count > 0)
		{
			context.Values.Pop();
		}
	}

	private static void HandleSwap(ExecutionContext context)
	{
		context.EnsureStack(2);

		var a = context.Values.Pop();
		var b = context.Values.Pop();

		context.Values.Push(a);
		context.Values.Push(b);
	}

	internal override void OnHandle(ExecutionContext context)
	{
		switch (context.Current.Value)
		{
			case StackInstructionHandler.ClearInstruction:
				StackInstructionHandler.HandleClear(context);
				break;
			case StackInstructionHandler.DuplicateInstruction:
				StackInstructionHandler.HandleDuplicate(context);
				break;
			case StackInstructionHandler.PopInstruction:
				StackInstructionHandler.HandlePop(context);
				break;
			case StackInstructionHandler.SwapInstruction:
				StackInstructionHandler.HandleSwap(context);
				break;
		}
	}
}