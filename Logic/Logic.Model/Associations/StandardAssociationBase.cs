using System;
using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	public abstract class StandardAssociationBase : AssociationBase
	{
		protected StandardAssociationBase(Type type1, Type type2, string name, int lowerBoundForFirstType, int upperBoundForFirstType, int lowerBoundForSecondType, int upperBoundForSecondType) : base(type1, type2, name, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType)
		{
		}

		/// <summary>
		///		Returns the collection of objects assocaited with provided object.
		/// </summary>
		/// <param name="obj">
		///		Object for which we retrieve linked objects.
		/// </param>
		/// <returns>
		///		Collection of objects.
		/// </returns>
		/// <remarks>
		///		You may provide either of the types as the parametrized object. Method will recognize
		///		what type was provided(and whether it is conforming with the association.
		/// 
		///		If the provided object doesn't exist in the association's, method will return an empty collection.
		/// </remarks>
		public abstract List<object> GetAssociatedObjects(object obj);

		/// <summary>
		///		Creates a link between two objects within this association.
		/// </summary>
		/// <param name="firstObject">
		///		First object.
		/// </param>
		/// <param name="secondObject">
		///		Second object.
		/// </param>
		public abstract void Link(object firstObject, object secondObject);
	}
}