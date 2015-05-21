using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.EntityHelpers;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using en.AndrewTorski.CineOS.Shared.HelperLibrary;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents a Cinema unit.
	/// </summary>
	[DataContract]
	public class Cinema : AssociatedObject
	{
		/// <summary>
		///		Creates a Cinema in the parametrized Region.
		/// </summary>
		/// <param name="region">
		///		Region in which this Cinema is placed.
		/// </param>
		public Cinema(Region region)
		{
			Link(CinemaToRegionAssociationName, region);

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
		///		Name of the Cinema.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		///		Address of the Cinema.
		/// </summary>
		/// TODO: Create Address complex class.
		[DataMember]
		public string Address { get; set; }

		/// <summary>
		///		Cinema's telephone number.
		/// </summary>
		[DataMember]
		public string TelephoneNumber { get; set; }

		/// <summary>
		///		Region in which this Cinema is situated	.
		/// </summary>
		public Region Region 
		{
			get
			{
				var result = GetLinkedObjects(CinemaToRegionAssociationName)
					.FirstOrDefault();

				return (Region) result;
			}
		}

		/// <summary>
		///		Collection representing the history of Employment in the Cinema.
		/// </summary>
		public IEnumerable<Employment> HistoryOfEmployments { get; private set; }

		/// <summary>
		///		Collection of Projection Rooms contained within this Cinema.
		/// </summary>
		public IEnumerable<ProjectionRoom> ProjectionRooms 
		{
			get
			{
				var result = GetLinkedObjects(CinemaToProjectionRoomAssociationName)
					.Cast<ProjectionRoom>();
				return result;
			}
		}

		/// <summary>
		///		Collection of Projections which take place in the Cinema.
		/// </summary>
		public IEnumerable<Projection> Projections
		{
			get
			{
				var projectionList = new List<Projection>();
				foreach (var projectionRoom in ProjectionRooms)
				{
					projectionList.AddRange(projectionRoom.Projections);
				}

				return projectionList;
			}
		}

		public static string CinemaToRegionAssociationName { get; set; }

		public static string CinemaToProjectionRoomAssociationName { get; set; }

		public static string CinemaToEmployeeAssociationName { get; set; }

		#endregion



		#region Methods

		/// <summary>
		///		Returns the Projection Room contained in this Cinema based on the Room's number.
		/// </summary>
		/// <param name="projectionRoomNumber">
		///		Unique(within the Cinema) number of the Projection Room.
		/// </param>
		/// <returns>
		///		Returns reference to found Projection Room or null if no such Room was found.
		/// </returns>
		public ProjectionRoom GetProjectionRoom(string projectionRoomNumber)
		{
			var result = GetQualifiedLinkedObject(CinemaToProjectionRoomAssociationName, new ProjectionRoomCoordinates(projectionRoomNumber)).FirstOrDefault();

			return (ProjectionRoom) result;
		}

		/// <summary>
		///		Returns the collection of Projections from the given date span.
		/// </summary>
		public IEnumerable<Projection> GetProjections(DateTime fromDate, DateTime toDate)
		{
			return Projections.Where(projection => projection.DateTime.IsBetween(fromDate, toDate));
		}

	    ///  <summary>
	    /// 		Returns the collection of Projections from the given date span for specified Film.
	    ///  </summary>
	    public IEnumerable<Projection> GetProjectionsFor(Film film, DateTime fromDate, DateTime toDate)
        {
            //  TODO Try with Distinct() and without.
            return Projections.Where(projection => projection.DateTime.IsBetween(fromDate, toDate)).Where(projection => projection.Film == film).Distinct();
        }


        /// <summary>
        ///     Gets the collection of Films that will be viewed in this Cinema.
        /// </summary>
	    public IEnumerable<Film> FilmsThatWillBeViewed
	    {
	        get
	        {
	            var now = DateTime.Now;
                //  TODO There will be an issue with uniqueness of return films. Fix it.
                //  TODO Will Distinct() fix it?
	            return
	                Projections.Where(projection => projection.DateTime >= now)
	                    .Select(projection => projection.Film).Distinct();
	        }
	    } 

		/// <summary>
		///		Returns the collection of Projections for this Cinema for the next week.
		/// </summary>
		/// <returns>
		///		Collection of Projections.
		/// </returns>
		public IEnumerable<Projection> GetProjectionsForTheNextWeek()
		{
			var nowDateTime = DateTime.Now;
			var nextWeekDateTime = DateTime.Now.AddDays(7);

			return Projections.Where(projection => projection.DateTime.IsBetween(nowDateTime, nextWeekDateTime));
		}

        /// <summary>
        ///		Gets the collection of existing Cinemas in the system.
        /// </summary>
        public static IEnumerable<Cinema> Extent
        {
            get
            {
                var result = RetrieveExtentFor(typeof(Cinema));

                return result.Cast<Cinema>();
            }
        }

		public void AddEmployee(Employee employee)
		{
			new Employment(this, employee);
		}

		#endregion
	}
}
