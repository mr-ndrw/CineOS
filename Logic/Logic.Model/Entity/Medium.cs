using System;
using System.Linq;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents a physical medium on which a Film can be stored.
	/// </summary>
	[DataContract]
	public class Medium : AssociatedObject
	{
		#region Private Members

		/// <summary>
		///		Serial number of the Medium.
		/// </summary>
		[DataMember]
		private readonly string _serialNumber;

		/// <summary>
		///		Date from which this Medium is stored.
		/// </summary>
		[DataMember]
		private readonly DateTime _storedFrom;

		/// <summary>
		///		Date to which this Medium is stored.
		/// </summary>
		[DataMember]
		private readonly DateTime _storedTo;
		#endregion


		public Medium(string serialNumber, DateTime dateFrom, DateTime dateTo, Film film)
		{
			_serialNumber = serialNumber;
			_storedFrom = dateFrom;
			_storedTo = dateTo;

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
				throw new NotImplementedException();
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

			throw new NotImplementedException();
		}
		#endregion
	}
}
