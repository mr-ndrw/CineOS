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
			Id = NextFreeId;
			NextFreeId++;
		}

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
		public string[] Actors { get; set; }

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

		public static string FilmToProjectionAssociationName { get; set; }

		public static string FilmToMediumAssociationName { get; set; }

		public static string FilmToClientAssociationName { get; set; }
	}
}
