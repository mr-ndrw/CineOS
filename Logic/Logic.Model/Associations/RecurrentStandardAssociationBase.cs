using System;
using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	public abstract class RecurrentStandardAssociationBase : AssociationBase
	{
		private RecurrentStandardAssociationBase(Type type1, Type type2, string name, int lowerBoundForFirstType, int upperBoundForFirstType, int lowerBoundForSecondType, int upperBoundForSecondType) 
			: base(type1, type2, name, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType)
		{
		}

		public abstract List<object> GetAssociatedObjects(object obj);

		public abstract void Link(object firstObject, object secondObject);
	}
}