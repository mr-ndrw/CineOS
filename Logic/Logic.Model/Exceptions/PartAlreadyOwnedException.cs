using System;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Exceptions
{
	/// <summary>
	///		The exception that is thrown when an Owner object attempts to add a Part object 
	///		that is already composed into other Owner object.
	/// </summary>
	public class PartAlreadyOwnedException : Exception
	{
		public object OwnedPart { get; set; }

		/// <summary>
		///		Initializes a new instance of the PartAlreadyOwnedException class.
		/// </summary>
		public PartAlreadyOwnedException(object ownedPart)
			: base("The part object is already owned.")
		{
			OwnedPart = ownedPart;
		}

		/// <summary>
		///		Initializes a new instance of the PartAlreadyOwnedException class with a 
		///		reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="inner">
		///		The inner exception reference.
		/// </param>
		public PartAlreadyOwnedException(Exception inner)
			: base("The part object is already owned.", inner)
		{
			
		}
	}
}