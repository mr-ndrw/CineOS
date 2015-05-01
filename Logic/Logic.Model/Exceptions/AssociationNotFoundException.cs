using System;

namespace en.AndrewTorski.CineOS.Logic.Model.Exceptions
{
	/// <summary>
	///		The exception that is thrown when Association was not found in the Associations dictionary.
	/// </summary>
	public class AssociationNotFoundException : Exception
	{
		/// <summary>
		///		Initializes a new instance of the AssociationNotFoundException class with a
		///		name of the association that was not found.
		/// </summary>
		public AssociationNotFoundException(string associationName)
			: base(string.Format("Association of name {0} was not found.", associationName))
		{
			
		}
	}
}