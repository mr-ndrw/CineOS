using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
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
				return GetAssociations(AssociationRole.FromRegionToCinema)
					.Cast<Cinema>();
			}
		} 

		#endregion

		#region Methods

		#endregion


	}
}
