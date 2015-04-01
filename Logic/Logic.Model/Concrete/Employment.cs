using System;

namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{
	/// <summary>
	///		Represents an employment relation ship between a Cinema and an Employee
	///		during a certain time.
	/// </summary>
	public class Employment
	{
		/// <summary>
		///		Cinema's Id.
		/// </summary>
		public int CinemaId { get; set; }

		/// <summary>
		///		Cinema in which the Employee was employed.
		/// </summary>
		public Cinema Cinema { get; set; }

		/// <summary>
		///		Employee's Id.
		/// </summary>
		public int EmployeeId { get; set; }

		/// <summary>
		///		Employee which was employed in the Cinema.
		/// </summary>
		public Employee Employee { get; set; }

		/// <summary>
		///		Date of employment.
		/// </summary>
		public DateTime DateOfEmployment { get; set; }

		/// <summary>
		///		Date of Employee's discharge from the cinema.
		/// </summary>
		/// <remarks>
		///		Initially null.
		/// </remarks>
		public DateTime? DateOfDischarge { get; set; }

		/// <summary>
		///		Employee's position in the Cinema.
		/// </summary>
		public string Position { get; set; }

		/// <summary>
		///		Employee's salary.
		/// </summary>
		public int Salary { get; set; }
		


		//	TODO: remove \ lata pracy from cd diagram.

	}
}
