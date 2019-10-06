﻿using IronBefunge.InstructionHandlers;
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
		internal const char EndProgramInstruction = '@';
		internal const char NopInstruction = ' ';
		internal const char StringModeInstruction = '"';

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

		private static bool ContainsWhitespace(Cell current, Cell previous) =>
			current.Location.X == previous.Location.X ?
				Math.Abs(current.Location.Y - previous.Location.Y) > 1 :
				Math.Abs(current.Location.X - previous.Location.X) > 1;

		public void Dispose() => this.randomizer.Dispose();

		public void Execute()
		{
			if (this.cells.Length > 0)
			{
				var context = new ExecutionContext(
					new List<Cell>(this.cells), this.reader, this.writer, this.trace, this.randomizer);
				var mappings = new InstructionMapper();

				while (!(context.Current.Value == Executor.EndProgramInstruction && !context.InStringMode))
				{
					context.RunTrace();

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