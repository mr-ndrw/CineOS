using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;
using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Core
{
    public class CineOsServices
    {
        /// <summary>
        ///     Returns the collection of all RegionViewModel which contains just Id and the Name of Regions.
        /// </summary>
        public IEnumerable<RegionViewModel> GetRegions()
        {
            return Region.Extent.Select(region => new RegionViewModel(region));
        }

        /// <summary>
        ///     Returns the collection of ViewModels of Cinema linked with specified RegionViewModel.
        /// </summary>
        public IEnumerable<CinemaViewModel> GetCinemasFor(RegionViewModel regionViewModel)
        {
            if (regionViewModel == null) throw new ArgumentNullException("regionViewModel");
            //  Find Region which has the Id as the specified ViewModel.
            var region = Region.Extent.FirstOrDefault(rgn => rgn.Id == regionViewModel.Id);

            //  If no such region was found, return an empty collection.
            return region == null
                ? new List<CinemaViewModel>()
                : region.Cinemas.Select(cinema => new CinemaViewModel(cinema));
        }

        /// <summary>
        ///     Returns the collection of FilmViewModels for Films that will be viewed in the specified CinemaViewModel for Cinema.
        /// </summary>
        public IEnumerable<FilmViewModel> GetFilmsViewedIn(CinemaViewModel cinemaViewModel)
        {
            if (cinemaViewModel == null) throw new ArgumentNullException("cinemaViewModel");
            var cinema = Cinema.Extent.FirstOrDefault(cnm => cnm.Id == cinemaViewModel.Id);

            return cinema == null
                ? new List<FilmViewModel>()
                : cinema.FilmsThatWillBeViewed.Select(film => new FilmViewModel(film));
        }

        /// <summary>
        ///     Returns the collection of ProjectionViewModels for Projections for specified FilmViewModel for Film in specified CinemaViewModel for Cinema.
        /// </summary>
        public IEnumerable<ProjectionViewModel> GetProjectionsInRangeFor(CinemaViewModel cinemaViewModel, FilmViewModel filmViewModel, DateTime from,
            DateTime to)
        {
            if (cinemaViewModel == null) throw new ArgumentNullException("cinemaViewModel");
            if (filmViewModel == null) throw new ArgumentNullException("filmViewModel");

            var cinema = Cinema.Extent.FirstOrDefault(cnm => cnm.Id == cinemaViewModel.Id);
            var film = Film.Extent.FirstOrDefault(flm => flm.Id == filmViewModel.Id);

            if (cinema == null || film == null)
            {
                return new List<ProjectionViewModel>();
            }

            return cinema.GetProjectionsFor(film, from, to).Select(projection => new ProjectionViewModel(projection));
        }


    }
}
