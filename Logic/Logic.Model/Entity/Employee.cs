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
		#region Properties
		/// <summary>
		///		Unique identifier of the Employee.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

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
