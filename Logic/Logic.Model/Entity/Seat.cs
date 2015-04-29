using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Seat in a Projection Room.
	/// </summary>
	public class Seat : ObjectWithAssociations
	{
		/// <summary>
		///		Projection Room in which the Seat is situated.
		/// </summary>
		private readonly ProjectionRoom _projectionRoom;

		public Seat(string rowNumber, string columnNumber, ProjectionRoom projectionRoom)
		{
			RowNumber = rowNumber;
			ColumnNumber = columnNumber;
			_projectionRoom = projectionRoom;

			this.AddAsPartOf(AssociationRole.FromSeatToProjectionRoom, AssociationRole.FromProjectionRoomToSeat, projectionRoom);
		}

		/// <summary>
		///		Unique identifier of the Seat.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Number of the column in which the Seat is situated.
		/// </summary>
		public string ColumnNumber { get; private set; }

		/// <summary>
		///		Number of the Row in which the Seat is placed.
		/// </summary>
		public string RowNumber { get; private set; }

		/// <summary>
		///		Gets the Projection Room in which the Seat is situated.
		/// </summary>
		public ProjectionRoom ProjectionRoom {
			get { return _projectionRoom; } 
		}

	}
}