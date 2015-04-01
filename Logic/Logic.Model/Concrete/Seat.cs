using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Seat in a Projection Room.
	/// </summary>
	public class Seat
	{
		/// <summary>
		///		Unique identifier of the Seat.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Number of the column in which the Seat is situated.
		/// </summary>
		public string ColumnNumber { get; set; }

		/// <summary>
		///		Number of the Row in which the Seat is placed.
		/// </summary>
		public string RowNumber { get; set; }

		/// <summary>
		///		Id of the Projection Room in which the Seat is situated.
		/// </summary>
		public int ProjectionRoomId { get; set; }

		/// <summary>
		///		Projection Room in which the Seat is situated.
		/// </summary>
		public ProjectionRoom ProjectionRoom { get; set; }

	}
}