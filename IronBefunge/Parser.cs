using System.Collections.Immutable;

namespace IronBefunge;

public sealed class Parser
{
	private readonly string?[]? lines;

	public Parser(string?[]? lines)
		: base() => this.lines = lines;

	public ImmutableArray<Cell> Parse()
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

		return cells.ToImmutableArray();
	}

	private static ImmutableArray<Cell> ProcessLine(string line, int y)
	{
		var cells = new List<Cell>();

		for (var x = 0; x < line.Length; x++)
		{
			var value = line[x];

			if (value != ' ')
			{
				cells.Add(new(new(x, y), value));
			}
		}

		return cells.ToImmutableArray();
	}
}