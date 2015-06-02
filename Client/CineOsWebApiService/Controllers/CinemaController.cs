using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;
using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    public class CinemaController : CineOsController
    {
        [Route("api/cinemas")]
        [HttpGet]
        public IEnumerable<CinemaViewModel> GetCinemas()
        {
            return CineOsServices.GetCinemas();
        }

        [Route("api/cinema/{idCinema}/newfilms")]
        [HttpGet]
        public IEnumerable<FilmViewModel> GetFilmsViewedIn(int idCinema)
        {
            return CineOsServices.GetFilmsViewedIn(new CinemaViewModel {Id = idCinema});
        }

        [Route("api/cinema/{idCinema}/film/{idFilm}/{dateFrom}/{dateTo}")]
        [HttpGet]
        public IEnumerable<ProjectionViewModel> GetProjectionsInRangeFor(int idCinema, int idFilm, DateTime dateFrom,
            DateTime dateTo)
        {
            return CineOsServices.GetProjectionsInRangeFor(idCinema, idFilm, dateFrom, dateTo);
        } 
    }
}