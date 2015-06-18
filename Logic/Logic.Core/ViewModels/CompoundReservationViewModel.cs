using System;
using System.Linq;
using System.Text.RegularExpressions;
using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Core.ViewModels
{
    public class CompoundReservationViewModel
    {
        public CompoundReservationViewModel(Reservation reservation)
        {
            ClientEmail = Regex.Replace(reservation.Client.Person.Email, @"\s+", "");
            ReservationDateTime = reservation.DateTime;
            ProjectionRoomNumber = reservation.Projection.ProjectionRoom.Number;
            CinemaName = reservation.Projection.Cinema.Name;
            CinemaAddress = reservation.Projection.Cinema.Address;
            var seat = reservation.ReservedSeats.FirstOrDefault();
            RowNumber = seat.RowNumber;
            ColumnNumber = seat.ColumnNumber;
        }

        public string ClientEmail { get; set; }
        public DateTime ReservationDateTime { get; set; }
        public string ProjectionRoomNumber { get; set; }
        public string CinemaName { get; set; }
        public string CinemaAddress { get; set; }
        public string RowNumber { get; set; }
        public string ColumnNumber { get; set; }
    }
}