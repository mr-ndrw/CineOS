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
		#region Properties

		/// <summary>
		///		Unique identifier of the Region.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

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

		#endregion

		#region Methods

		#endregion


	}
}
