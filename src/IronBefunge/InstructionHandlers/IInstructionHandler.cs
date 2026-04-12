using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers;

internal interface IInstructionHandler
{
	void Handle(ExecutionContext context);

	ImmutableArray<char> Instructions { get; }
}