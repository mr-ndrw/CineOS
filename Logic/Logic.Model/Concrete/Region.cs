using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
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
		#region Properties

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
		public IEnumerable<Cinema> Cinemas
		{
			get
			{
				return GetAssociations(Association.FromRegionToCinema)
					.Cast<Cinema>();
			}
		} 

		#endregion

		#region Methods

		/// <summary>
		///		Adds the designated Cinema to this Region's composition.
		/// </summary>
		/// <param name="cinema">
		///		Cinema to be added into this Region's composition.
		/// </param>
		public void AddCinema(Cinema cinema)
		{
			AddPart(Association.FromRegionToCinema, Association.FromCinemaToRegion, cinema);
		}

		#endregion


	}
}
