using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using en.AndrewTorski.CineOS.Logic.Model.Associations;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Experimental
{
	[TestFixture]
	public class CompositionTest
	{

		[Test]
		public void Test_If_Parts_Can_Be_Added_To_Owner()
		{
			//	Arrange
			const string associationName = "Test_If_Parts_Can_Be_Added_To_Owner";
			var owner = new Owner();
			Part part1 = new Part(), part2 = new Part(), part3 = new Part();

			var partList = new List<Part>() {part1, part2, part3};

			//	Create composition with 0..3 bounadaries on Part's side.
			var composition = new Composition<Owner, Part>(associationName, 0, 3);

			//	Act
			composition.Link(owner, part1);
			composition.Link(owner, part2);
			composition.Link(owner, part3);

			//	Assert
			var foundParts = composition.GetAssociatedObjects(owner);

			Assert.IsTrue(foundParts.SequenceEqual(partList));
		}

		[Test]
		public void Test_If_Part_Cant_Be_Added_To_Different_Owners()
		{
			const string associationName = @"Test_If_Part_Can_Be_Added_To_Different";
			//	Arrange
			Owner owner1 = new Owner(), owner2 = new Owner();
			var part = new Part();

			var composition = new Composition<Owner, Part>(associationName, 0, int.MaxValue);

			var wasPartAlreadyOwnedExceptionThrown = false;

			//	Act
			composition.Link(owner1, part);
			try
			{
				composition.Link(owner2, part);
			}
			catch (PartAlreadyOwnedException)
			{
				wasPartAlreadyOwnedExceptionThrown = true;
			}

			//	Assert
			Assert.IsTrue(wasPartAlreadyOwnedExceptionThrown, "Part was linked to Owners.");
		}

		[Test]
		public void Test_If_Part_Cant_Be_Added_Twice_To_Same_Owner()
		{
			const string associationName = @"Test_If_Part_Cant_Be_Added_Twice_To_Same_Owner";
			//	Arrange
			var owner = new Owner();
			var part = new Part();
			var composition = new Composition<Owner, Part>(associationName, 0, 3);

			var wasPartAlreadyOwnedExceptionThrown = false;

			//	Act
			composition.Link(owner, part);
			try
			{
				composition.Link(owner, part);
			}
			catch (PartAlreadyOwnedException)
			{
				wasPartAlreadyOwnedExceptionThrown = true;
			}

			//	Assert
			Assert.IsTrue(wasPartAlreadyOwnedExceptionThrown, "Part was double linked to the same Owner.");



		}
	}
}