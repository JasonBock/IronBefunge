using System;

namespace IronBefunge
{
	/// <summary>
	/// Defines the X and Y positions of a value in FungeSpace.
	/// </summary>
	public readonly struct Point
		: IEquatable<Point>
	{
		/// <summary>
		/// Creates a new <see cref="Point" /> instance.
		/// </summary>
		/// <param name="x">The x value.</param>
		/// <param name="y">The y value.</param>
		public Point(int x, int y)
			: this() => (this.X, this.Y) = (x, y);

		/// <summary>
		/// Determines whether two specified <see cref="Point" /> objects have the same value. 
		/// </summary>
		/// <param name="a">A <see cref="Point" /> or a null reference.</param>
		/// <param name="b">A <see cref="Point" /> or a null reference.</param>
		/// <returns><b>true</b> if the value of <paramref name="a"/> is the same as the value of <paramref name="b"/>; otherwise, <b>false</b>. </returns>
		public static bool operator ==(Point a, Point b) => a.Equals(b);

		/// <summary>
		/// Determines whether two specified <see cref="Point" /> objects have different value. 
		/// </summary>
		/// <param name="a">A <see cref="Point" /> or a null reference.</param>
		/// <param name="b">A <see cref="Point" /> or a null reference.</param>
		/// <returns><b>true</b> if the value of <paramref name="a"/> is different from the value of <paramref name="b"/>; otherwise, <b>false</b>. </returns>
		public static bool operator !=(Point a, Point b) => !(a == b);

		/// <summary>
		/// Checks to see if the given object is equal to the current <see cref="Point" /> instance.
		/// </summary>
		/// <param name="obj">The object to check for equality.</param>
		/// <returns>Returns <c>true</c> if the objects are equals; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			var areEqual = false;

			if (obj is Point point)
			{
				areEqual = this.Equals(point);
			}

			return areEqual;
		}

		/// <summary>
		/// Provides a type-safe equality check.
		/// </summary>
		/// <param name="other">The object to check for equality.</param>
		/// <returns>Returns <c>true</c> if the objects are equals; otherwise, <c>false</c>.</returns>
		public bool Equals(Point other) =>
			this.X == other.X && this.Y == other.Y;

		/// <summary>
		/// Gets a hash code based on the <see cref="X"/> and <see cref="Y"/> values.
		/// </summary>
		/// <returns>A hash code.</returns>
		public override int GetHashCode() => HashCode.Combine(this.X, this.Y);

		/// <summary>
		/// Returns a meaningful string representation of the current <see cref="Point" /> instance.
		/// </summary>
		/// <returns>A string representation of the object.</returns>
		public override string ToString() => $"{this.X}, {this.Y}";

		/// <summary>
		/// Gets the x value.
		/// </summary>
		public int X { get; }

		/// <summary>
		/// Gets the y value.
		/// </summary>
		public int Y { get; }
	}
}