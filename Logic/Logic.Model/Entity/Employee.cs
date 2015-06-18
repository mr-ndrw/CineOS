using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents an Employee entity.
	/// </summary>
	[DataContract]
	public class Employee : BusinessObject
	{
		public Employee(string address, DateTime dateOfBirth, Person person)
		{
		    Address = address;
		    DateOfBirth = dateOfBirth;
		    Person = person;
		}

		#region Properties

        [DataMember]
	    public Person Person { get; set; }

		/// <summary>
		///		Employee's address.
		/// </summary>
		[DataMember]
		public string Address { get; set; }

        /// <summary>
        ///     Date of Birth of this Employee.
        /// </summary>
        [DataMember]
	    public DateTime DateOfBirth { get; set; }

		/// <summary>
		///		Collection of Employee's history of Employments.
		/// </summary>
		public IEnumerable<Employment> HistoryOfEmployment 
		{
			get
			{
				throw new NotImplementedException();
			} 
		}

		public static string EmployeeToCinemaAssociationName { get; set; }

		public static string EmployeeToProjectionRoomAssociationName { get; set; }

		#endregion		
	}
}
