using System.Collections.Immutable;

namespace IronBefunge.Core.InstructionHandlers
{
	internal interface IInstructionHandler
	{
		void Handle(ExecutionContext context);

		ImmutableArray<char> Instructions { get; }
	}
}
