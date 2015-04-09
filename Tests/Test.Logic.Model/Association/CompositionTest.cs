using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Logic.Model.Association
{
	/// <summary>
	///		Unit test for composition functionality.
	/// </summary>
	[TestFixture]
	public class CompositionTest
	{
		[Test]
		public void AddPartToOwnersComposition()
		{
			//	Arrange
			var region1 = new Region();
			var region2 = new Region();

			//	Act
			//	Addition to Owner's(in this case any Region) composition is done implicitly inside
			//	the constructor of the Part class.
			var cinema1 = new Cinema(region1);
			var cinema2 = new Cinema(region2);
			
			//	Assert
			var cinema1Owner = cinema1.Region;
			var cinema2Owner = cinema2.Region;

			var cinemasOwnedByRegion1 = region1.Cinemas;
			var cinemasOwnedByRegion2 = region2.Cinemas;

			Assert.That(cinema1Owner, Is.EqualTo(region1));
			Assert.That(cinema2Owner, Is.EqualTo(region2));

			Assert.That(cinemasOwnedByRegion1.Contains(cinema1));
			Assert.That(cinemasOwnedByRegion2.Contains(cinema2));
		}

		/// <summary>
		///		Tests whether a owned Part object may be added to other Owner object.
		/// </summary>
		[Test]
		public void AddPartToOwnerAndCheckIfPartCanBeAddedToOtherOwner()
		{
			/* 
			 *	The convention for creating Part objects in Composition is as follows:
			 *	If we want to create any object of class that was identified as a part object, we must supply
			 *	the object of the Owner in every of the Part class constructor. That way we ensure that a Part
			 *	object may not exist without a Owner object.
			 *	
			 *	This is best illustrated by the following example. A Region is composed of one-to-many Cinemas.
			 *	Cinema class constructor expects a Region object reference. During the creation of the Cinema
			 *	object, a call to AddCinema(Cinema) method of Region is made, which in turn calls 
			 *	AddPart(Association,Association,ObjectWithAssociation) of ObjectWithAssociation - the base class
			 *	for all business classes. 
			 *	
			 */
			//	Arrange
			Region region1 = new Region(), region2 = new Region();
			var cinema1 = new Cinema(region1);

			//	Act
			var partAlreadyOwnedExceptionWasThrown = false;
			try
			{
				//region2.AddCinema(cinema1);
			}
			catch (PartAlreadyOwnedException e)
			{
				partAlreadyOwnedExceptionWasThrown = true;
			}

			//	Assert
			Assert.That(partAlreadyOwnedExceptionWasThrown);
		}
	}
}