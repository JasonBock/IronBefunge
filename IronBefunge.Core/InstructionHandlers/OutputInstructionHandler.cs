using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace IronBefunge.Core.InstructionHandlers
{
	internal sealed class OutputInstructionHandler
		: InstructionHandler
	{
		internal const char AsciiInstruction = ',';
		internal const char NumericInstruction = '.';

		internal override ReadOnlyCollection<char> GetInstructions()
		{
			return new List<char>() { OutputInstructionHandler.AsciiInstruction, 
				OutputInstructionHandler.NumericInstruction }.AsReadOnly();
		}

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
}
