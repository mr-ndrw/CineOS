using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    public class ProjectionController : CineOsController
    {
        [Route("api/projection/{idProjection}/seats")]
        [HttpGet]
        public IEnumerable<IEnumerable<SeatViewModel>> ShowSeatsWithStatus(int idProjection)
        {
            return CineOsServices.GetSeatsWithStatusForProjection(idProjection);
        }
    }
}
