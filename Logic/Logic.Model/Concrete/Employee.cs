using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		
	/// </summary>
	public class Employee
	{
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
	}
}
