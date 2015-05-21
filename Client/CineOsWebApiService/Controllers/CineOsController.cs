using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using en.AndrewTorski.CineOS.Logic.Core;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    public class CineOsController : ApiController
    {
        protected readonly CineOsServices CineOsServices;

        public CineOsController()
        {
            CineOsServices = new CineOsServices();
        }
    }
}
