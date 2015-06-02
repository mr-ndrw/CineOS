using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Experimental.Associated_Object
{
	[TestFixture]
	public class AssociatedObjectCompositionTest
	{
		[Test]
		public void Test_Registering_Compositon()
		{
			//	Arrange & Act
			const string compositionName = @"Test_Registering_Compositon";
			BusinessObject.RegisterComposition<Owner, Part>(compositionName, 0, int.MaxValue);
			//	Assert
			Assert.IsTrue((BusinessObject.ContainsAssociation(compositionName)));
		}

		[Test]
		public void Test_Adding_Parts_To_Owner()
		{
			//	Arrange
			const string associationName = "Test_Adding_Parts_To_Owner";
			var owner = new Owner();
			Part part1 = new Part(), part2 = new Part(), part3 = new Part();

			var partList = new List<Part>() { part1, part2, part3 };

			//	Register composition with 0..3 bounadaries on Part's side.
			BusinessObject.RegisterComposition<Owner, Part>(associationName, 0, 3);

			//	Act
			owner.Link(associationName, part1);
			owner.Link(associationName, part2);
			//	Change ordering - link part with the owner.
			part3.Link(associationName, owner);

			//	Assert
			var foundParts = owner.GetLinkedObjects(associationName);

			Assert.IsTrue(foundParts.SequenceEqual(partList));

		}

	}
}