using System;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents a physical medium on which a Film can be stored.
	/// </summary>
	public class Medium
	{
		/// <summary>
		///		Unique identifier of the Medium.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Serial number of the Medium.
		/// </summary>
		public string SerialNumber { get; set; }

		/// <summary>
		///		Date from which this Medium is stored.
		/// </summary>
		public DateTime StoredFrom { get; set; }

		/// <summary>
		///		Date to which this Medium is stored.
		/// </summary>
		public DateTime StoredTo { get; set; }

		/// <summary>
		///		Id of the Film for which this Medium is designated.
		/// </summary>
		public int FilmId { get; set; }

		/// <summary>
		///		Film for which this Medium is designated.
		/// </summary>
		public Film Film { get; set; }
	}
}
