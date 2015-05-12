using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using en.AndrewTorski.CineOS.Shared.HelperLibrary.EqualityComparer;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///     Represents a Projection Room structure within the Cinema which is capable
	///     of holding Projections.
	/// </summary>
	[DataContract]
	public class ProjectionRoom : AssociatedObject
	{
		public ProjectionRoom(string number, Cinema cinema)
		{
			Number = number;
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
		///     Projection room's number.
		/// </summary>
		/// <remarks>
		///     Is a a string just in case, someone would decide to use either Roman numerals
		///     or any other numbering system.
		/// </remarks>
		[DataMember]
		public string Number { get; set; }

		/// <summary>
		///     Projection room's name.
		/// </summary>
		/// <remarks>
		///     May be null. Type? operator doesn't work on string. Beware.
		/// </remarks>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		///     Gets the Cinema to which this Projection Room belongs.
		/// </summary>
		public Cinema Cinema
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		///     Collection of Projections which take place in this Projection Room.
		/// </summary>
		public IEnumerable<Projection> Projections
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		///     Seats situated in this Projection Room.
		/// </summary>
		public IEnumerable<Seat> Seats
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region Methods

		/// <summary>
		///     Returns the Seat contained in this Projection Room based on the Seat's row number and column number.
		/// </summary>
		/// <param name="rowNumber">
		///     Seat's row number.
		/// </param>
		/// <param name="columnNumber">
		///		Seat's column number.
		/// </param>
		/// <returns>
		///     Returns reference to found Projection Room or null if no such Room was found.
		/// </returns>
		public Seat GetSeat(string rowNumber, string columnNumber)
		{
			throw new NotImplementedException();
		}

		public static string ProjectionRoomToCinemaAssociationName { get; set; }

		public static string ProjectionRoomToSeatAssociationName { get; set; }

		public static string ProjectionRoomToProjectionAssociationName { get; set; }

		#endregion
	}
}