using System.Linq;
using NUnit.Framework;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Test.Logic.Model.Entity
{
	[TestFixture]
	public class CinemaTest
	{
		[Test]
		public void Test_Linking_Cinemas_And_Regions()
		{
			//	Arrange
			var associationName = System.Reflection.MethodBase.GetCurrentMethod()
				.Name;

			//	Todo Overload RegisterComposition<,> with default amount boundaries.
			BusinessObject.RegisterComposition<Region, Cinema>(associationName, 0, int.MaxValue);

			Cinema.CinemaToRegionAssociationName = Region.RegionToCinemaAssociationName = associationName;

			var region1 = new Region() {Name = "Region1"};
			var region2 = new Region() { Name = "Region2" };

			//	Act
			var cinema1 = new Cinema(region1);
			var cinema2 = new Cinema(region1);
			var cinema3 = new Cinema(region2);
			var cinema4 = new Cinema(region2);

			//	Assert

			var linkedCinemasWithRegion1 = region1.Cinemas;
			var linkedCinemasWithRegion2 = region2.Cinemas;

			var cinema1LinkedRegion = cinema1.Region;
			var cinema2LinkedRegion = cinema2.Region;
			var cinema3LinkedRegion = cinema3.Region;
			var cinema4LinkedRegion = cinema4.Region;

			Assert.NotNull(linkedCinemasWithRegion1);
			Assert.NotNull(linkedCinemasWithRegion2);

			Assert.IsTrue(linkedCinemasWithRegion1.Contains(cinema1));
			Assert.IsTrue(linkedCinemasWithRegion1.Contains(cinema2));

			Assert.That(cinema1LinkedRegion, Is.EqualTo(region1));
			Assert.That(cinema2LinkedRegion, Is.EqualTo(region1));
			Assert.That(cinema3LinkedRegion, Is.EqualTo(region2));
			Assert.That(cinema4LinkedRegion, Is.EqualTo(region2));
		}
	}
}