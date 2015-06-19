using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    [EnableCors("*", "*", "*")]
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
        //[AcceptVerbs("OPTIONS")]
        
        public void CreateReservation([FromBody] PostReservationViewModel reservation)
        {
            CineOsServices.CreateReservation(reservation);

            //Save();
        }

        private static void Save()
        {
            var path =
                @"C:\Users\Andrew\Documents\Visual Studio 2013\Projects\CineOS\Client\CineOsWebApiService\persistance.xml";

            WebApiApplication.WriteExtents(path, BusinessObject.DictionaryContainer);
        }
    }
}
