using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace en.AndrewTorski.CineOS.Logic.Core.ViewModels
{
    /// <summary>
    ///     Contains most basic data about newly created Reservation.
    /// </summary>
    public class PostReservationViewModel
    {
        public int ProjectionId { get; set; }
        public int SeatId { get; set; }
        public int ClientId { get; set; }
    }
}
