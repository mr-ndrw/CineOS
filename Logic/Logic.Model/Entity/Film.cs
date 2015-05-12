using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents a Film entity.
	/// </summary>
	[DataContract]
	public class Film : AssociatedObject
	{

		public Film()
		{
		}

		/// <summary>
		///		Unique identifier of the Film.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		///		Title of the Film.
		/// </summary>
		[DataMember]
		public string Title { get; set; }

		/// <summary>
		///		Director of the Film.
		/// </summary>
		[DataMember]
		public string Director { get; set; }

		/// <summary>
		///		Genre of the Film.
		/// </summary>
		[DataMember]
		public string Genre { get; set; }

		/// <summary>
		///		Actors which star in the Film.
		/// </summary>
		[DataMember]
		public string Actors { get; set; }

		/// <summary>
		///		ESRB rating of the Film.
		/// </summary>
		[DataMember]
		public string EsrbRating { get; set; }

		/// <summary>
		///		Duration of the Film in minutes.
		/// </summary>
		[DataMember]
		public int Length { get; set; }

		/// <summary>
		///		Collection of Mediums associated with this Film.
		/// </summary>
		public IEnumerable<Medium> Mediums
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
