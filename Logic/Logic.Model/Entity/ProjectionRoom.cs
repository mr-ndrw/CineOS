using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using en.AndrewTorski.CineOS.Shared.HelperLibrary.EqualityComparer;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///     Represents a Projection Room structure within the Cinema which is capable
	///     of holding Projections.
	/// </summary>
	public class ProjectionRoom : ObjectWithAssociations
	{
		public ProjectionRoom(string number, Cinema cinema)
		{
			Number = number;
			//	Add this Projection Room to the composition owner - Cinema.
			this.AddAsPartOf(AssociationRole.FromProjectionRoomToCinema, AssociationRole.FromCinemaToProjectionRoom, cinema);
		}

		#region Properties

		/// <summary>
		///     Unique identifier of the Projection Room.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Projection room's number.
		/// </summary>
		/// <remarks>
		///     Is a a string just in case, someone would decide to use either Roman numerals
		///     or any other numbering system.
		/// </remarks>
		public string Number { get; set; }

		/// <summary>
		///     Projection room's name.
		/// </summary>
		/// <remarks>
		///     May be null. Type? operator doesn't work on string. Beware.
		/// </remarks>
		public string Name { get; set; }

		/// <summary>
		///     Gets the Cinema to which this Projection Room belongs.
		/// </summary>
		public Cinema Cinema
		{
			get
			{
				return GetAssociations(AssociationRole.FromProjectionRoomToCinema)
					.FirstOrDefault() as Cinema;
			}
		}

		/// <summary>
		///     Collection of Projections which take place in this Projection Room.
		/// </summary>
		public IEnumerable<Projection> Projections
		{
			get
			{
				return GetAssociations(AssociationRole.FromProjectionRoomToProjection)
					.Cast<Projection>();
			}
		}

		/// <summary>
		///     Seats situated in this Projection Room.
		/// </summary>
		public IEnumerable<Seat> Seats
		{
			get
			{
				return GetAssociations(AssociationRole.FromProjectionRoomToSeat)
					.Cast<Seat>();
			}
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
			return GetQualifiedAssociation(AssociationRole.FromProjectionRoomToSeat, new Tuple<string, string>(rowNumber, columnNumber), new PairComparer<string, string>()) as Seat;
		}

		#endregion
	}
}