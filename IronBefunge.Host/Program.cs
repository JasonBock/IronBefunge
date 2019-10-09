using System;
using System.IO;

namespace IronBefunge.Host
{
	class Program
	{
		static void Main(FileInfo codeFile, bool doTrace = false)
		{
			codeFile ??= new FileInfo("100DigitsOfPi.b98");

			using (var interpreter = doTrace ?
				new Interpreter(codeFile, Console.In, Console.Out, Console.Error) :
				new Interpreter(codeFile, Console.In, Console.Out))
			{
				interpreter.Interpret();
			}

			Console.Out.WriteLine();
			Console.Out.WriteLine("Press any key to continue...");
			Console.In.ReadLine();
		}
	}
}
