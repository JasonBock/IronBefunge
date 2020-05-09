using System;
using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers
{
	internal sealed class StringInstructionHandlers
		: InstructionHandler
	{
		internal const char FetchCharacterInstruction = '\'';
		internal const char StringModeInstruction = '"';

		internal override ImmutableArray<char> GetInstructions() =>
			ImmutableArray.Create(StringInstructionHandlers.FetchCharacterInstruction,
				StringInstructionHandlers.StringModeInstruction);

		internal override void OnHandle(ExecutionContext context)
		{
			switch (context.Current.Value)
			{
				case StringInstructionHandlers.StringModeInstruction:
					while (true)
					{
						context.Move();
						context.Next();

						if (StringInstructionHandlers.ContainsWhitespace(context.Current, context.Previous))
						{
							context.Values.Push(Executor.NopInstruction);
						}

						if(context.Current.Value == StringInstructionHandlers.StringModeInstruction)
						{
							break;
						}
						else
						{
							context.Values.Push(context.Current.Value);
						}
					}
					break;
				case StringInstructionHandlers.FetchCharacterInstruction:
					break;
			}
		}

		private static bool ContainsWhitespace(Cell current, Cell previous) =>
			current.Location.Y == previous.Location.Y ?
				Math.Abs(current.Location.X - previous.Location.X) > 1 :
				Math.Abs(current.Location.Y - previous.Location.Y) > 1;
	}
}