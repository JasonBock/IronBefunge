using System.Collections.Immutable;

namespace IronBefunge.Core.InstructionHandlers
{
	internal sealed class TurnInstructionHandler
		: InstructionHandler
	{
		internal const char LeftRightInstruction = '_';
		internal const char UpDownInstruction = '|';

		internal override ImmutableArray<char> GetInstructions() =>
			ImmutableArray.Create(TurnInstructionHandler.LeftRightInstruction,
				TurnInstructionHandler.UpDownInstruction);

		internal override void OnHandle(ExecutionContext context)
		{
			context.EnsureStack(1);

			switch (context.Current.Value)
			{
				case TurnInstructionHandler.LeftRightInstruction:
					context.Direction = context.Values.Pop() == 0 ? Direction.Right : Direction.Left;
					break;
				case TurnInstructionHandler.UpDownInstruction:
					context.Direction = context.Values.Pop() == 0 ? Direction.Down : Direction.Up;
					break;
			}
		}
	}
}