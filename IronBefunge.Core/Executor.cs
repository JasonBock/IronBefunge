using IronBefunge.Core.InstructionHandlers;
using Spackle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace IronBefunge.Core
{
	public sealed class Executor
		: IDisposable
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public Executor(ReadOnlyCollection<Cell> cells, TextReader reader, TextWriter writer)
			: this(cells, reader, writer, new SecureRandom()) { }

		public Executor(ReadOnlyCollection<Cell> cells, TextReader reader, TextWriter writer, SecureRandom randomizer)
		{
			if (cells == null)
			{
				throw new ArgumentNullException(nameof(cells));
			}

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

			this.Cells = cells;
			this.Reader = reader;
			this.Writer = writer;
			this.Randomizer = randomizer;
		}

		private static bool ContainsWhitespace(Cell current, Cell previous)
		{
			return (current.Location.X == previous.Location.X ?
				Math.Abs(current.Location.Y - previous.Location.Y) > 1 :
				Math.Abs(current.Location.X - previous.Location.X) > 1);
		}

		public void Dispose()
		{
			this.Randomizer.Dispose();
		}

		public void Execute()
		{
			if (this.Cells.Count > 0)
			{
				var context = new ExecutionContext(
					new List<Cell>(this.Cells), this.Reader, this.Writer, this.Randomizer);
				var mappings = new InstructionMapper();

				while (context.Current.Value != '@')
				{
					if (context.Current.Value == '"')
					{
						context.InStringMode = !context.InStringMode;
					}
					else
					{
						if (context.InStringMode)
						{
							if (Executor.ContainsWhitespace(context.Current, context.Previous))
							{
								context.Values.Push(' ');
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

		private ReadOnlyCollection<Cell> Cells { get; }

		private SecureRandom Randomizer { get; }

		private TextReader Reader { get; }

		private TextWriter Writer { get; }
	}
}
