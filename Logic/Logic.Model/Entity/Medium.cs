using System;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents a physical medium on which a Film can be stored.
	/// </summary>
	public class Medium : ObjectWithAssociations
	{
		#region Private Members

		/// <summary>
		///		Serial number of the Medium.
		/// </summary>
		private readonly string _serialNumber;

		/// <summary>
		///		Date from which this Medium is stored.
		/// </summary>
		private readonly DateTime _storedFrom;

		/// <summary>
		///		Date to which this Medium is stored.
		/// </summary>
		private readonly DateTime _storedTo;
		#endregion


		public Medium(string serialNumber, DateTime dateFrom, DateTime dateTo, Film film)
		{
			_serialNumber = serialNumber;
			_storedFrom = dateFrom;
			_storedTo = dateTo;

			AddAsPartOf(Association.FromMediumToFilm, Association.FromFilmToMedium, film);
		}

		#region Properties
		/// <summary>
		///		Unique identifier of the Medium.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Serial number of the Medium.
		/// </summary>
		public string SerialNumber 
		{
			get
			{
				return _serialNumber;
			} 
		}

		/// <summary>
		///		Date from which this Medium is stored.
		/// </summary>
		public DateTime StoredFrom
		{
			get
			{
				return _storedFrom;
			}
		}

		/// <summary>
		///		Date to which this Medium is stored.
		/// </summary>
		public DateTime StoredTo
		{
			get { return _storedTo; }
		}

		/// <summary>
		///		Film for which this Medium is designated.
		/// </summary>
		public Film Film
		{
			get
			{
				return GetAssociations(Association.FromMediumToFilm)
					.FirstOrDefault() as Film;
			}
		}

		/// <summary>
		///		Assigns this Medium to given Projection by creating an association.
		/// </summary>
		/// <param name="projection">
		///		Reference to Projection class object.
		/// </param>
		public void AssignToProjection(Projection projection)
		{
			if (projection.Mediums.Count() > 2) return;

			AddAssociation(Association.FromMediumToProjection, Association.FromProjectionToMedium, projection);
		}
		#endregion
	}
}
