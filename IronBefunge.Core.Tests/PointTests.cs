using System;
using Xunit;

namespace IronBefunge.Core.Tests
{
	public static class PointTests
	{
		[Fact]
		public static void CheckForEquality()
		{
			var c1 = new Point(1, 2);
			var c2 = new Point(3, 2);
			var c3 = new Point(1, 2);

			Assert.NotEqual<Point>(c1, c2);
			Assert.Equal<Point>(c1, c3);
			Assert.NotEqual<Point>(c2, c3);
		}

		[Fact]
		public static void CheckForEqualityViaOperators()
		{
			var c1 = new Point(1, 2);
			var c2 = new Point(3, 2);
			var c3 = new Point(1, 2);

#pragma warning disable 1718
			Assert.True(c1 == c1);
#pragma warning restore 1718
			Assert.True(c1 != c2);
			Assert.True(c1 == c3);
			Assert.True(c2 != c3);
		}

		[Fact]
		public static void CheckForEqualityWithIncompatibleTypes()
		{
			var c = new Point(1, 2);
			var g = Guid.NewGuid();

			Assert.NotEqual<object>(c, g);
		}

		[Fact]
		public static void Create()
		{
			var point = new Point();
			Assert.Equal(0, point.X);
			Assert.Equal(0, point.Y);
		}

		[Fact]
		public static void CreateWithValues()
		{
			const int x = 3;
			const int y = 20;

			var point = new Point(x, y);
			Assert.Equal(x, point.X);
			Assert.Equal(y, point.Y);
		}
	}
}
