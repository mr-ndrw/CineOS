using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;
using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    public class CinemaController : CineOsController
    {
        [Route("api/cinema/{idCinema}/newfilms")]
        [HttpGet]
        public IEnumerable<FilmViewModel> GetFilmsViewedIn(int idCinema)
        {
            return CineOsServices.GetFilmsViewedIn(new CinemaViewModel {Id = idCinema});
        }

        [Route("api/cinemas")]
        [HttpGet]
        public IEnumerable<CinemaViewModel> GetCinemas()
        {
            return Cinema.Extent.Select(cinema => new CinemaViewModel(cinema));
        } 
    }
}