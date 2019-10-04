using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers
{
	internal sealed class TurnInstructionHandler
		: InstructionHandler
	{
		internal const char CompareInstruction = 'w';
		internal const char LeftRightInstruction = '_';
		internal const char ReverseInstruction = 'r';
		internal const char TurnLeftInstruction = '[';
		internal const char TurnRightInstruction = ']';
		internal const char UpDownInstruction = '|';

		internal override ImmutableArray<char> GetInstructions() =>
			ImmutableArray.Create(TurnInstructionHandler.CompareInstruction, TurnInstructionHandler.LeftRightInstruction, 
				TurnInstructionHandler.ReverseInstruction, TurnInstructionHandler.TurnLeftInstruction, 
				TurnInstructionHandler.TurnRightInstruction, TurnInstructionHandler.UpDownInstruction);

		internal override void OnHandle(ExecutionContext context)
		{
			static Direction TurnLeft(Direction currentDirection) =>
				currentDirection == Direction.Right ? Direction.Up :
					currentDirection == Direction.Up ? Direction.Left :
					currentDirection == Direction.Left ? Direction.Down : Direction.Right;

			static Direction TurnRight(Direction currentDirection) =>
				currentDirection == Direction.Right ? Direction.Down :
					currentDirection == Direction.Down ? Direction.Left :
					currentDirection == Direction.Left ? Direction.Up : Direction.Right;

			switch (context.Current.Value)
			{
				case TurnInstructionHandler.CompareInstruction:
					context.EnsureStack(2);
					var b = context.Values.Pop();
					var a = context.Values.Pop();
					var compare = a.CompareTo(b);
					context.Direction = compare < 0 ? TurnLeft(context.Direction) :
						compare > 0 ? TurnRight(context.Direction) : context.Direction;
					break;
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
					context.Direction = TurnRight(context.Direction);
					break;
				case TurnInstructionHandler.TurnLeftInstruction:
					context.Direction = TurnLeft(context.Direction);
					break;
				case TurnInstructionHandler.UpDownInstruction:
					context.EnsureStack(1);
					context.Direction = context.Values.Pop() == 0 ? Direction.Down : Direction.Up;
					break;
			}
		}
	}
}