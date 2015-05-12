using System;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Seat in a Projection Room.
	/// </summary>
	[DataContract]
	public class Seat : AssociatedObject
	{

		public Seat(string rowNumber, string columnNumber)
		{
			RowNumber = rowNumber;
			ColumnNumber = columnNumber;

			//this.AddAsPartOf(AssociationRole.FromSeatToProjectionRoom, AssociationRole.FromProjectionRoomToSeat, projectionRoom);
		}

		/// <summary>
		///		Unique identifier of the Seat.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		///		Number of the column in which the Seat is situated.
		/// </summary>
		[DataMember]
		public string ColumnNumber { get; private set; }

		/// <summary>
		///		Number of the Row in which the Seat is placed.
		/// </summary>
		[DataMember]
		public string RowNumber { get; private set; }

		/// <summary>
		///		Gets the Projection Room in which the Seat is situated.
		/// </summary>
		public ProjectionRoom ProjectionRoom {
			get { throw new NotImplementedException(); } 
		}

	}
}