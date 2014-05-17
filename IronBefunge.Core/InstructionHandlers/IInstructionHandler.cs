using System.Collections.ObjectModel;

namespace IronBefunge.Core.InstructionHandlers
{
	internal interface IInstructionHandler
	{
		void Handle(ExecutionContext context);

		ReadOnlyCollection<char> Instructions { get; }
	}
}
