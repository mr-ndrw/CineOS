using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using en.AndrewTorski.CineOS.Shared.HelperLibrary;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents a Cinema unit.
	/// </summary>
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
			throw new NotImplementedException();
		}

		#region Properties
		/// <summary>
		///		Unique identifier of the Cinema.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Name of the Cinema.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Address of the Cinema.
		/// </summary>
		/// TODO: Create Address complex class.
		public string Address { get; set; }

		/// <summary>
		///		Cinema's telephone number.
		/// </summary>
		public string TelephoneNumber { get; set; }

		/// <summary>
		///		Region in which this Cinema is situated.
		/// </summary>
		public Region Region 
		{
			get { throw new NotImplementedException(); }
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
				throw new NotImplementedException();
			}
		}

		/// <summary>
		///		Collection of Projections which take place in the Cinema.
		/// </summary>
		public IEnumerable<Projection> Projections
		{
			get
			{
				throw new NotImplementedException();
			}
		}
 		 
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
			throw new NotImplementedException();
		}

		/// <summary>
		///		Returns the collection of Projections from the given date span.
		/// </summary>
		/// <param name="fromDate">
		///		From DateTime.
		/// </param>
		/// <param name="toDate">
		///		To DateTime
		/// </param>
		/// <returns>
		///		Collection of Projections.
		/// </returns>
		public IEnumerable<Projection> GetProjections(DateTime fromDate, DateTime toDate)
		{
			return Projections.Where(projection => projection.DateTime.IsBetween(fromDate, toDate));
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

		public void AddEmployee(Employee employee)
		{
			new Employment(this, employee);
		}

		#endregion
	}
}
