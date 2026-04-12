namespace IronBefunge.Host;

public static class Program
{
	public static int Main(FileInfo codeFile, bool doTrace = false)
	{
		codeFile ??= new FileInfo("Collatz.b98");

		var interpreter = doTrace ?
			new Interpreter(codeFile, Console.In, Console.Out, Console.Error) :
			new Interpreter(codeFile, Console.In, Console.Out);
		return interpreter.Interpret();
	}
}