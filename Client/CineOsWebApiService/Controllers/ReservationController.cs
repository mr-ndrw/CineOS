using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    public class ReservationController : CineOsController
    {
        [Route("api/reservations")]
        [HttpGet]

        public IEnumerable<CompoundReservationViewModel> GetReservations()
        {
            return CineOsServices.GetReservations();
        }

        [Route("api/reservation")]
        [HttpPost]
        public void CreateReservation(PostReservationViewModel reservation)
        {
            CineOsServices.CreateReservation(reservation);
        }
    }
}
