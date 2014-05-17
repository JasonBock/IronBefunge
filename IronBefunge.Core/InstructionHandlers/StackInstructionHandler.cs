using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IronBefunge.Core.InstructionHandlers
{
	internal sealed class StackInstructionHandler
		: InstructionHandler
	{
		internal const char DuplicateInstruction = ':';
		internal const char PopInstruction = '$';
		internal const char SwapInstruction = '\\';

		internal override ReadOnlyCollection<char> GetInstructions()
		{
			return new List<char>() { StackInstructionHandler.DuplicateInstruction, 
				StackInstructionHandler.PopInstruction, 
				StackInstructionHandler.SwapInstruction }.AsReadOnly();
		}

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
}
