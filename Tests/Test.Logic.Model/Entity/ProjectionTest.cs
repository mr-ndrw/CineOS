using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using en.AndrewTorski.CineOS.Logic.Core;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.EntityHelpers;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Logic.Model.Entity
{
    [TestFixture]
	public class ProjectionTest
	{
        [Test]
        public void TestGetSeatsWithStatusForProjection_AllVacant()
        {
            //  Arrange
            #region Association Names

            var cinemaregionassociation = "CRA";
            var cinemaprojectionroomassociation = "CPRA";
            var projectionroomprojectionassociation = "PRPA";
            var projectionroomseatassociation = "PRSA";
            var projectionreservationassociation = "PRA";

            var clientreservationassociation = "CLRA";
            var reservationseatsassociation = "RSA";
            var filmprojectionassociation = "FPA";

            #endregion

            #region Association Registration

            BusinessObject.RegisterAssociation<Region, Cinema>(cinemaregionassociation);
            BusinessObject.RegisterQualifiedAssociation<Cinema, ProjectionRoom, ProjectionRoomQualifier>(cinemaprojectionroomassociation, 20, 20, ProjectionRoomQualifier.EqualityComparer);
            BusinessObject.RegisterQualifiedAssociation<ProjectionRoom, Seat, SeatQualifier>(projectionroomseatassociation, 20, 20, SeatQualifier.EqualityComparer);
            BusinessObject.RegisterAssociation<Projection, Reservation>(projectionreservationassociation);

            BusinessObject.RegisterAssociation<ProjectionRoom, Projection>(projectionroomprojectionassociation);
            BusinessObject.RegisterAssociation<Seat, Reservation>(reservationseatsassociation);
            BusinessObject.RegisterAssociation<Client, Reservation>(clientreservationassociation);
            BusinessObject.RegisterAssociation<Projection, Film>(filmprojectionassociation);

            #endregion

            #region Association name injection
            Film.FilmToProjectionAssociationName =
                    Projection.ProjectionToFilmAssociationName = filmprojectionassociation;

            Region.RegionToCinemaAssociationName =
                Cinema.CinemaToRegionAssociationName = cinemaregionassociation;

            Cinema.CinemaToProjectionRoomAssociationName =
                ProjectionRoom.ProjectionRoomToCinemaAssociationName = cinemaprojectionroomassociation;

            ProjectionRoom.ProjectionRoomToProjectionAssociationName =
                Projection.ProjectionToProjectionRoomAssociationName = projectionroomprojectionassociation;

            ProjectionRoom.ProjectionRoomToSeatAssociationName =
                Seat.SeatToProjectionRoomAssociationName = projectionroomseatassociation;

            Reservation.ReservationToSeatAssociationName =
                Seat.SeatToReservationAssociationName = reservationseatsassociation;

            Reservation.ReservationToProjectionAssociationName =
                Projection.ProjectionToReservationAssociationName = projectionreservationassociation;

            Reservation.ReserevationToClientAssociationName =
                Client.ClientToReserevationAssociationName = clientreservationassociation;
            #endregion

            var region = new Region();
            var cinema = new Cinema(region) { Name = "Cinema", Address = "Address" };
            var projectionRoom = new ProjectionRoom("1", cinema);
            var seat = new Seat("1", "1", projectionRoom);
            var seat2 = new Seat("1", "2", projectionRoom);
            var seat3 = new Seat("2", "1", projectionRoom);
            var seat4 = new Seat("2", "2", projectionRoom);
            var creds = new PersonCredentials { Email = "Email", Name = "Name", Surname = "Surname", TelephoneNo = "Tel" };
            var client = Person.CreateClient(creds, "password").Client;
            var film = new Film();
            var projection = new Projection(projectionRoom, film, new DateTime(2015, 12, 15));
            projectionRoom.ColumnCount = 2;
            projectionRoom.RowCount = 2;
            var cineOsServices= new CineOsServices();



            var reservation = new Reservation(client, projection, seat);

            //  Act
            var foundSeats = (List<List<SeatViewModel>>)cineOsServices.GetSeatsWithStatusForProjection(projection.Id);

            //  Assert
            Assert.That(foundSeats.Count, Is.EqualTo(2));
            var foundSeat11 = foundSeats[0][0];
            var foundSeat12 = foundSeats[0][1];
            var foundSeat21 = foundSeats[1][0];
            var foundSeat22 = foundSeats[1][1];

            Assert.AreEqual(foundSeat11.Id, seat.Id);
            Assert.True(foundSeat11.Reserved);

            Assert.AreEqual(foundSeat12.Id, seat2.Id);
            Assert.False(foundSeat12.Reserved);

            Assert.AreEqual(foundSeat21.Id, seat3.Id);
            Assert.False(foundSeat21.Reserved);

            Assert.AreEqual(foundSeat22.Id, seat4.Id);
            Assert.False(foundSeat22.Reserved);


        }

	}
}
