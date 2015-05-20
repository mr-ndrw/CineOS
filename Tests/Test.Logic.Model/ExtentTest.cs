using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Logic.Model
{
	[TestFixture]
	public class ExtentTest
	{
		[Test]
		public void Test_If_Retrieval_Of_Class_Extent_Functions()
		{
			//	Arrange
			var region1 = new Region();
			var region2 = new Region();
			var region3 = new Region();
			var region4 = new Region();
			var region5 = new Region();

			var regionList = new List<Region>() {region1, region2, region3, region4, region5};

			//	Act

			var retrievedRegionList = Region.Extent;

			//	Assert
			Assert.IsTrue(retrievedRegionList.Intersect(retrievedRegionList).SequenceEqual(retrievedRegionList));
		}
	}
}