using NUnit.Framework;
using System;

namespace IronBefunge.Tests
{
	public static class PointTests
	{
		[Test]
		public static void CheckForEqualityWithSpecificType()
		{
			var c1 = new Point(1, 2);
			var c2 = new Point(3, 2);
			var c3 = new Point(1, 2);

			Assert.Multiple(() =>
			{
				Assert.That(c1.Equals(c2), Is.False, "!c1.Equals(c2)");
				Assert.That(c1.Equals(c3), Is.True, "c1.Equals(c3)");
				Assert.That(c2.Equals(c3), Is.False, "!c2.Equals(c3)");
			});
		}

		[Test]
		public static void CheckForEqualityWithObjectType()
		{
			var c1 = new Point(1, 2);
			var c2 = new Point(1, 2);

			Assert.That(c1.Equals((object)c2), Is.True, "c1.Equals(c3)");
		}

		[Test]
		public static void CheckForEqualityViaOperators()
		{
			var c1 = new Point(1, 2);
			var c2 = new Point(3, 2);
			var c3 = new Point(1, 2);

			Assert.Multiple(() =>
			{
#pragma warning disable 1718
				Assert.That(c1 == c1, Is.True, "c1 == c1");
#pragma warning restore 1718
				Assert.That(c1 != c2, Is.True, "c1 != c2");
				Assert.That(c1 == c3, Is.True, "c1 == c3");
				Assert.That(c2 != c3, Is.True, "c2 != c3");
			});
		}

		[Test]
		public static void CheckForEqualityWithIncompatibleTypes()
		{
			var c = new Point(1, 2);
			var g = Guid.NewGuid();

			Assert.That(c, Is.Not.EqualTo(g), "!c.Equals(g)");
		}

		[Test]
		public static void GetHashCodes()
		{
			var c1 = new Point(1, 2);
			var c2 = new Point(3, 2);
			var c3 = new Point(1, 2);

			Assert.Multiple(() =>
			{
				Assert.That(c1.GetHashCode(), Is.Not.EqualTo(c2.GetHashCode()), "c1 != c2");
				Assert.That(c1.GetHashCode(), Is.EqualTo(c3.GetHashCode()), "c1 == c3");
				Assert.That(c2.GetHashCode(), Is.Not.EqualTo(c3.GetHashCode()), "c2 != c3");
			});
		}

		[Test]
		public static void Create()
		{
			var point = new Point();

			Assert.Multiple(() =>
			{
				Assert.That(point.X, Is.Zero, nameof(Point.X));
				Assert.That(point.Y, Is.Zero, nameof(Point.Y));
			});
		}

		[Test]
		public static void CreateWithValues()
		{
			const int x = 3;
			const int y = 20;

			var point = new Point(x, y);

			Assert.Multiple(() =>
			{
				Assert.That(point.X, Is.EqualTo(x), nameof(Point.X));
				Assert.That(point.Y, Is.EqualTo(y), nameof(Point.Y));
			});
		}
	}
}