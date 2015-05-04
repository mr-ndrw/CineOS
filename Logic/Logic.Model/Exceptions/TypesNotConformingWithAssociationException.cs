using System;
using en.AndrewTorski.CineOS.Logic.Model.Association;

namespace en.AndrewTorski.CineOS.Logic.Model.Exceptions
{
	/// <summary>
	///		Thrown when neither of the provided Types conform the types of Association.
	/// </summary>
	public class TypesNotConformingWithAssociationException : Exception
	{
		/// <summary>
		///		Initializes an new instance of the TypesNotConformingWithAssociationException class with a reference to Association 
		///		in which the Exception was thrown and two types which might not be conforming with those of the Association.
		/// </summary>
		public TypesNotConformingWithAssociationException(AssociationBase association, Type firstProvidedType, Type secondProvidedType)
			:base(string.Format("One of the types is not conforming with the '{0}' Association.", association.Name))
		{
			Association = association;
			FirstProvidedType = firstProvidedType;
			SecondProvidedType = secondProvidedType;
		}

		/// <summary>
		///		Association in which this Exception was thrown.
		/// </summary>
		public AssociationBase Association { get; set; }

		/// <summary>
		///		First provided Type which might not be conforming with association.
		/// </summary>
		public Type FirstProvidedType { get; set; }

		/// <summary>
		///		Second provided Type which might not be conforming with association.
		/// </summary>
		public Type SecondProvidedType { get; set; }
	}
}