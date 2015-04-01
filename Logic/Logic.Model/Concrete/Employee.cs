using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Represents an Employee entity.
	/// </summary>
	public class Employee
	{
		public Employee()
		{
			HistoryOfEmployment = new List<Employment>();
			Subordinates = new List<Employee>();
		}

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
		public IEnumerable<Employment> HistoryOfEmployment { get; set; }

		/// <summary>
		///		Id of the Employee's manager.
		/// </summary>
		public int ManagerId { get; set; }

		/// <summary>
		///		Employee's manager.
		/// </summary>
		public Employee Manager { get; set; }

		/// <summary>
		///		Employee's/Manager's subordinate Employees.
		/// </summary>
		public IEnumerable<Employee> Subordinates { get; set; }		
	}
}
