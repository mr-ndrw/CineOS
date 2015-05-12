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
	public class Employee : AssociatedObject
	{
		public Employee()
		{
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
		///		Employee's address.
		/// </summary>
		[DataMember]
		public string Address { get; set; }

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

		/// <summary>
		///		Employee's manager.
		/// </summary>
		public Employee Manager
		{
			get
			{
				throw new NotImplementedException();

			}
		}

		/// <summary>
		///		Employee's/Manager's subordinate Employees.
		/// </summary>
		public IEnumerable<Employee> Subordinates
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public static string EmployeeToCinemaAssociationName { get; set; }

		public static string EmployeeToProjectionRoomAssociationName { get; set; }

		#endregion		

		#region Methods

		public void AddToCinema(Cinema cinema)
		{
			new Employment(cinema, this);
		}

		public void AddSubordinate(Employee employee)
		{
			if (Manager != null) return;
			throw new NotImplementedException();
		}

		#endregion
	}
}
