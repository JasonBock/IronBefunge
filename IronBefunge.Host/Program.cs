using System;
using System.IO;

namespace IronBefunge.Host
{
	public static class Program
	{
		public static int Main(FileInfo codeFile, bool doTrace = false)
		{
			codeFile ??= new FileInfo("99Bottles.b98");

			using var interpreter = doTrace ?
				new Interpreter(codeFile, Console.In, Console.Out, Console.Error) :
				new Interpreter(codeFile, Console.In, Console.Out);
			return interpreter.Interpret();
		}
	}
}