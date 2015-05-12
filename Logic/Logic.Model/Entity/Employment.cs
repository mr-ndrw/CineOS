using System;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents an employment relation ship between a Cinema and an Employee
	///		during a certain time.
	/// </summary>
	[DataContract]
	public class Employment : AssociatedObject
	{
		public Employment(Cinema cinema, Employee employee)
		{
			//	Create Assosciations between this and Cinema
			throw new NotImplementedException();

			Id = NextFreeId;
			NextFreeId++;
		}

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
		///		Date of employment.
		/// </summary>
		[DataMember]
		public DateTime DateOfEmployment { get; set; }

		/// <summary>
		///		Date of Employee's discharge from the cinema.
		/// </summary>
		/// <remarks>
		///		Initially null.
		/// </remarks>
		[DataMember]
		public DateTime? DateOfDischarge { get; set; }

		/// <summary>
		///		Employee's position in the Cinema.
		/// </summary>
		[DataMember]
		public string Position { get; set; }

		/// <summary>
		///		Employee's salary.
		/// </summary>
		[DataMember]
		public int Salary { get; set; }
		//	TODO: remove \ lata pracy from cd diagram.

	}
}
