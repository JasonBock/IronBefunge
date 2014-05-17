using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IronBefunge.Core
{
	public sealed class Parser
	{
		private const string ErrorNullLine = "The line at row {0} is null.";

		public Parser(string[] lines)
			: base()
		{
			this.Lines = lines;
		}

		public ReadOnlyCollection<Cell> Parse()
		{
			var cells = new List<Cell>();

			if (this.Lines != null)
			{
				for (var i = 0; i < this.Lines.Length; i++)
				{
					var line = this.Lines[i];

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

		private string[] Lines { get; set; }
	}
}
