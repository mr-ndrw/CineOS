namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Represents a Projection Room structure within the Cinema which is capable
	///		of holding Projections.
	/// </summary>
	public class ProjectionRoom
	{
		/// <summary>
		///		Unique identifier of the Projection Room.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Projection room's number.
		/// </summary>
		/// <remarks>
		///		Is a a string just in case, someone would decide to use either Roman numerals
		///		or any other numbering system.
		/// </remarks>
		public string Number { get; set; }

		/// <summary>
		///		Projection room's name.
		/// </summary>
		/// <remarks>
		///		May be null. Type? operator doesn't work on string. Beware.
		/// </remarks>
		public string Name { get; set; }

		/// <summary>
		///		Unique identifier of the Cinema in which this projection room exists.
		/// </summary>
		public int CinemaId { get; set; }

		/// <summary>
		///		Cinema to which this Projection Room belongs.
		/// </summary>
		public Cinema Cinema { get; set; }

	}
}
