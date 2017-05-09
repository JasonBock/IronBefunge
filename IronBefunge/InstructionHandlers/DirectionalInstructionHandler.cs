using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers
{
	internal sealed class DirectionalInstructionHandler
		: InstructionHandler
	{
		internal const char DownInstruction = 'v';
		internal const char LeftInstruction = '<';
		internal const char RandomInstruction = '?';
		internal const char RightInstruction = '>';
		internal const char TrampolineInstruction = '#';
		internal const char UpInstruction = '^';

		internal override ImmutableArray<char> GetInstructions() =>
			ImmutableArray.Create(DirectionalInstructionHandler.DownInstruction,
				DirectionalInstructionHandler.LeftInstruction, DirectionalInstructionHandler.RandomInstruction,
				DirectionalInstructionHandler.RightInstruction, DirectionalInstructionHandler.TrampolineInstruction,
				DirectionalInstructionHandler.UpInstruction);

		internal override void OnHandle(ExecutionContext context)
		{
			switch (context.Current.Value)
			{
				case DirectionalInstructionHandler.RightInstruction:
					context.Direction = Direction.Right;
					break;
				case DirectionalInstructionHandler.LeftInstruction:
					context.Direction = Direction.Left;
					break;
				case DirectionalInstructionHandler.UpInstruction:
					context.Direction = Direction.Up;
					break;
				case DirectionalInstructionHandler.DownInstruction:
					context.Direction = Direction.Down;
					break;
				case DirectionalInstructionHandler.RandomInstruction:
					context.Direction = (Direction)context.Randomizer.Next(4);
					break;
				case DirectionalInstructionHandler.TrampolineInstruction:
					context.Move();
					break;
			}
		}
	}
}