using Spackle.Extensions;
using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers;

internal sealed class DirectionalInstructionHandler
	: InstructionHandler
{
	internal const char DownInstruction = 'v';
	internal const char JumpOverInstruction = ';';
	internal const char LeftInstruction = '<';
	internal const char RandomInstruction = '?';
	internal const char RightInstruction = '>';
	internal const char TrampolineInstruction = '#';
	internal const char UpInstruction = '^';

	internal override ImmutableArray<char> GetInstructions() =>
		[
			DirectionalInstructionHandler.DownInstruction,
			DirectionalInstructionHandler.JumpOverInstruction,
			DirectionalInstructionHandler.LeftInstruction,
			DirectionalInstructionHandler.RandomInstruction,
			DirectionalInstructionHandler.RightInstruction,
			DirectionalInstructionHandler.TrampolineInstruction,
			DirectionalInstructionHandler.UpInstruction,
		];

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
			case DirectionalInstructionHandler.JumpOverInstruction:
				// Look for the next ';' instruction in the direction we're pointed in.
				// It's possible this could wrap around to our current instruction.
				do
				{
					context.Move();
					context.Next();
				} while (context.Current.Value != DirectionalInstructionHandler.JumpOverInstruction);

				break;
			default:
				break;
		}
	}
}