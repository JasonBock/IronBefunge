using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IronBefunge.Core
{
	public sealed class Parser
	{
		private readonly string[] lines;

		public Parser(string[] lines)
			: base()
		{
			this.lines = lines;
		}

		public ReadOnlyCollection<Cell> Parse()
		{
			var cells = new List<Cell>();

			if (this.lines != null)
			{
				for (var i = 0; i < this.lines.Length; i++)
				{
					var line = this.lines[i];

					if (line != null)
					{
						cells.AddRange(Parser.ProcessLine(line, i));
					}
				}
			}

			return cells.AsReadOnly();
		}

		private static ReadOnlyCollection<Cell> ProcessLine(string line, int x)
		{
			var cells = new List<Cell>();

			for (var y = 0; y < line.Length; y++)
			{
				var value = line[y];

				if (value != ' ')
				{
					cells.Add(new Cell(new Point(x, y), value));
				}
			}

			return cells.AsReadOnly();
		}
	}
}