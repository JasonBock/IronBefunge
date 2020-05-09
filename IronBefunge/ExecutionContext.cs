using System;
using System.Collections.Generic;
using System.IO;
using Spackle;
using System.Linq;

namespace IronBefunge
{
	internal sealed class ExecutionContext
	{
		// The reason this is disabled around the constructor is the compiler isn't 
		// "smart enough" to realize Current is assigned in the constructor through Next()
		// and will "never" be null
#nullable disable
		internal ExecutionContext(List<Cell> cells, TextReader reader, TextWriter writer, TextWriter trace, SecureRandom randomizer)
			: base()
		{
			this.Cells = cells;
			this.CurrentPosition = new Point();
			this.Direction = Direction.Right;
			this.InStringMode = false;
			this.Randomizer = randomizer;
			this.Values = new Stack<int>();
			this.Reader = reader;
			this.Writer = writer;
			this.Trace = trace;

			this.Next();
		}
#nullable enable

		internal void EnsureStack(int count)
		{
			if (this.Values.Count < count)
			{
				var neededValues = count - this.Values.Count;

				for (var i = 0; i < neededValues; i++)
				{
					this.Values.Push(0);
				}
			}
		}

		internal Cell Find(Point position) =>
			this.Cells.Find((cell) => cell.Location == position);

		internal void Move()
		{
			var maxX = this.Cells.Max(_ => _.Location.X);
			var maxY = this.Cells.Max(_ => _.Location.Y);

			this.CurrentPosition = this.Direction switch
			{
				Direction.Down => new Point(
					this.CurrentPosition.X, this.CurrentPosition.Y == maxY ? 0 : this.CurrentPosition.Y + 1),
				Direction.Up => new Point(
					this.CurrentPosition.X, this.CurrentPosition.Y == 0 ? maxY : this.CurrentPosition.Y - 1),
				Direction.Left => new Point(
					this.CurrentPosition.X == 0 ? maxX : this.CurrentPosition.X - 1, this.CurrentPosition.Y),
				Direction.Right => new Point(
					this.CurrentPosition.X == maxX ? 0 : this.CurrentPosition.X + 1, this.CurrentPosition.Y),
				_ => throw new NotSupportedException(),
			};
		}

		internal void Next()
		{
			this.Previous = this.Current;

			var next = this.Find(this.CurrentPosition);

			while (next is null)
			{
				this.Move();
				next = this.Find(this.CurrentPosition);
			}

			this.Current = next;
		}
		
		internal void RunTrace(string message)
		{
			if (this.Trace is { })
			{
				this.Trace.WriteLine($"{nameof(message)} = {message}");
				this.Trace.WriteLine($"{nameof(this.Current)} = {this.Current}");
				this.Trace.WriteLine($"{nameof(this.Direction)} = {this.Direction}");
				this.Trace.WriteLine($"{nameof(this.InStringMode)} = {this.InStringMode}");
				this.Trace.WriteLine($"\t{nameof(this.Values)} = {string.Join(", ", this.Values)}");
			}
		}

		internal List<Cell> Cells { get; }

		internal Cell Current { get; private set; }

		internal Point CurrentPosition { get; set; }

		internal Direction Direction { get; set; }

		internal bool InStringMode { get; set; }

		internal Cell Previous { get; private set; }

		internal SecureRandom Randomizer { get; }

		internal TextReader Reader { get; }

		internal TextWriter Trace { get; }

		internal Stack<int> Values { get; }

		internal TextWriter Writer { get; }
	}
}