using System;
using System.Collections.Generic;
using System.IO;
using Spackle;
using System.Linq;

namespace IronBefunge.Core
{
	internal sealed class ExecutionContext
	{
		internal ExecutionContext(List<Cell> cells, TextReader reader, TextWriter writer, SecureRandom randomizer)
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

			this.Next();
		}

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
			// TODO: I should consider doing this calcuation in the constructor and then only if
			// a cell was added. Unfortunately the context doesn't get to know that right now, so
			// I have to add then. Until then, this is the "safest" (though not performant) way
			// to get the max values.
			var maxX = this.Cells.Max(_ => _.Location.X);
			var maxY = this.Cells.Max(_ => _.Location.Y);

			switch (this.Direction)
			{
				case Direction.Down:
					this.CurrentPosition = new Point(
						(this.CurrentPosition.X == maxX ? 0 : this.CurrentPosition.X + 1), this.CurrentPosition.Y);
					break;
				case Direction.Up:
					this.CurrentPosition = new Point(
						(this.CurrentPosition.X == 0 ? maxX : this.CurrentPosition.X - 1), this.CurrentPosition.Y);
					break;
				case Direction.Left:
					this.CurrentPosition = new Point(
						this.CurrentPosition.X, (this.CurrentPosition.Y == 0 ? maxY : this.CurrentPosition.Y - 1));
					break;
				case Direction.Right:
					this.CurrentPosition = new Point(
						this.CurrentPosition.X, (this.CurrentPosition.Y == maxY ? 0 : this.CurrentPosition.Y + 1));
					break;
				default:
					throw new NotSupportedException();
			}
		}

		internal void Next()
		{
			this.Previous = this.Current;

			var next = this.Find(this.CurrentPosition);

			while (next == null)
			{
				this.Move();
				next = this.Find(this.CurrentPosition);
			}

			this.Current = next;
		}

		internal List<Cell> Cells { get; }

		internal Cell Current { get; private set; }

		internal Point CurrentPosition { get; set; }

		internal Direction Direction { get; set; }

		internal bool InStringMode { get; set; }

		internal Cell Previous { get; private set; }

		internal SecureRandom Randomizer { get; }

		internal TextReader Reader { get; }

		internal Stack<int> Values { get; }

		internal TextWriter Writer { get; }
	}
}