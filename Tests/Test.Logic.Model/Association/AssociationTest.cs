using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Logic.Model.Association
{
	[TestFixture]
	public class AssociationTest
	{
		[Test]
		public void AssociateTwoObjects()
		{
			//	Arrange
			var cinema1 = new Cinema(new Region());
			var cinema2 = new Cinema(new Region());

			//	Act
			Projection	projection1 = new Projection(cinema1), projection2 = new Projection(cinema1), projection3 = new Projection(cinema1),
						projection4 = new Projection(cinema2), projection5 = new Projection(cinema2), projection6 = new Projection(cinema2);

			//	Assert.
			var projectionList1 = new List<Projection> { projection1, projection2, projection3 };
			var projectionList2 = new List<Projection> { projection4, projection5, projection6 };

			var projectionListForCinema1 = cinema1.Projections;
			var projectionListForCinema2 = cinema2.Projections;

			Assert.That(projectionListForCinema1.SequenceEqual(projectionList1));
			Assert.That(projectionListForCinema2.SequenceEqual(projectionList2));
		}
	}
}
