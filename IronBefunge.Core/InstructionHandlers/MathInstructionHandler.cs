using System.Collections.Immutable;

namespace IronBefunge.Core.InstructionHandlers
{
	internal sealed class MathInstructionHandler
		: InstructionHandler
	{
		internal const char AddInstruction = '+';
		internal const char DivideInstruction = '/';
		internal const char ModInstruction = '%';
		internal const char MultiplyInstruction = '*';
		internal const char SubtractInstruction = '-';

		internal override ImmutableArray<char> GetInstructions()
		{
			return ImmutableArray.Create(MathInstructionHandler.AddInstruction,
				MathInstructionHandler.DivideInstruction, MathInstructionHandler.ModInstruction,
				MathInstructionHandler.MultiplyInstruction, MathInstructionHandler.SubtractInstruction);
		}

		private static void HandleAddition(ExecutionContext context)
		{
			context.Values.Push(context.Values.Pop() + context.Values.Pop());
		}

		private static void HandleDivision(ExecutionContext context)
		{
			var a = context.Values.Pop();
			var b = context.Values.Pop();

			context.Values.Push(a == 0 ? 0 : b / a);
		}

		private static void HandleModulo(ExecutionContext context)
		{
			var a = context.Values.Pop();
			var b = context.Values.Pop();
			context.Values.Push(a == 0 ? 0 : b % a);
		}

		private static void HandleMultiplication(ExecutionContext context)
		{
			context.Values.Push(context.Values.Pop() * context.Values.Pop());
		}

		private static void HandleSubtraction(ExecutionContext context)
		{
			var a = context.Values.Pop();
			var b = context.Values.Pop();
			context.Values.Push(b - a);
		}

		internal override void OnHandle(ExecutionContext context)
		{
			context.EnsureStack(2);

			switch (context.Current.Value)
			{
				case MathInstructionHandler.AddInstruction:
					MathInstructionHandler.HandleAddition(context);
					break;
				case MathInstructionHandler.SubtractInstruction:
					MathInstructionHandler.HandleSubtraction(context);
					break;
				case MathInstructionHandler.MultiplyInstruction:
					MathInstructionHandler.HandleMultiplication(context);
					break;
				case MathInstructionHandler.DivideInstruction:
					MathInstructionHandler.HandleDivision(context);
					break;
				case MathInstructionHandler.ModInstruction:
					MathInstructionHandler.HandleModulo(context);
					break;
			}
		}
	}
}