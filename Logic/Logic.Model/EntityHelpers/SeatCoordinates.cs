using System.Collections.Generic;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Model.EntityHelpers
{
	[DataContract]
	public class SeatQualifier
	{
		public SeatQualifier(Seat seat)
		{
			RowNumber = seat.RowNumber;
			ColumNumber = seat.ColumnNumber;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public SeatQualifier(string rowNumber, string columNumber)
		{
			RowNumber = rowNumber;
			ColumNumber = columNumber;
		}

		public static SeatQualifierEqualityComparer EqualityComparer
		{
			get
			{
				return new SeatQualifierEqualityComparer();
			}
		}

		[DataMember]
		public string RowNumber { get; set; }

		[DataMember]
		public string ColumNumber { get; set; }
	}

	public class SeatQualifierEqualityComparer : IEqualityComparer<SeatQualifier>
	{
		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		/// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
		public bool Equals(SeatQualifier x, SeatQualifier y)
		{
			return x.RowNumber == y.RowNumber && x.ColumNumber == y.ColumNumber;
		}

		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <returns>
		/// A hash code for the specified object.
		/// </returns>
		/// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
		public int GetHashCode(SeatQualifier obj)
		{
			unchecked
			{
				int hash = 17;
				hash *= 23 + obj.RowNumber.GetHashCode();
				hash *= 29 + obj.ColumNumber.GetHashCode();
				return hash;
			}
		}
	}
}
