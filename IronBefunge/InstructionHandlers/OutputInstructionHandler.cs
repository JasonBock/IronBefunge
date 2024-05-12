using System.Collections.Immutable;
using System.Globalization;

namespace IronBefunge.InstructionHandlers;

internal sealed class OutputInstructionHandler
	: InstructionHandler
{
	internal const char AsciiInstruction = ',';
	internal const char NumericInstruction = '.';

	internal override ImmutableArray<char> GetInstructions() =>
		[OutputInstructionHandler.AsciiInstruction, OutputInstructionHandler.NumericInstruction];

	internal override void OnHandle(ExecutionContext context)
	{
		context.EnsureStack(1);

		switch (context.Current.Value)
		{
			case OutputInstructionHandler.AsciiInstruction:
				context.Writer.Write(Convert.ToChar(
					context.Values.Pop()));
				break;
			case OutputInstructionHandler.NumericInstruction:
				context.Writer.Write(
					context.Values.Pop().ToString(CultureInfo.CurrentCulture));
				break;
		}
	}
}