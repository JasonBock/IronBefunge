﻿using System;

namespace IronBefunge
{
	/// <summary>
	/// Defines the X and Y positions of a value in FungeSpace.
	/// </summary>
	public sealed class Cell
		: IEquatable<Cell>
	{
		/// <summary>
		/// Creates a new <see cref="Cell" /> instance.
		/// </summary>
		/// <param name="x">The x value.</param>
		/// <param name="y">The y value.</param>
		public Cell(Point location, char value)
			: base() => (this.Location, this.Value) = (location, value);

		/// <summary>
		/// Determines whether two specified <see cref="Cell" /> objects have the same value. 
		/// </summary>
		/// <param name="a">A <see cref="Cell" /> or a null reference.</param>
		/// <param name="b">A <see cref="Cell" /> or a null reference.</param>
		/// <returns><b>true</b> if the value of <paramref name="a"/> is the same as the value of <paramref name="b"/>; otherwise, <b>false</b>. </returns>
		public static bool operator ==(Cell a, Cell b)
		{
			bool equal;

			if (object.ReferenceEquals(a, b))
			{
				equal = true;
			}
			else if (a is null || b is null)
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
		public static bool operator !=(Cell a, Cell b) => !(a == b);

		/// <summary>
		/// Checks to see if the given object is equal to the current <see cref="Cell" /> instance.
		/// </summary>
		/// <param name="obj">The object to check for equality.</param>
		/// <returns>Returns <c>true</c> if the objects are equals; otherwise, <c>false</c>.</returns>
		public override bool Equals(object? obj)
		{
			var areEqual = false;

			if (obj is Cell target)
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
		public bool Equals(Cell? other) =>
			other is not null && this.Location == other.Location &&
				this.Value == other.Value;

		/// <summary>
		/// Gets a hash code based on the <see cref="Location" and <see cref="Value"/> values./>
		/// </summary>
		/// <returns>A hash code.</returns>
		public override int GetHashCode() => HashCode.Combine(this.Location, this.Value);

		/// <summary>
		/// Returns a meaningful string representation of the current <see cref="Cell" /> instance.
		/// </summary>
		/// <returns>A string representation of the object.</returns>
		public override string ToString() => $"{this.Location} - '{this.Value}'";

		/// <summary>
		/// Gets the location.
		/// </summary>
		public Point Location { get; }

		/// <summary>
		/// Gets the value.
		/// </summary>
		public char Value { get; }
	}
}