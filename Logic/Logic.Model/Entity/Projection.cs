using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents a Projection entity. An event during which a film is shown.
	/// </summary>
	[DataContract]
	public class Projection : BusinessObject
	{

		public Projection(ProjectionRoom projectionRoom, Film film, DateTime dateTime)
		{
			Id = NextFreeId;
			NextFreeId++;

		    DateTime = dateTime;

			Link(ProjectionToProjectionRoomAssociationName, projectionRoom);
            Link(ProjectionToFilmAssociationName, film);
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
		///		Get or sets the date and time of the Projection.
		/// </summary>
		[DataMember]
		public DateTime DateTime { get; set; }

		/// <summary>
		///		Get or sets the duration of the whole projection in minutes.
		/// </summary>
		/// TODO change to film's length
		[DataMember]
		public int Length { get; set; }

		/// <summary>
		///		Gets the Projection Room in which this Projection takes place.
		/// </summary>
		public ProjectionRoom ProjectionRoom
		{
			get
			{
				return (ProjectionRoom) GetLinkedObjects(ProjectionToProjectionRoomAssociationName)
					.FirstOrDefault();

			}
		}

		/// <summary>
		///		Get the Cinema in which this Projection takes place.
		/// </summary>
		public Cinema Cinema
		{
			get { return ProjectionRoom.Cinema; }
		}

		/// <summary>
		///		Return the collection of Mediums associated with this Projection.
		/// </summary>
		public IEnumerable<Medium> Mediums
		{
			get { return GetLinkedObjects(ProjectionToMediumAssociationName).Cast<Medium>(); }
		}

		/// <summary>
		///		Gets the Film which will be viewed on this Projection.
		/// </summary>
		public Film Film 
		{
			get
			{
				return (Film) GetLinkedObjects(ProjectionToFilmAssociationName)
					.FirstOrDefault();
			}
		}

		public IEnumerable<Reservation> Reservations
		{
			get
			{
				return GetLinkedObjects(ProjectionToReservationAssociationName)
					.Cast<Reservation>();
			}
		}

	    public static IEnumerable<Projection> Extent
	    {
	        get { return RetrieveExtentFor(typeof (Projection)).Cast<Projection>(); }
	    }

        [DataMember]
	    public static string ProjectionToProjectionRoomAssociationName { get; set; }

        [DataMember]
		public static string ProjectionToEmployeeAssociatioName { get; set; }

        [DataMember]
		public static string ProjectionToReservationAssociationName { get; set; }

        [DataMember]
		public static string ProjectionToFilmAssociationName { get; set; }

        [DataMember]
		public static string ProjectionToMediumAssociationName { get; set; }

		#endregion

		#region Methods

	    public IEnumerable<Seat> GetSeatsInProjectionRoom()
	    {
	        return ProjectionRoom.Seats;
	    }

	    public IEnumerable<Seat> GetSeatsReserved()
	    {
	        var result = new List<Seat>();
	        foreach (var reservation in Reservations)
	        {
	            result.AddRange(reservation.ReservedSeats);
	        }

	        return result;
	    } 

		#endregion

	}
}
