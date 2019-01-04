using System;
using System.IO;

namespace IronBefunge.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var interpreter = new Interpreter(new FileInfo(args[0]), Console.In, Console.Out))
			{
				interpreter.Interpret();
			}

			Console.Out.WriteLine();
			Console.Out.WriteLine("Press any key to continue...");
			Console.In.ReadLine();
		}
	}
}
