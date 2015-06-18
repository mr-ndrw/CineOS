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
        [DataMember] 
        private readonly DateTime _dateTime;

        /// <summary>
        ///     Initializes a new instance of the Reservation class using the Client which creates the Reservaration, Projection for which the Reservation is made and the Seats.
        /// </summary>
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

        /// <summary>
        ///     Initializes a new instance of the Reservation class using the Client which creates the Reservaration, Projection for which the Reservation is made and a Seat0.
        /// </summary>
        public Reservation(Client client, Projection projection, Seat seat)
            : this(client, projection, new List<Seat>() { seat })
        {
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

        /// <summary>
        ///     Gets the collection of Seats which have reserved by this Reservation.
        /// </summary>
        public IEnumerable<Seat> ReservedSeats
        {
            get { return GetLinkedObjects(ReservationToSeatAssociationName).Cast<Seat>(); }
        }

        /// <summary>
        ///     Gets the Client for whom this Reservation was made.
        /// </summary>
        public Client Client
        {
            get
            {
                var foundClient = GetLinkedObjects(ReserevationToClientAssociationName).FirstOrDefault();
                var client = (Client) foundClient;

                return client;
            }
        }

        public static IEnumerable<Reservation> Extent
        {
            get { return RetrieveExtentFor(typeof (Reservation)).Cast<Reservation>(); }
        }

        [DataMember]
        public static string ReservationToSeatAssociationName { get; set; }

        [DataMember]
        public static string ReservationToProjectionAssociationName { get; set; }

        [DataMember]
        public static string ReserevationToClientAssociationName { get; set; }



        #endregion
    }
}