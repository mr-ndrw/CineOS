using System;
using System.Runtime.CompilerServices;

namespace en.AndrewTorski.CineOS.Shared.HelperLibrary
{
	/// <summary>
	///		Contains extension methods for DateTime.
	/// </summary>
	public static class DateTimeExtenstions
	{
		/// <summary>
		///		Check whether this Date Time is between two given Date Times.
		/// </summary>
		/// <param name="dateTime">
		///		Date and Time to check.
		/// </param>
		/// <param name="from">
		///		From Date and Time.
		/// </param>
		/// <param name="to">
		///		To Date and Time.
		/// </param>
		/// <returns>
		///		Bool value.
		/// </returns>
		public static bool IsBetween(this DateTime dateTime, DateTime from, DateTime to)
		{
			return dateTime > from && dateTime < to;
		}
	}
}