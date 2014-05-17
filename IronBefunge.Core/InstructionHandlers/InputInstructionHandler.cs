using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace IronBefunge.Core.InstructionHandlers
{
	internal sealed class InputInstructionHandler
		: InstructionHandler
	{
		internal const char AsciiInstruction = '~';
		internal const string AsciiMessage = "Please enter in a character:";
		internal const char NumericInstruction = '&';
		internal const string NumericMessage = "Please enter in a numeric value:";

		internal override ReadOnlyCollection<char> GetInstructions()
		{
			return new List<char>() { InputInstructionHandler.AsciiInstruction, 
				InputInstructionHandler.NumericInstruction }.AsReadOnly();
		}

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

			var value = 0;

			if (int.TryParse(result, NumberStyles.Integer, CultureInfo.CurrentCulture, out value))
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
}
