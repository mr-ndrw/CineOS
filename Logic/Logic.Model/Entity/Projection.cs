using System;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents a Projection entity. An event during which a film is shown.
	/// </summary>
	/// <remarks>
	///		This class is associated with a one-to-many relationship with Cinema class, hence
	///		the presence of a reference to a Cinema object in the constructor's parameters.
	///		This is not a composition!!!
	/// </remarks>
	public class Projection : ObjectWithAssociations
	{

		/// <summary>
		///		Initializes a new instance of the Projection class using the mandatory refererence
		///		to the Cinema.
		/// </summary>
		/// <param name="cinema">
		///		Reference to the Cinema in which the Projection takes place.
		/// </param>
		public Projection(Cinema cinema)
		{
			AddAssociation(Association.FromProjectionToCinema, Association.FromCinemaToProjection, cinema);
		}

		#region Properties
		
		/// <summary>
		///		Unique identifier of the Projection.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Get or sets the date and time of the Projection.
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		///		Get or sets the duration of the whole projection in minutes.
		/// </summary>
		/// TODO change to film's length
		public int Length { get; set; }

		/// <summary>
		///		Gets the Projection Room in which this Projection takes place.
		/// </summary>
		public ProjectionRoom ProjectionRoom
		{
			get
			{
				return	GetAssociations(Association.FromProjectionToProjectionRoom)
						.FirstOrDefault() as ProjectionRoom;
			}
		}

		/// <summary>
		///		Get the Cinema in which this Projection takes place.
		/// </summary>
		public Cinema Cinema
		{
			get { return ProjectionRoom.Cinema; }
		} 

		#endregion

		#region Methods
		
		#endregion

	}
}
