using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace en.AndrewTorski.CineOS.Logic.Model.Association
{
	[DataContract]
	public abstract class StandardAssociationBase : AssociationBase
	{
		protected StandardAssociationBase(Type type1, Type type2, string name, int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary, int secondTypeLowerAmountBoundary, int secondTypeUpperAmountBoundary) 
			: base(type1, type2, name, firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary, secondTypeLowerAmountBoundary, secondTypeUpperAmountBoundary)
		{
		}

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