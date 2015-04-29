using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	/// <summary>
	///		Represents an Employee entity.
	/// </summary>
	public class Employee : ObjectWithAssociations
	{
		#region Properties
		/// <summary>
		///		Unique identifier of the Employee.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Employee's address.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		///		Collection of Employee's history of Employments.
		/// </summary>
		public IEnumerable<Employment> HistoryOfEmployment 
		{
			get
			{
				return GetAssociations(AssociationRole.FromEmployeeToEmployment)
					.Cast<Employment>();
			} 
		}

		/// <summary>
		///		Employee's manager.
		/// </summary>
		public Employee Manager
		{
			get
			{
				return GetAssociations(AssociationRole.FromSubordinateToManager)
					.FirstOrDefault() as Employee;

			}
		}

		/// <summary>
		///		Employee's/Manager's subordinate Employees.
		/// </summary>
		public IEnumerable<Employee> Subordinates
		{
			get
			{
				return GetAssociations(AssociationRole.FromManagerToSubordinate)
					.Cast<Employee>();
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
			AddAssociation(AssociationRole.FromManagerToSubordinate, AssociationRole.FromSubordinateToManager, employee);
		}

		#endregion
	}
}
