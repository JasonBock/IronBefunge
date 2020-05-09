using IronBefunge.InstructionHandlers;
using Spackle;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace IronBefunge
{
	public sealed class Executor
		: IDisposable
	{
		internal const char EndInstruction = '@';
		internal const char QuitInstruction = 'q';
		internal const char NopInstruction = ' ';

		private readonly ImmutableArray<Cell> cells;
		private readonly SecureRandom randomizer;
		private readonly TextReader reader;
		private readonly TextWriter? trace;
		private readonly TextWriter writer;

		public Executor(ImmutableArray<Cell> cells, TextReader reader, TextWriter writer)
			: this(cells, reader, writer, new SecureRandom()) { }

		public Executor(ImmutableArray<Cell> cells, TextReader reader, TextWriter writer, TextWriter trace)
			: this(cells, reader, writer, new SecureRandom()) =>
				this.trace = trace ?? throw new ArgumentNullException(nameof(trace));

		public Executor(ImmutableArray<Cell> cells, TextReader reader, TextWriter writer, SecureRandom randomizer)
		{
			this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
			this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
			this.randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
			this.cells = cells;
		}

		public Executor(ImmutableArray<Cell> cells, TextReader reader, TextWriter writer, TextWriter trace, SecureRandom randomizer)
			: this(cells, reader, writer, randomizer) =>
				this.trace = trace ?? throw new ArgumentNullException(nameof(trace));

		public void Dispose() => this.randomizer.Dispose();

		public int Execute()
		{
			if (this.cells.Length > 0)
			{
				var context = new ExecutionContext(
					new List<Cell>(this.cells), this.reader, this.writer, this.trace, this.randomizer);
				var mappings = new InstructionMapper();

				while (true)
				{
					if (context.Current.Value == Executor.EndInstruction && !context.InStringMode)
					{
						break;
					}
					else if(context.Current.Value == Executor.QuitInstruction && !context.InStringMode)
					{
						context.EnsureStack(1);
						return context.Values.Pop();
					}

					context.RunTrace("Before Move");
					mappings.Handle(context);
					context.Move();
					context.Next();
					context.RunTrace("After Move");
				}
			}

			return 0;
		}
	}
}