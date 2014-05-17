using System;
using System.Collections.Generic;
using System.IO;
using Spackle;

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

		internal Cell Find(Point position)
		{
			return this.Cells.Find((cell) => cell.Location == position);
		}

		internal void Move()
		{
			switch (this.Direction)
			{
				case Direction.Down:
					this.CurrentPosition = new Point(
						(this.CurrentPosition.X == int.MaxValue ? 0 : this.CurrentPosition.X + 1), this.CurrentPosition.Y);
					break;
				case Direction.Up:
					this.CurrentPosition = new Point(
						(this.CurrentPosition.X == 0 ? int.MaxValue : this.CurrentPosition.X - 1), this.CurrentPosition.Y);
					break;
				case Direction.Left:
					this.CurrentPosition = new Point(
						this.CurrentPosition.X, (this.CurrentPosition.Y == 0 ? int.MaxValue : this.CurrentPosition.Y - 1));
					break;
				case Direction.Right:
					this.CurrentPosition = new Point(
						this.CurrentPosition.X, (this.CurrentPosition.Y == int.MaxValue ? 0 : this.CurrentPosition.Y + 1));
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

		internal List<Cell> Cells { get; private set; }

		internal Cell Current { get; private set; }

		internal Point CurrentPosition { get; set; }

		internal Direction Direction { get; set; }

		internal bool InStringMode { get; set; }

		internal Cell Previous { get; private set; }

		internal SecureRandom Randomizer { get; private set; }

		internal TextReader Reader { get; private set; }

		internal Stack<int> Values { get; private set; }

		internal TextWriter Writer { get; private set; }
	}
}
