using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Represents a Cinema unit.
	/// </summary>
	public class Cinema : ObjectWithAssociations
	{

		public Cinema()
		{
			HistoryOfEmployments = new List<Employment>();
			ProjectionRooms = new List<ProjectionRoom>();
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
		///		Id of the Region in which this Cinema is situated.
		/// </summary>
		public int RegionId { get; set; }

		/// <summary>
		///		Region in which this Cinema is situated.
		/// </summary>
		public Region Region { get; set; }

		/// <summary>
		///		Collection representing the history of Employment in the Cinema.
		/// </summary>
		public IEnumerable<Employment> HistoryOfEmployments { get; set; }

		/// <summary>
		///		Collection of Projection Rooms contained within this Cinema.
		/// </summary>
		public IEnumerable<ProjectionRoom> ProjectionRooms { get; set; }

		/// <summary>
		///		Collection of Projections which take place in the Cinema.
		/// </summary>
		public IEnumerable<Projection> Projections
		{
			get
			{
				var projections = new List<Projection>();
				foreach (var projectionRoom in ProjectionRooms)
				{
					projections.AddRange(projectionRoom.Projections);
				}

				return projections;
			}
		} 
		#endregion


		#region Methods

		public ProjectionRoom GetProjectionRoom(string projectionRoomNumber)
		{
			return ProjectionRooms.FirstOrDefault(projectionRoom => projectionRoom.Number.Equals(projectionRoomNumber));
		}

		#endregion
	}
}
