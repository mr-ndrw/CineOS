using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.EntityHelpers;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Logic.Model.Entity
{
	[TestFixture]
	public class ProjectionRoomTest
	{
		const string regionToCinemaName = "CR";
		const string cinemaToProjectionRoomName = "CPR";
		const string projectionRoomToSeatName = "PRS";

		[TestFixtureSetUp]
		public void setup()
		{
			Region.RegionToCinemaAssociationName = Cinema.CinemaToRegionAssociationName = regionToCinemaName;
			Cinema.CinemaToProjectionRoomAssociationName = ProjectionRoom.ProjectionRoomToCinemaAssociationName = cinemaToProjectionRoomName;
			ProjectionRoom.ProjectionRoomToSeatAssociationName = Seat.SeatToProjectionRoomAssociationName = projectionRoomToSeatName;
			AssociatedObject.RegisterComposition<Region, Cinema>(regionToCinemaName, 0, int.MaxValue);
			AssociatedObject.RegisterQualifiedAssociation<Cinema, ProjectionRoom, ProjectionRoomCoordinates>(cinemaToProjectionRoomName, 0, 1, 0, 1, ProjectionRoomCoordinates.EqualityComparer);
			AssociatedObject.RegisterQualifiedAssociation<ProjectionRoom, Seat, SeatQualifier>(projectionRoomToSeatName, 1, 1, SeatQualifier.EqualityComparer);
		}


		[Test]
		public void Test_If_Projection_Rooms_Are_Added_Correctly_To_Cinemas()
		{
			//	Arrange


			var region = new Region();

			var cinema1 = new Cinema(region);
			var cinema2 = new Cinema(region);

			//	Act
			var projectionRoom1 = new ProjectionRoom("1", cinema1);
			var projectionRoom2 = new ProjectionRoom("2", cinema1);
			var projectionRoom3 = new ProjectionRoom("3", cinema1);

			var projectionRoom4 = new ProjectionRoom("1", cinema2);
			var projectionRoom5 = new ProjectionRoom("2", cinema2);
			var projectionRoom6 = new ProjectionRoom("3", cinema2);

			var projectionRoomListForCinema1 = new List<ProjectionRoom>() {projectionRoom1, projectionRoom2, projectionRoom3};
			var projectionRoomListForCinema2 = new List<ProjectionRoom>() { projectionRoom4, projectionRoom5, projectionRoom6 };

			//	Assert

			var retrievedProjectionRoomsForCinema1 = cinema1.ProjectionRooms;
			var retrievedProjectionRoomsForCinema2 = cinema2.ProjectionRooms;

			Assert.True(projectionRoomListForCinema1.SequenceEqual(retrievedProjectionRoomsForCinema1));
			Assert.True(projectionRoomListForCinema2.SequenceEqual(retrievedProjectionRoomsForCinema2));

			var retrievedProjectionRoomNumber1ForCinema1 = cinema1.GetProjectionRoom("1");
			var retrievedProjectionRoomNumber2ForCinema1 = cinema1.GetProjectionRoom("2");
			var retrievedProjectionRoomNumber3ForCinema1 = cinema1.GetProjectionRoom("3");

			var retrievedProjectionRoomNumber1ForCinema2 = cinema2.GetProjectionRoom("1");
			var retrievedProjectionRoomNumber2ForCinema2 = cinema2.GetProjectionRoom("2");
			var retrievedProjectionRoomNumber3ForCinema2 = cinema2.GetProjectionRoom("3");

			Assert.That(projectionRoom1, Is.EqualTo(retrievedProjectionRoomNumber1ForCinema1));
			Assert.That(projectionRoom2, Is.EqualTo(retrievedProjectionRoomNumber2ForCinema1));
			Assert.That(projectionRoom3, Is.EqualTo(retrievedProjectionRoomNumber3ForCinema1));

			Assert.That(projectionRoom4, Is.EqualTo(retrievedProjectionRoomNumber1ForCinema2));
			Assert.That(projectionRoom5, Is.EqualTo(retrievedProjectionRoomNumber2ForCinema2));
			Assert.That(projectionRoom6, Is.EqualTo(retrievedProjectionRoomNumber3ForCinema2));
		}

		[Test]
		public void Test_If_Seats_And_Projection_Rooms_Are_Linked_Correctly()
		{
			//	Arrange
			var region1 = new Region();
			var cinema1 = new Cinema(region1);
			var projectionRoom1 = new ProjectionRoom("3", cinema1);
			var projectionRoom2 = new ProjectionRoom("4", cinema1);

			//	Act
			var seat0 = new Seat("1", "1", projectionRoom1);
			var seat1 = new Seat("1", "2", projectionRoom1);
			var seat2 = new Seat("1", "3", projectionRoom1);
			var seat3 = new Seat("1", "4", projectionRoom1);
			var seat4 = new Seat("1", "5", projectionRoom1);

			var seat5 = new Seat("1", "1", projectionRoom2);
			var seat6 = new Seat("1", "2", projectionRoom2);
			var seat7 = new Seat("1", "3", projectionRoom2);
			var seat8 = new Seat("1", "4", projectionRoom2);
			var seat9 = new Seat("1", "5", projectionRoom2);

			var projectionRoom1SeatList = new List<Seat>() {seat0, seat1, seat2, seat3, seat4};
			var projectionRoom2SeatList = new List<Seat>() { seat5, seat6, seat7, seat8, seat9};

			//	Assert

			var retrievedSeatsFroProjectionRoom1 = projectionRoom1.Seats;
			var retrievedSeatsFroProjectionRoom2 = projectionRoom2.Seats;

			Assert.True(projectionRoom1SeatList.SequenceEqual(retrievedSeatsFroProjectionRoom1));
			Assert.True(projectionRoom2SeatList.SequenceEqual(retrievedSeatsFroProjectionRoom2));
		}

		[Test]
		public void Test_Retrieving_Seats_By_Their_Identifier()
		{
			var region1 = new Region();
			var cinema1 = new Cinema(region1);
			var projectionRoom1 = new ProjectionRoom("5", cinema1);
			var projectionRoom2 = new ProjectionRoom("6", cinema1);

			//	Act
			var seat0 = new Seat("1", "1", projectionRoom1);
			var seat1 = new Seat("1", "2", projectionRoom1);

			var seat5 = new Seat("1", "1", projectionRoom2);
			var seat6 = new Seat("1", "2", projectionRoom2);

			//	Assert

			var retrievedSeat11ForProjectionRoom1 = projectionRoom1.GetSeat("1", "1");
			var retrievedSeat12ForProjectionRoom1 = projectionRoom1.GetSeat("1", "2");
			var notFoundForProjectionRoom1 = projectionRoom1.GetSeat("2", "1");


			var retrievedSeat11ForProjectionRoom2 = projectionRoom2.GetSeat("1", "1");
			var retrievedSeat12ForProjectionRoom2 = projectionRoom2.GetSeat("1", "2");
			var notFoundForProjectionRoom2 = projectionRoom1.GetSeat("2", "1");

			Assert.That(retrievedSeat11ForProjectionRoom1, Is.EqualTo(seat0));
			Assert.That(retrievedSeat12ForProjectionRoom1, Is.EqualTo(seat1));
			Assert.IsNull(notFoundForProjectionRoom1);

			Assert.That(retrievedSeat11ForProjectionRoom2, Is.EqualTo(seat5));
			Assert.That(retrievedSeat12ForProjectionRoom2, Is.EqualTo(seat6));
			Assert.IsNull(notFoundForProjectionRoom2);
		}
	}
}
