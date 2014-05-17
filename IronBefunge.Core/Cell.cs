using System;
using System.Globalization;

namespace IronBefunge.Core
{
	/// <summary>
	/// Defines the X and Y positions of a value in FungeSpace.
	/// </summary>
	[Serializable]
	public sealed class Cell
		: IEquatable<Cell>
	{
		private const string ToStringFormat = "{0} - '{1}'";

		/// <summary>
		/// Creates a new <see cref="Cell" /> instance.
		/// </summary>
		/// <param name="x">The x value.</param>
		/// <param name="y">The y value.</param>
		public Cell(Point location, char value)
			: base()
		{
			this.Location = location;
			this.Value = value;
		}

		/// <summary>
		/// Determines whether two specified <see cref="Cell" /> objects have the same value. 
		/// </summary>
		/// <param name="a">A <see cref="Cell" /> or a null reference.</param>
		/// <param name="b">A <see cref="Cell" /> or a null reference.</param>
		/// <returns><b>true</b> if the value of <paramref name="a"/> is the same as the value of <paramref name="b"/>; otherwise, <b>false</b>. </returns>
		public static bool operator ==(Cell a, Cell b)
		{
			bool equal = false;

			if (object.ReferenceEquals(a, b))
			{
				equal = true;
			}
			else if (((object)a == null) || ((object)b == null))
			{
				equal = false;
			}
			else
			{
				equal = a.Equals(b);
			}

			return equal;
		}

		/// <summary>
		/// Determines whether two specified <see cref="Cell" /> objects have different value. 
		/// </summary>
		/// <param name="a">A <see cref="Cell" /> or a null reference.</param>
		/// <param name="b">A <see cref="Cell" /> or a null reference.</param>
		/// <returns><b>true</b> if the value of <paramref name="a"/> is different from the value of <paramref name="b"/>; otherwise, <b>false</b>. </returns>
		public static bool operator !=(Cell a, Cell b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Checks to see if the given object is equal to the current <see cref="Cell" /> instance.
		/// </summary>
		/// <param name="obj">The object to check for equality.</param>
		/// <returns>Returns <c>true</c> if the objects are equals; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			var areEqual = false;
			var target = obj as Cell;

			if (target != null)
			{
				areEqual = this.Equals(target);
			}

			return areEqual;
		}

		/// <summary>
		/// Provides a type-safe equality check.
		/// </summary>
		/// <param name="other">The object to check for equality.</param>
		/// <returns>Returns <c>true</c> if the objects are equals; otherwise, <c>false</c>.</returns>
		public bool Equals(Cell other)
		{
			return other != null && this.Location == other.Location &&
				this.Value == other.Value;
		}

		/// <summary>
		/// Gets a hash code based on the 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.Location.GetHashCode() ^ this.Value.GetHashCode();
		}

		/// <summary>
		/// Returns a meaningful string representation of the current <see cref="Cell" /> instance.
		/// </summary>
		/// <returns>A string representation of the object.</returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, Cell.ToStringFormat,
				this.Location, this.Value);
		}

		/// <summary>
		/// Gets the location.
		/// </summary>
		public Point Location { get; private set; }

		/// <summary>
		/// Gets the value.
		/// </summary>
		public char Value { get; set; }
	}
}
