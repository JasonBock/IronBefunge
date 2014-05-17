using System;
using System.IO;
using IronBefunge.Core;

namespace IronBefunge
{
	class Program
	{
		static void Main(string[] args)
		{
			new Interpreter(new FileInfo(args[0]), Console.In, Console.Out).Interpret();
			Console.Out.WriteLine();
			Console.Out.WriteLine("Press any key to continue...");
			Console.In.ReadLine();
		}
	}
}
