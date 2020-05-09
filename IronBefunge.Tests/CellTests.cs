using System;
using NUnit.Framework;

namespace IronBefunge.Tests
{
	public static class CellTests
	{
		[Test]
		public static void CheckForEqualityWithSpecificType()
		{
			var c1 = new Cell(new Point(1, 1), 'c');
			var c2 = new Cell(new Point(2, 2), 'd');
			var c3 = new Cell(new Point(1, 1), 'd');
			var c4 = new Cell(new Point(1, 1), 'c');

			Assert.Multiple(() =>
			{
				Assert.That(c1.Equals(c2), Is.False, "!c1.Equals(c2)");
				Assert.That(c1.Equals(c3), Is.False, "!c1.Equals(c3)");
				Assert.That(c1.Equals(c4), Is.True, "c1.Equals(c4)");
				Assert.That(c2.Equals(c3), Is.False, "!c2.Equals(c3)");
				Assert.That(c2.Equals(c4), Is.False, "!c2.Equals(c4)");
				Assert.That(c3.Equals(c4), Is.False, "!c3.Equals(c4)");
			});
		}

		[Test]
		public static void CheckForEqualityWithObjectType()
		{
			var c1 = new Cell(new Point(1, 1), 'c');
			var c2 = new Cell(new Point(1, 1), 'c');

			Assert.That(c1.Equals((object)c2), Is.True, "c1.Equals(c4)");
		}

		[Test]
		public static void CheckForEqualityViaOperators()
		{
			var c1 = new Cell(new Point(1, 1), 'c');
			var c2 = new Cell(new Point(2, 2), 'd');
			var c3 = new Cell(new Point(1, 1), 'd');
			var c4 = new Cell(new Point(1, 1), 'c');

			Assert.Multiple(() =>
			{
#pragma warning disable 1718
				Assert.That(c1 == c1, Is.True, "c1 == c1");
#pragma warning restore 1718
				Assert.That(c1 != c2, Is.True, "c1 != c2");
				Assert.That(c1 != c3, Is.True, "c1 != c3");
				Assert.That(c1 == c4, Is.True, "c1 == c4");
			});
		}

		[Test]
		public static void CheckForEqualityWithIncompatibleTypes()
		{
			var c = new Cell(new Point(1, 1), 'c');
			var g = Guid.NewGuid();

			Assert.That(c, Is.Not.EqualTo(g));
		}

		[Test]
		public static void GetHashCodes()
		{
			var c1 = new Cell(new Point(1, 1), 'c');
			var c2 = new Cell(new Point(2, 2), 'd');
			var c3 = new Cell(new Point(1, 1), 'd');
			var c4 = new Cell(new Point(1, 1), 'c');

			Assert.Multiple(() =>
			{
				Assert.That(c1.GetHashCode(), Is.Not.EqualTo(c2.GetHashCode()), "c1 != c2");
				Assert.That(c1.GetHashCode(), Is.Not.EqualTo(c3.GetHashCode()), "c1 != c3");
				Assert.That(c1.GetHashCode(), Is.EqualTo(c4.GetHashCode()), "c1 == c4");
			});
		}

		[Test]
		public static void Create()
		{
			const int x = 3;
			const int y = 20;
			const char value = '^';

			var cell = new Cell(new Point(x, y), value);
			
			Assert.Multiple(() =>
			{
				Assert.That(cell.Location.X, Is.EqualTo(x), nameof(Point.X));
				Assert.That(cell.Location.Y, Is.EqualTo(y), nameof(Point.Y));
				Assert.That(cell.Value, Is.EqualTo(value), nameof(Cell.Value));
			});
		}
	}
}