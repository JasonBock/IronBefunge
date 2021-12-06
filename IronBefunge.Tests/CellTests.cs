using NUnit.Framework;

namespace IronBefunge.Tests;

public static class CellTests
{
	[Test]
	public static void CheckForEqualityWithSpecificType()
	{
		var c1 = new Cell(new(1, 1), 'c');
		var c2 = new Cell(new(2, 2), 'd');
		var c3 = new Cell(new(1, 1), 'd');
		var c4 = new Cell(new(1, 1), 'c');

		Assert.Multiple(() =>
		{
#pragma warning disable NUnit2010 // Use EqualConstraint for better assertion messages in case of failure
		  Assert.That(c1 == c2, Is.False, "c1 == c2");
			Assert.That(c1 != c2, Is.True, "c1 != c2");

			Assert.That(c1 == c3, Is.False, "c1 == c3");
			Assert.That(c1 != c3, Is.True, "c1 != c3");

			Assert.That(c1 == c4, Is.True, "c1 == c4");
			Assert.That(c1 != c4, Is.False, "c1 != c4");

			Assert.That(c2 == c3, Is.False, "c2 == c3");
			Assert.That(c2 != c3, Is.True, "c2 != c3");

			Assert.That(c2 == c4, Is.False, "c2 == c4");
			Assert.That(c2 != c4, Is.True, "c2 != c4");

			Assert.That(c3 == c4, Is.False, "c3 == c4");
			Assert.That(c3 != c4, Is.True, "c3 != c4");

			Assert.That(c1, Is.Not.EqualTo(c2), "!c1.Equals(c2)");
			Assert.That(c1, Is.Not.EqualTo(c3), "!c1.Equals(c3)");
			Assert.That(c1, Is.EqualTo(c4), "c1.Equals(c4)");
			Assert.That(c2, Is.Not.EqualTo(c3), "!c2.Equals(c3)");
			Assert.That(c2, Is.Not.EqualTo(c4), "!c2.Equals(c4)");
			Assert.That(c3, Is.Not.EqualTo(c4), "!c3.Equals(c4)");
#pragma warning restore NUnit2010 // Use EqualConstraint for better assertion messages in case of failure
	  });
	}

	[Test]
	public static void CheckForEqualityWithNulls()
	{
		var cell = new Cell(new(1, 1), 'c');

		Assert.Multiple(() =>
		{
#pragma warning disable NUnit2010 // Use EqualConstraint for better assertion messages in case of failure
		  Assert.That(cell == (null as Cell)!, Is.False, "cell == null");
			Assert.That(cell != (null as Cell)!, Is.True, "cell != null");
			Assert.That((null as Cell)! == cell, Is.False, "null == cell");
			Assert.That((null as Cell)! != cell, Is.True, "null != cell");
			Assert.That((null as Cell)! == (null as Cell)!, Is.True, "null == null");
			Assert.That((null as Cell)! != (null as Cell)!, Is.False, "null != null");
#pragma warning restore NUnit2010 // Use EqualConstraint for better assertion messages in case of failure
	  });
	}

	[Test]
	public static void CheckForEqualityWithObjectType()
	{
		var c1 = new Cell(new(1, 1), 'c');
		var c2 = new Cell(new(1, 1), 'c');

		Assert.That(c1, Is.EqualTo(c2), "c1.Equals(c4)");
	}

	[Test]
	public static void GetHashCodes()
	{
		var c1 = new Cell(new(1, 1), 'c');
		var c2 = new Cell(new(2, 2), 'd');
		var c3 = new Cell(new(1, 1), 'd');
		var c4 = new Cell(new(1, 1), 'c');

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

		var cell = new Cell(new(x, y), value);

		Assert.Multiple(() =>
		{
			Assert.That(cell.Location.X, Is.EqualTo(x), nameof(Point.X));
			Assert.That(cell.Location.Y, Is.EqualTo(y), nameof(Point.Y));
			Assert.That(cell.Value, Is.EqualTo(value), nameof(Cell.Value));
		});
	}

	[Test]
	public static void VerifyToString()
	{
		const int x = 3;
		const int y = 20;
		const char value = '^';

		var cell = new Cell(new(x, y), value);
		Assert.That(cell.ToString(), Is.EqualTo("3, 20 - '^'"));
	}
}