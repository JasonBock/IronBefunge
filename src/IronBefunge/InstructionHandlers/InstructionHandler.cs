using System.Collections.Immutable;

namespace IronBefunge.InstructionHandlers;

internal abstract class InstructionHandler
	: IInstructionHandler
{
	private readonly Lazy<ImmutableArray<char>> instructions;

	internal InstructionHandler() =>
		this.instructions = new Lazy<ImmutableArray<char>>(this.GetInstructions);

	internal abstract ImmutableArray<char> GetInstructions();

	public void Handle(ExecutionContext context) => this.OnHandle(context);

	internal abstract void OnHandle(ExecutionContext context);

	public ImmutableArray<char> Instructions => this.instructions.Value;
}