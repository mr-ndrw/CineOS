using System;
using System.Collections.Generic;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	public class Reservation : AssociatedObject
	{
		/// <summary>
		///		Date And Time on which this Reservation was made.
		/// </summary>
		private readonly DateTime _dateTime;

		//	TODO TEST THIS THING!
		public Reservation(Client client, Projection projection, IEnumerable<Seat> seats)
		{
			_dateTime = DateTime.Now;
			
			//	Iterate over seats and associate them with this Reservation.
			foreach (var seat in seats)
			{
				//AddAssociation(AssociationRole.FromReservationToSeat, AssociationRole.FromSeatToReservation, seat);
			}

			//	Associate this Reservation with the Client who has made the Reservation.
			//AddAssociation(AssociationRole.FromReservationToClient, AssociationRole.FromClientToReservation, client);

			//	Compose this Reservation into Projection for which this Reservation is made.
			//this.AddAsPartOf(AssociationRole.FromReservationToProjection, AssociationRole.FromProjectionToReservation,  projection);
		}
		
		#region Properties

		/// <summary>
		///		Get or sets wheter this reservation redemption status.
		/// </summary>
		public bool IsRedeemed { get; set; }

		/// <summary>
		///		Gets the date and time on which the Reservation was made.
		/// </summary>
		public DateTime DateTime
		{
			get{ return _dateTime; }
		}

		/// <summary>
		///		Gets the Projection for which this Reservation was made.
		/// </summary>
		public Projection Projection
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		///		Gets the Client for whom this Reservation was made.
		/// </summary>
		public Client Client
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}