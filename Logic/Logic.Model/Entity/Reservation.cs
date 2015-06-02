using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
    [DataContract]
    public class Reservation : BusinessObject
    {
        /// <summary>
        ///     Date And Time on which this Reservation was made.
        /// </summary>
        [DataMember] private readonly DateTime _dateTime;

        //	TODO TEST THIS THING!
        public Reservation(Client client, Projection projection, IEnumerable<Seat> seats)
        {
            _dateTime = DateTime.Now;

            Link(ReserevationToClientAssociationName, client);
            Link(ReservationToProjectionAssociationName, projection);

            foreach (var seat in seats)
            {
                Link(ReservationToSeatAssociationName, seat);
            }


            Id = NextFreeId;
            NextFreeId++;
        }

        #region Properties

        /// <summary>
        ///     Unique identifier of this object.
        /// </summary>
        [DataMember]
        public int Id { get; private set; }

        /// <summary>
        ///     Next free identifier number which will be ascribed to next newly created instance of this class.
        /// </summary>
        [DataMember]
        public static int NextFreeId { get; set; }

        /// <summary>
        ///     Get or sets wheter this reservation redemption status.
        /// </summary>
        [DataMember]
        public bool IsRedeemed { get; set; }

        /// <summary>
        ///     Gets the date and time on which the Reservation was made.
        /// </summary>
        public DateTime DateTime
        {
            get { return _dateTime; }
        }

        /// <summary>
        ///     Gets the Projection for which this Reservation was made.
        /// </summary>
        public Projection Projection
        {
            get { return (Projection) GetLinkedObjects(ReservationToProjectionAssociationName).FirstOrDefault(); }
        }

        public IEnumerable<Seat> ReservedSeats
        {
            get { return GetLinkedObjects(ReservationToSeatAssociationName).Cast<Seat>(); }
        }

        /// <summary>
        ///     Gets the Client for whom this Reservation was made.
        /// </summary>
        public Client Client
        {
            get { throw new NotImplementedException(); }
        }

        public static string ReservationToSeatAssociationName { get; set; }

        public static string ReservationToProjectionAssociationName { get; set; }

        public static string ReserevationToClientAssociationName { get; set; }

        #endregion
    }
}