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
				// Look for the next ';' instruction in the direction
				// we're currently going. Look "both ways" - that is,
				// "after" the current position, and then "wrap around".
				// If we do not find another ';', we consider it to be
				// a no-op and it does nothing.
				Cell? nextCell = null;

				switch (context.Direction)
				{
					case Direction.Right:
						nextCell = context.Cells
							.Where(cell => cell.Location.Y == context.CurrentPosition.Y && cell.Location.X > context.CurrentPosition.X && cell.Value == DirectionalInstructionHandler.JumpOverInstruction)
							.OrderBy(cell => cell.Location.X)
							.FirstOrDefault();

						if (nextCell is null)
						{
							nextCell = context.Cells
								.Where(cell => cell.Location.Y == context.CurrentPosition.Y && cell.Location.X < context.CurrentPosition.X && cell.Value == DirectionalInstructionHandler.JumpOverInstruction)
								.OrderByDescending(cell => cell.Location.X)
								.FirstOrDefault();
						}
						break;
					case Direction.Left:
						nextCell = context.Cells
							.Where(cell => cell.Location.Y == context.CurrentPosition.Y && cell.Location.X < context.CurrentPosition.X && cell.Value == DirectionalInstructionHandler.JumpOverInstruction)
							.OrderByDescending(cell => cell.Location.X)
							.FirstOrDefault();

						if (nextCell is null)
						{
							nextCell = context.Cells
								.Where(cell => cell.Location.Y == context.CurrentPosition.Y && cell.Location.X > context.CurrentPosition.X && cell.Value == DirectionalInstructionHandler.JumpOverInstruction)
								.OrderBy(cell => cell.Location.X)
								.FirstOrDefault();
						}
						break;
				}

				if (nextCell is not null)
				{
					// Set the current location this cell, and then "move" again.
				}
				else
				{
					context.Move();
				}

				break;
			default:
				break;
		}
	}
}