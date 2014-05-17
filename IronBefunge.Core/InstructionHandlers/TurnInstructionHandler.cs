using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IronBefunge.Core.InstructionHandlers
{
	internal sealed class TurnInstructionHandler
		: InstructionHandler
	{
		internal const char LeftRightInstruction = '_';
		internal const char UpDownInstruction = '|';

		internal override ReadOnlyCollection<char> GetInstructions()
		{
			return new List<char>() { TurnInstructionHandler.LeftRightInstruction,
				TurnInstructionHandler.UpDownInstruction }.AsReadOnly();
		}

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
