using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers;

internal sealed class LogicalInstructionHandler
	: InstructionHandler
{
	internal const char GreaterThanInstruction = '`';
	internal const char NotInstruction = '!';

	internal override ImmutableArray<char> GetInstructions() =>
		[LogicalInstructionHandler.NotInstruction, LogicalInstructionHandler.GreaterThanInstruction];

	private static void HandleGreaterThan(ExecutionContext context)
	{
		context.EnsureStack(2);
		var a = context.Values.Pop();
		var b = context.Values.Pop();

		context.Values.Push(b > a ? 1 : 0);
	}

	private static void HandleNot(ExecutionContext context)
	{
		context.EnsureStack(1);
		context.Values.Push(context.Values.Pop() == 0 ? 1 : 0);
	}

	internal override void OnHandle(ExecutionContext context)
	{
		switch (context.Current.Value)
		{
			case LogicalInstructionHandler.GreaterThanInstruction:
				LogicalInstructionHandler.HandleGreaterThan(context);
				break;
			case LogicalInstructionHandler.NotInstruction:
				LogicalInstructionHandler.HandleNot(context);
				break;
			default:
				break;
		}
	}
}