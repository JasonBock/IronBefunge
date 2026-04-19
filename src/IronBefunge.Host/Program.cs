using IronBefunge;
using System.CommandLine;

var codeFileOption = new Option<FileInfo>("--codeFile")
{
	Description = "The Befunge code file",
	DefaultValueFactory = parseResult => new FileInfo("Collatz.b98")
};

var traceOption = new Option<bool>("--trace")
{
	Description = "An optional flag to specify if tracing should occur",
	DefaultValueFactory = parseResult => false
};

var rootCommand = new RootCommand("IronBefunge host");
rootCommand.Options.Add(codeFileOption);
rootCommand.Options.Add(traceOption);
rootCommand.SetAction(parseResult =>
{
	var codeFile = parseResult.GetValue(codeFileOption)!;
	var trace = parseResult.GetValue(traceOption);

	var interpreter = trace ?
		new Interpreter(codeFile, Console.In, Console.Out, Console.Error) :
		new Interpreter(codeFile, Console.In, Console.Out);
	return interpreter.Interpret();
});

var parseResult = rootCommand.Parse(args);
return parseResult.Invoke();