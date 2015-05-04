using System;
using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.Association
{
	public abstract class RecurrentStandardAssociationBase : AssociationBase
	{
		private RecurrentStandardAssociationBase(Type type1, Type type2, string name, int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary, int partLowerAmountBoundary, int secondTypeUpperAmountBoundary) 
			: base(type1, type2, name, firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary, partLowerAmountBoundary, secondTypeUpperAmountBoundary)
		{
		}

		public abstract List<object> GetAssociatedObjects(object obj);

		public abstract void Link(object firstObject, object secondObject);
	}
}