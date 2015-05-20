using System.Collections.Generic;
using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Model.EntityHelpers
{
	public class ProjectionRoomCoordinates
	{
		public ProjectionRoomCoordinates(ProjectionRoom projectionRoom)
		{
			RoomNumber = projectionRoom.Number;
		}

		public ProjectionRoomCoordinates(string roomNumber)
		{
			RoomNumber = roomNumber;
		}

		public static ProjectionRoomCoordiantesEqualityComparer EqualityComparer
		{
			get
			{
				return new ProjectionRoomCoordiantesEqualityComparer();
			}
		}

		public string RoomNumber { get; private set; }
	}

	public class ProjectionRoomCoordiantesEqualityComparer : IEqualityComparer<ProjectionRoomCoordinates>
	{
		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		/// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
		public bool Equals(ProjectionRoomCoordinates x, ProjectionRoomCoordinates y)
		{
			return x.RoomNumber == y.RoomNumber;
		}

		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <returns>
		/// A hash code for the specified object.
		/// </returns>
		/// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
		public int GetHashCode(ProjectionRoomCoordinates obj)
		{
			unchecked
			{
				int hash = 17;
				hash *= 23 + obj.RoomNumber.GetHashCode();
				return hash;
			}
		}
	}
}