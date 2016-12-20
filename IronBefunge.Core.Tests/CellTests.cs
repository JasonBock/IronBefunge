using System;
using Xunit;

namespace IronBefunge.Core.Tests
{
	public static class CellTests
	{
		[Fact]
		public static void CheckForEquality()
		{
			var c1 = new Cell(new Point(1, 1), 'c');
			var c2 = new Cell(new Point(2, 2), 'd');
			var c3 = new Cell(new Point(1, 1), 'd');
			var c4 = new Cell(new Point(1, 1), 'c');

			Assert.NotEqual<Cell>(c1, c2);
			Assert.NotEqual<Cell>(c1, c3);
			Assert.Equal<Cell>(c1, c4);
			Assert.NotEqual<Cell>(c2, c3);
			Assert.NotEqual<Cell>(c2, c4);
			Assert.NotEqual<Cell>(c3, c4);
		}

		[Fact]
		public static void CheckForEqualityViaOperators()
		{
			var c1 = new Cell(new Point(1, 1), 'c');
			var c2 = new Cell(new Point(2, 2), 'd');
			var c3 = new Cell(new Point(1, 1), 'd');
			var c4 = new Cell(new Point(1, 1), 'c');

#pragma warning disable 1718
			Assert.True(c1 == c1);
#pragma warning restore 1718
			Assert.True(c1 != c2);
			Assert.True(c1 != c3);
			Assert.True(c1 == c4);
		}

		[Fact]
		public static void CheckForEqualityWithIncompatibleTypes()
		{
			var c = new Cell(new Point(1, 1), 'c');
			var g = Guid.NewGuid();

			Assert.NotEqual<object>(c, g);
		}

		[Fact]
		public static void Create()
		{
			const int x = 3;
			const int y = 20;
			const char value = '^';

			var cell = new Cell(new Point(x, y), value);
			Assert.Equal(x, cell.Location.X);
			Assert.Equal(y, cell.Location.Y);
			Assert.Equal(value, cell.Value);
		}
	}
}
