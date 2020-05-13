﻿using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers
{
	internal sealed class NopInstructionHandler
		: InstructionHandler
	{
		internal const char NopInstruction = 'z';

		internal override ImmutableArray<char> GetInstructions() =>
			ImmutableArray.Create(NopInstructionHandler.NopInstruction);

		internal override void OnHandle(ExecutionContext context) { }
	}
}