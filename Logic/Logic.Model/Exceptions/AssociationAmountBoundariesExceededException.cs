using System;
using en.AndrewTorski.CineOS.Logic.Model.Association;

namespace en.AndrewTorski.CineOS.Logic.Model.Exceptions
{
	/// <summary>
	///		The exception that is thrown when upon adding a link or removing a link between objects
	///		amount boundaries are exceeded.
	/// </summary>
	public class AssociationAmountBoundariesExceededException : Exception
	{
		/// <summary>
		///		Initializes a new instance of the AssociationAmountBoundariesExceededException class with a
		///		reference to the Association in which it was thrown and the new amount values which exceed referenced
		///		Association amount boundaries.
		/// </summary>
		public AssociationAmountBoundariesExceededException(AssociationBase association, int firstTypeNewAmountValue, int secondTypeNewAmountValue)
			: base(string.Format("Boundaries were exceeded for Association: '{0}'", association.Name))
		{
			Association = association;
			FirstTypeNewAmountValue = firstTypeNewAmountValue;
			SecondTypeNewAmountValue = secondTypeNewAmountValue;
		}

		/// <summary>
		///		Association in which this AssociationAmountBoundariesExceededException was thrown. 
		/// </summary>
		public AssociationBase Association { get; private set; }

		/// <summary>
		///		Value of new amount on the side of First Type which exceeded referenced Association's amount boundaries.
		/// </summary>
		public int FirstTypeNewAmountValue { get; private set; }

		/// <summary>
		///		Value of new amount on the side of Second Typed which exceeded referenced Association's amount boundaries.
		/// </summary>
		public int SecondTypeNewAmountValue { get; private set; }

	}
}