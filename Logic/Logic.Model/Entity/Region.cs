using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{

	/// <summary>
	///		Region of any kind.
	/// </summary>
	/// <remarks>
	///		May be either geographical or political.
	/// </remarks>
	[DataContract]
	public class Region : AssociatedObject
	{
		public Region()
		{

			Id = NextFreeId;
			NextFreeId++;
		}

		#region Properties

		/// <summary>
		///		Unique identifier of this object.
		/// </summary>
		[DataMember]
		public int Id { get; private set; }

		/// <summary>
		///		Next free identifier number which will be ascribed to next newly created instance of this class.
		/// </summary>
		[DataMember]
		public static int NextFreeId { get; set; }
		/// <summary>
		///		Name of the Region.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		///		Collection of Cinemas in the Region.
		/// </summary>
		public IEnumerable<Cinema> Cinemas
		{
			get { return null; }
		}

		public static string RegionToCinemaAssociationName { get; set; }

		#endregion

		#region Methods

		#endregion


	}
}
