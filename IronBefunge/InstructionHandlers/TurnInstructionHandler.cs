using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers
{
	internal sealed class TurnInstructionHandler
		: InstructionHandler
	{
		internal const char LeftRightInstruction = '_';
		internal const char ReverseInstruction = 'r';
		internal const char TurnLeftInstruction = '[';
		internal const char TurnRightInstruction = ']';
		internal const char UpDownInstruction = '|';

		internal override ImmutableArray<char> GetInstructions() =>
			ImmutableArray.Create(TurnInstructionHandler.LeftRightInstruction, TurnInstructionHandler.ReverseInstruction,
				TurnInstructionHandler.TurnLeftInstruction, TurnInstructionHandler.TurnRightInstruction,
				TurnInstructionHandler.UpDownInstruction);

		internal override void OnHandle(ExecutionContext context)
		{
			switch (context.Current.Value)
			{
				case TurnInstructionHandler.LeftRightInstruction:
					context.EnsureStack(1);
					context.Direction = context.Values.Pop() == 0 ? Direction.Right : Direction.Left;
					break;
				case TurnInstructionHandler.ReverseInstruction:
					context.Direction = context.Direction == Direction.Right ? Direction.Left :
						context.Direction == Direction.Down ? Direction.Up :
						context.Direction == Direction.Left ? Direction.Right : Direction.Down;
					break;
				case TurnInstructionHandler.TurnRightInstruction:
					context.Direction = context.Direction == Direction.Right ? Direction.Down :
						context.Direction == Direction.Down ? Direction.Left :
						context.Direction == Direction.Left ? Direction.Up : Direction.Right;
					break;
				case TurnInstructionHandler.TurnLeftInstruction:
					context.Direction = context.Direction == Direction.Right ? Direction.Up :
						context.Direction == Direction.Up ? Direction.Left :
						context.Direction == Direction.Left ? Direction.Down : Direction.Right;
					break;
				case TurnInstructionHandler.UpDownInstruction:
					context.EnsureStack(1);
					context.Direction = context.Values.Pop() == 0 ? Direction.Down : Direction.Up;
					break;
			}
		}
	}
}