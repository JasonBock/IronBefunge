using System;
using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers
{
	internal sealed class StringInstructionHandler
		: InstructionHandler
	{
		internal const char FetchCharacterInstruction = '\'';
		internal const char StringModeInstruction = '"';

		internal override ImmutableArray<char> GetInstructions() =>
			ImmutableArray.Create(StringInstructionHandler.FetchCharacterInstruction,
				StringInstructionHandler.StringModeInstruction);

		internal override void OnHandle(ExecutionContext context)
		{
			switch (context.Current.Value)
			{
				case StringInstructionHandler.StringModeInstruction:
					while (true)
					{
						context.Move();
						context.Next();

						var whitespaceCount = StringInstructionHandler.ContainsWhitespace(context.Current, context.Previous);

						for (var i = 0; i < whitespaceCount; i++)
						{
							context.Values.Push(Executor.NopInstruction);
						}

						if(context.Current.Value == StringInstructionHandler.StringModeInstruction)
						{
							break;
						}
						else
						{
							context.Values.Push(context.Current.Value);
						}
					}
					break;
				case StringInstructionHandler.FetchCharacterInstruction:
					context.Move();
					context.Next();
					context.Values.Push(context.Current.Value);
					break;
			}
		}

		private static int ContainsWhitespace(Cell current, Cell previous) =>
			current.Location.Y == previous.Location.Y ?
				Math.Abs(current.Location.X - previous.Location.X) - 1 :
				Math.Abs(current.Location.Y - previous.Location.Y) - 1;
	}
}