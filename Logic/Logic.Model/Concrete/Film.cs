using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Represents a Film entity.
	/// </summary>
	public class Film
	{

		public Film()
		{
			Mediums = new HashSet<Medium>();
		}
		/// <summary>
		///		Unique identifier of the Film.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Title of the Film.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Director of the Film.
		/// </summary>
		public string Director { get; set; }

		/// <summary>
		///		Genre of the Film.
		/// </summary>
		public string Genre { get; set; }

		/// <summary>
		///		Actors which star in the Film.
		/// </summary>
		public string Actors { get; set; }

		/// <summary>
		///		ESRB rating of the Film.
		/// </summary>
		public string EsrbRating { get; set; }

		/// <summary>
		///		Duration of the Film in minutes.
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		///		Collection of Mediums associated with this Film.
		/// </summary>
		public IEnumerable<Medium> Mediums { get; set; }
	}
}
