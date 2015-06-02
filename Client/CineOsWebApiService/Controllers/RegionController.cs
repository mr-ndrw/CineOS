using System.Collections.Generic;
using System.Web.Http;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    public class RegionController : CineOsController
    {

        [Route("api/regions")]
        [HttpGet]
        public IEnumerable<RegionViewModel> GetRegions()
        {
            var result = CineOsServices.GetRegions();
            return result;
        }

        [Route("api/region/{id}/cinemas")]
        [HttpGet]
        public IEnumerable<CinemaViewModel> GetCinemasFor(int id)
        {
            var result = CineOsServices.GetCinemasFor(id);
            return result;
        }
        
    }
}
