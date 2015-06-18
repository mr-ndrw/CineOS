using System;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.EntityHelpers;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Logic.Model.Entity
{
    [TestFixture]
	public class ReservationTest
	{
        [Test]
        public void TestCompoundResevationViewModel()
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
            var cinema  = new Cinema(region){Name = "Cinema", Address = "Address"};
            var projectionRoom = new ProjectionRoom("1", cinema);
            var seat = new Seat("1", "1", projectionRoom);
            var creds = new PersonCredentials {Email = "Email", Name = "Name", Surname = "Surname", TelephoneNo = "Tel"};
            var client = Person.CreateClient(creds, "password").Client;
            var film = new Film();
            var projection = new Projection(projectionRoom, film, new DateTime(2015, 12, 15));
            var reservation = new Reservation(client, projection, seat);

            //  Act
            var compound = new CompoundReservationViewModel(reservation);

            //  Assert
            Assert.AreEqual(compound.ClientEmail, client.Person.Email);
            Assert.AreEqual(compound.RowNumber, seat.RowNumber);
            Assert.AreEqual(compound.ColumnNumber, seat.ColumnNumber);
            Assert.AreEqual(compound.CinemaName, cinema.Name);
            Assert.AreEqual(compound.ProjectionRoomNumber, projectionRoom.Number);
            Assert.AreEqual(compound.CinemaAddress, cinema.Address);
            Assert.AreEqual(compound.ReservationDateTime, reservation.DateTime);
        }
	}
}
