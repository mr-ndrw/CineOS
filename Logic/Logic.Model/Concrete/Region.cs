using System.Collections.Generic;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{

	/// <summary>
	///		Region of any kind.
	/// </summary>
	/// <remarks>
	///		May be either geographical or political.
	/// </remarks>
	public class Region : ObjectWithAssociations
	{
		public Region()
		{
			Cinemas = new List<Cinema>();
		}

		/// <summary>
		///		Unique identifier of the Region.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Name of the Region.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Collection of Cinemas in the Region.
		/// </summary>
		public virtual IEnumerable<Cinema> Cinemas { get; set; }

	}
}
