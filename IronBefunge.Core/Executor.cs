using IronBefunge.Core.InstructionHandlers;
using Spackle;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace IronBefunge.Core
{
	public sealed class Executor
		: IDisposable
	{
		internal const char EndProgramInstruction = '@';
		internal const char NopInstruction = ' ';
		internal const char StringModeInstruction = '"';

		private readonly ImmutableArray<Cell> cells;
		private readonly SecureRandom randomizer;
		private readonly TextReader reader;
		private readonly TextWriter writer;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public Executor(ImmutableArray<Cell> cells, TextReader reader, TextWriter writer)
			: this(cells, reader, writer, new SecureRandom()) { }

		public Executor(ImmutableArray<Cell> cells, TextReader reader, TextWriter writer, SecureRandom randomizer)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			if (writer == null)
			{
				throw new ArgumentNullException(nameof(writer));
			}

			if (randomizer == null)
			{
				throw new ArgumentNullException(nameof(randomizer));
			}

			this.cells = cells;
			this.reader = reader;
			this.writer = writer;
			this.randomizer = randomizer;
		}

		private static bool ContainsWhitespace(Cell current, Cell previous)
		{
			return (current.Location.X == previous.Location.X ?
				Math.Abs(current.Location.Y - previous.Location.Y) > 1 :
				Math.Abs(current.Location.X - previous.Location.X) > 1);
		}

		public void Dispose()
		{
			this.randomizer.Dispose();
		}

		public void Execute()
		{
			if (this.cells.Length > 0)
			{
				var context = new ExecutionContext(
					new List<Cell>(this.cells), this.reader, this.writer, this.randomizer);
				var mappings = new InstructionMapper();

				while (!(context.Current.Value == Executor.EndProgramInstruction && !context.InStringMode))
				{
					if (context.Current.Value == Executor.StringModeInstruction)
					{
						context.InStringMode = !context.InStringMode;
					}
					else
					{
						if (context.InStringMode)
						{
							if (Executor.ContainsWhitespace(context.Current, context.Previous))
							{
								context.Values.Push(Executor.NopInstruction);
							}

							context.Values.Push(context.Current.Value);
						}
						else
						{
							mappings.Handle(context);
						}
					}

					context.Move();
					context.Next();
				}
			}
		}
	}
}