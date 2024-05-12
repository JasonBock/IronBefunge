using System.Collections.Immutable;
using System.Globalization;

namespace IronBefunge.InstructionHandlers;

internal sealed class InputInstructionHandler
	: InstructionHandler
{
	internal const char AsciiInstruction = '~';
	internal const string AsciiMessage = "Please enter in a character:";
	internal const char NumericInstruction = '&';
	internal const string NumericMessage = "Please enter in a numeric value:";

	internal override ImmutableArray<char> GetInstructions() =>
		[InputInstructionHandler.AsciiInstruction, InputInstructionHandler.NumericInstruction];

	private static void HandleAscii(ExecutionContext context)
	{
		context.Writer.WriteLine(InputInstructionHandler.AsciiMessage);

		var result = context.Reader.Read();

		if (result >= 0)
		{
			context.Values.Push(result);
		}
	}

	private static void HandleNumeric(ExecutionContext context)
	{
		context.Writer.WriteLine(InputInstructionHandler.NumericMessage);

		var result = context.Reader.ReadLine();

		if (int.TryParse(result, NumberStyles.Integer, CultureInfo.CurrentCulture, out var value))
		{
			context.Values.Push(value);
		}
	}

	internal override void OnHandle(ExecutionContext context)
	{
		switch (context.Current.Value)
		{
			case InputInstructionHandler.AsciiInstruction:
				InputInstructionHandler.HandleAscii(context);
				break;
			case InputInstructionHandler.NumericInstruction:
				InputInstructionHandler.HandleNumeric(context);
				break;
		}
	}
}