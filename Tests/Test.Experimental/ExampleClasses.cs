using System.Collections.Generic;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Test.Experimental
{
	#region Qualified Association Exemplary Classes
	public class Identifier : AssociatedObject
	{
		public string Name { get; set; }
	}

	public class Identifiable : AssociatedObject
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	public class Qualifier
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	public class QulifierEqualityComparer : IEqualityComparer<Qualifier>
	{
		public bool Equals(Qualifier x, Qualifier y)
		{
			return x.X == y.X && x.Y == y.Y;
		}

		public int GetHashCode(Qualifier obj)
		{
			unchecked
			{
				var hash = 17;
				hash *= 23 + obj.X;
				hash *= 29 + obj.Y;
				return hash;
			}
		}
	} 
	#endregion

	#region Composition Exemplary Classes

	public class Owner : AssociatedObject
	{
		
	}

	public class Part : AssociatedObject
	{
		
	}

	#endregion

	#region Attribute Association Exmplary Classes

	public class TestClass1 : AssociatedObject {}
	public class TestClass2 : AssociatedObject {}
	public class AttributeExample {}

	#endregion
}