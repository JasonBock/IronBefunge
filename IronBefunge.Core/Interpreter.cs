using System;
using System.IO;
using Spackle;

namespace IronBefunge.Core
{
	public sealed class Interpreter
	{
		public Interpreter(string[] lines, TextReader reader, TextWriter writer)
			: base()
		{
			this.Executor = new Executor(
				new Parser(lines).Parse(), reader, writer);
		}

		public Interpreter(string[] lines, TextReader reader, TextWriter writer, SecureRandom randomizer)
			: base()
		{
			this.Executor = new Executor(
				new Parser(lines).Parse(), reader, writer, randomizer);
		}

		public Interpreter(FileInfo file, TextReader reader, TextWriter writer)
			: base()
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}

			this.Executor = new Executor(
				new Parser(File.ReadAllLines(file.FullName)).Parse(), reader, writer);
		}

		public Interpreter(FileInfo file, TextReader reader, TextWriter writer, SecureRandom randomizer)
			: base()
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}

			this.Executor = new Executor(
				new Parser(File.ReadAllLines(file.FullName)).Parse(), reader, writer, randomizer);
		}

		public void Interpret()
		{
			this.Executor.Execute();
		}

		private Executor Executor { get; set; }
	}
}