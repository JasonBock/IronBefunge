using NUnit.Framework;

namespace IronBefunge.Tests
{
	public static class ParserTests
	{
		[Test]
		public static void GetCellsWithMultipleLinesWithNoValues()
		{
			var lines = new string[] { "    ", string.Empty, "                   " };
			var cells = new Parser(lines).Parse();
			Assert.That(cells, Is.Empty);
		}

		[Test]
		public static void GetCellsWithMultipleLinesWithValues()
		{
			var lines = new string[] { "   <   ^#:  9              ", " ^ < ", " 3 5 :" };
			var cells = new Parser(lines).Parse();

			Assert.Multiple(() =>
			{
				Assert.That(cells.Length, Is.EqualTo(10), nameof(cells.Length));
				Assert.That(cells.Contains(new Cell(new Point(0, 3), '<')), Is.True, "<");
				Assert.That(cells.Contains(new Cell(new Point(0, 7), '^')), Is.True, "^");
				Assert.That(cells.Contains(new Cell(new Point(0, 8), '#')), Is.True, "#");
				Assert.That(cells.Contains(new Cell(new Point(0, 9), ':')), Is.True, ":");
				Assert.That(cells.Contains(new Cell(new Point(0, 12), '9')), Is.True, "9");
				Assert.That(cells.Contains(new Cell(new Point(1, 1), '^')), Is.True, "^");
				Assert.That(cells.Contains(new Cell(new Point(1, 3), '<')), Is.True, "<");
				Assert.That(cells.Contains(new Cell(new Point(2, 1), '3')), Is.True, "3");
				Assert.That(cells.Contains(new Cell(new Point(2, 3), '5')), Is.True, "5");
				Assert.That(cells.Contains(new Cell(new Point(2, 5), ':')), Is.True, ":");
			});
		}

		[Test]
		public static void GetCellsWithSingleLineWithNoValues()
		{
			var lines = new string[] { "    " };
			var cells = new Parser(lines).Parse();
			Assert.That(cells, Is.Empty);
		}

		[Test]
		public static void GetCellsWithSingleLineWithValues()
		{
			var lines = new string[] { "   <   ^#:  9              " };
			var cells = new Parser(lines).Parse();

			Assert.Multiple(() =>
			{
				Assert.That(cells.Length, Is.EqualTo(5));
				Assert.That(cells.Contains(new Cell(new Point(0, 3), '<')), Is.True, "<");
				Assert.That(cells.Contains(new Cell(new Point(0, 7), '^')), Is.True, "^");
				Assert.That(cells.Contains(new Cell(new Point(0, 8), '#')), Is.True, "#");
				Assert.That(cells.Contains(new Cell(new Point(0, 9), ':')), Is.True, ":");
				Assert.That(cells.Contains(new Cell(new Point(0, 12), '9')), Is.True, "9");
			});
		}

		[Test]
		public static void ParseWithNullArray()
		{
			var cells = new Parser(null).Parse();
			Assert.That(cells, Is.Empty);
		}

		[Test]
		public static void ParseWithNullLine()
		{
			var cells = new Parser(new string?[] { " ^ < ", null, " 3 5 :" }).Parse();
			Assert.That(cells.Length, Is.EqualTo(5));
		}
	}
}