using System;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Represents a Projection entity. An event during which a film is shown.
	/// </summary>
	public class Projection
	{
		/// <summary>
		///		Unique identifier of the Projection.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Date and time of the Projection.
		/// </summary>
		public DateTime DateTimeO { get; set; }

		/// <summary>
		///		Duration of the whole projection in minutes.
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		///		Id of the Projection Room in which this Projection takes place.
		/// </summary>
		public int ProjectionRoomId { get; set; }

		/// <summary>
		///		Projection Room in which this Projection takes place.
		/// </summary>
		public ProjectionRoom ProjectionRoom { get; set; }

		/// <summary>
		///		Cinema in which this Projection takes place.
		/// </summary>
		public Cinema Cinema
		{
			get { return ProjectionRoom.Cinema; }
		}

		/// <summary>
		///		Id of the Film which is displayed during this Projection.
		/// </summary>
		public int FilmdId { get; set; }

		/// <summary>
		///		Film which is displayed during this Projection.
		/// </summary>
		public Film Film { get; set; }
	}
}
