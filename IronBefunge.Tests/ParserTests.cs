using Xunit;

namespace IronBefunge.Tests
{
	public static class ParserTests
	{
		[Fact]
		public static void GetCellsWithMultipleLinesWithNoValues()
		{
			var lines = new string[] { "    ", string.Empty, "                   " };
			var cells = new Parser(lines).Parse();
			Assert.Empty(cells);
		}

		[Fact]
		public static void GetCellsWithMultipleLinesWithValues()
		{
			var lines = new string[] { "   <   ^#:  9              ", " ^ < ", " 3 5 :" };
			var cells = new Parser(lines).Parse();
			Assert.Equal(10, cells.Length);
			Assert.Contains(new Cell(new Point(0, 3), '<'), cells);
			Assert.Contains(new Cell(new Point(0, 7), '^'), cells);
			Assert.Contains(new Cell(new Point(0, 8), '#'), cells);
			Assert.Contains(new Cell(new Point(0, 9), ':'), cells);
			Assert.Contains(new Cell(new Point(0, 12), '9'), cells);
			Assert.Contains(new Cell(new Point(1, 1), '^'), cells);
			Assert.Contains(new Cell(new Point(1, 3), '<'), cells);
			Assert.Contains(new Cell(new Point(2, 1), '3'), cells);
			Assert.Contains(new Cell(new Point(2, 3), '5'), cells);
			Assert.Contains(new Cell(new Point(2, 5), ':'), cells);
		}

		[Fact]
		public static void GetCellsWithSingleLineWithNoValues()
		{
			var lines = new string[] { "    " };
			var cells = new Parser(lines).Parse();
			Assert.Empty(cells);
		}

		[Fact]
		public static void GetCellsWithSingleLineWithValues()
		{
			var lines = new string[] { "   <   ^#:  9              " };
			var cells = new Parser(lines).Parse();
			Assert.Equal(5, cells.Length);
			Assert.Contains(new Cell(new Point(0, 3), '<'), cells);
			Assert.Contains(new Cell(new Point(0, 7), '^'), cells);
			Assert.Contains(new Cell(new Point(0, 8), '#'), cells);
			Assert.Contains(new Cell(new Point(0, 9), ':'), cells);
			Assert.Contains(new Cell(new Point(0, 12), '9'), cells);
		}

		[Fact]
		public static void ParseWithNullArray()
		{
			var cells = new Parser(null).Parse();
			Assert.Empty(cells);
		}

		[Fact]
		public static void ParseWithNullLine()
		{
			var cells = new Parser(new string[] { " ^ < ", null, " 3 5 :" }).Parse();
			Assert.Equal(5, cells.Length);
		}
	}
}
