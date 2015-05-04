using System;
using System.Runtime.InteropServices;
using en.AndrewTorski.CineOS.Logic.Model.Association;

namespace en.AndrewTorski.CineOS.Logic.Model.Exceptions
{
	/// <summary>
	///		An exception that is thrown when an attempt is made to link an Identifiable to Identifier using a Qualifier
	///		when the UpperAmount on the side of the Identifiable is equal to 1.
	/// </summary>
	public class InvalidQualifiedLinkingOperationException : Exception
	{
		/// <summary>
		///		Initializes a new instance of the InvalidQualifiedLinkingOperationException class along with a reference to the association in which
		///		it was thrown.
		/// </summary>
		/// <param name="association"></param>
		public InvalidQualifiedLinkingOperationException(AssociationBase association)
		{
			Association = association;
		}

		/// <summary>
		///		Association in which the Exception was thrown.
		/// </summary>
		public AssociationBase Association { get; set; }
	}
}