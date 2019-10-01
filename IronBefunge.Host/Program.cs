using System;
using System.IO;

namespace IronBefunge.Host
{
	class Program
	{
		static void Main(FileInfo codeFile)
		{
			codeFile ??= new FileInfo("HelloWithRandom.b98");

			using (var interpreter = new Interpreter(codeFile, Console.In, Console.Out))
			{
				interpreter.Interpret();
			}

			Console.Out.WriteLine();
			Console.Out.WriteLine("Press any key to continue...");
			Console.In.ReadLine();
		}
	}
}
