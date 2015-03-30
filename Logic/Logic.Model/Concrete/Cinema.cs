using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Represents a Cinema unit.
	/// </summary>
	public class Cinema
	{
		/// <summary>
		///		Unique identifier of the Cinema.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Name of the Cinema.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Address of the Cinema.
		/// </summary>
		/// TODO: Create Address complex class.
		public string Address { get; set; }

		/// <summary>
		///		Cinema's telephone number.
		/// </summary>
		public string TelephoneNumber { get; set; }

		/// <summary>
		///		Collection representing the history of Employment in the Cinema.
		/// </summary>
		public IEnumerable<Employment> HistoryOfEmployments { get; set; }
	}
}
