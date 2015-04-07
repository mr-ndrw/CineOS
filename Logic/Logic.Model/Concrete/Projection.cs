using System;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Represents a Projection entity. An event during which a film is shown.
	/// </summary>
	public class Projection : ObjectWithAssociations
	{
		
		#region Properties
		
		/// <summary>
		///		Unique identifier of the Projection.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Date and time of the Projection.
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		///		Duration of the whole projection in minutes.
		/// </summary>
		/// TODO change to film's length
		public int Length { get; set; }

		/// <summary>
		///		Projection Room in which this Projection takes place.
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
		///		Cinema in which this Projection takes place.
		/// </summary>
		public Cinema Cinema
		{
			get { return ProjectionRoom.Cinema; }
		} 

		#endregion

		#region Methods

		public void AddReservation(Reservation reservation)
		{
			AddPart(Association.FromProjectionToReservation, Association.FromReservationToProjection, reservation);
		}

		#endregion

	}
}
