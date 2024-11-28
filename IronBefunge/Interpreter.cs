using Spackle;

namespace IronBefunge;

public sealed class Interpreter
{
	public const string FileExtension = ".b98";

	private readonly Executor executor;

	public Interpreter(string[] lines, TextReader reader, TextWriter writer)
		: base() =>
		this.executor = new Executor(new Parser(lines).Parse(), reader, writer);

	public Interpreter(string[] lines, TextReader reader, TextWriter writer, TextWriter trace)
		: base() =>
		this.executor = new Executor(new Parser(lines).Parse(), reader, writer, trace);

	public Interpreter(string[] lines, TextReader reader, TextWriter writer, SecureRandom randomizer)
		: base() =>
		this.executor = new Executor(new Parser(lines).Parse(), reader, writer, randomizer);

	public Interpreter(string[] lines, TextReader reader, TextWriter writer, TextWriter trace, SecureRandom randomizer)
		: base() =>
		this.executor = new Executor(new Parser(lines).Parse(), reader, writer, trace, randomizer);

	public Interpreter(FileInfo file, TextReader reader, TextWriter writer)
		: base()
	{
		Interpreter.VerifyFile(file);
		this.executor = new Executor(
			new Parser(File.ReadAllLines(file.FullName)).Parse(), reader, writer);
	}

	public Interpreter(FileInfo file, TextReader reader, TextWriter writer, SecureRandom randomizer)
		: base()
	{
		Interpreter.VerifyFile(file);
		this.executor = new Executor(
			new Parser(File.ReadAllLines(file.FullName)).Parse(), reader, writer, randomizer);
	}

	public Interpreter(FileInfo file, TextReader reader, TextWriter writer, TextWriter trace)
		: base()
	{
		Interpreter.VerifyFile(file);
		this.executor = new Executor(
			new Parser(File.ReadAllLines(file.FullName)).Parse(), reader, writer, trace);
	}

	public Interpreter(FileInfo file, TextReader reader, TextWriter writer, TextWriter trace, SecureRandom randomizer)
		: base()
	{
		Interpreter.VerifyFile(file);
		this.executor = new Executor(
			new Parser(File.ReadAllLines(file.FullName)).Parse(), reader, writer, trace, randomizer);
	}

	private static void VerifyFile(FileInfo file)
	{
		ArgumentNullException.ThrowIfNull(file);

		if (file.Extension != Interpreter.FileExtension)
		{
			throw new ArgumentException($"The file extension should be {Interpreter.FileExtension}; it is {file.Extension}", nameof(file));
		}
	}

	public int Interpret() => this.executor.Execute();
}