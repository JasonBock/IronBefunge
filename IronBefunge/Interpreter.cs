using System;
using System.IO;
using Spackle;

namespace IronBefunge
{
	public sealed class Interpreter
		: IDisposable
	{
		public const string FileExtension = ".b98";

		private readonly Executor executor;

		public Interpreter(string[] lines, TextReader reader, TextWriter writer)
			: base() =>
			this.executor = new Executor(new Parser(lines).Parse(), reader, writer);

		public Interpreter(string[] lines, TextReader reader, TextWriter writer, SecureRandom randomizer)
			: base() =>
			this.executor = new Executor(new Parser(lines).Parse(), reader, writer, randomizer);

		public Interpreter(FileInfo file, TextReader reader, TextWriter writer)
			: base()
		{
			if (file is null)
			{
				throw new ArgumentNullException(nameof(file));
			}

			if(file.Extension != Interpreter.FileExtension)
			{
				throw new ArgumentException($"The file extension should be {Interpreter.FileExtension}; it is {file.Extension}", nameof(file));
			}

			this.executor = new Executor(
				new Parser(File.ReadAllLines(file.FullName)).Parse(), reader, writer);
		}

		public Interpreter(FileInfo file, TextReader reader, TextWriter writer, SecureRandom randomizer)
			: base()
		{
			if (file is null)
			{
				throw new ArgumentNullException(nameof(file));
			}

			if (file.Extension != Interpreter.FileExtension)
			{
				throw new ArgumentException($"The file extension should be {Interpreter.FileExtension}; it is {file.Extension}", nameof(file));
			}

			this.executor = new Executor(
				new Parser(File.ReadAllLines(file.FullName)).Parse(), reader, writer, randomizer);
		}

		public void Dispose() => this.executor.Dispose();

		public void Interpret() => this.executor.Execute();
	}
}