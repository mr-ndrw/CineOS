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

        public IEnumerable<CinemaViewModel> GetCinemas()
        {
            return Cinema.Extent.Select(cinema => new CinemaViewModel(cinema));
        } 

        /// <summary>
        ///     Returns the collection of ViewModels of Cinema linked with specified RegionViewModel.
        /// </summary>
        public IEnumerable<CinemaViewModel> GetCinemasFor(int idRegion)
        {
            //  Find Region which has the Id as the specified ViewModel.
            var region = Region.Extent.FirstOrDefault(rgn => rgn.Id == idRegion);

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
        public IEnumerable<ProjectionViewModel> GetProjectionsInRangeFor(int idCinema, int idFilm, DateTime from, DateTime to)
        {
            var cinema = Cinema.Extent.FirstOrDefault(cnm => cnm.Id == idCinema);
            var film = Film.Extent.FirstOrDefault(flm => flm.Id == idFilm);

            if (cinema == null || film == null)
            {
                return new List<ProjectionViewModel>();
            }

            return cinema.GetProjectionsFor(film, from, to).Select(projection => new ProjectionViewModel(projection));
        }

        public IEnumerable<IEnumerable<SeatViewModel>> GetSeatsWithStatusForProjection(int idProjection)
        {
            var projection = Projection.Extent.FirstOrDefault(proj => proj.Id == idProjection);

            if (projection == null)
            {
                return new List<List<SeatViewModel>>();
            }

            var seatsInProjectionRoom = projection.GetSeatsInProjectionRoom();
            var seatsReserved = projection.GetSeatsReserved().ToList();

            var midresult = seatsInProjectionRoom.Select(seat => new SeatViewModel(seat, seatsReserved.Any(seatReserved => seat.Id == seatReserved.Id)));

            /*foreach (var seatViewModel in midresult)
            {
                bool isReserved = seatsReserved.Any(seat => seat.Id == seatViewModel.Id);
                seatViewModel.Reserved = isReserved;

            }*/

            return SortSeats(projection, midresult);
        }

        private IEnumerable<IEnumerable<SeatViewModel>> SortSeats(Projection projection, IEnumerable<SeatViewModel> seats)
        {
            var rowCount = projection.ProjectionRoom.RowCount;
            var columnCount = projection.ProjectionRoom.ColumnCount;

            var query = seats.OrderBy(seat => seat.RowColumn).ToList();

            var rows = new List<List<SeatViewModel>>();

            for (int i = 0; i < columnCount; i++)
            {
                var take = query.Take(rowCount).ToList();
                query.RemoveRange(0, rowCount);
                rows.Add(take);
            }

            return rows;
        }

        /// <summary>
        ///     Checks if a Client of provided email and password exists in the system.
        ///     If such client was found, method will return this Client's Id number.
        ///     If not, it will return -1;
        /// </summary>
        public int DoesClientExist(string email, string password)
        {
            return Client.Exists(email, password);
        }

        /// <summary>
        ///     Returns all Reservations within the system.
        /// </summary>
        public IEnumerable<CompoundReservationViewModel> GetReservations()
        {
            var reservations = Reservation.Extent;

            return reservations.Select(res => new CompoundReservationViewModel(res));
        }

        public void CreateReservation(PostReservationViewModel reservation)
        {
            var foundClient = Client.Extent.FirstOrDefault(client => client.Person.Id == reservation.ClientId );
            var foundProjection =
                Projection.Extent.FirstOrDefault(projection => projection.Id == reservation.ProjectionId);
            var foundSeat = Seat.Extent.FirstOrDefault(seat => seat.Id == reservation.SeatId);

            if (foundSeat == null) throw new ArgumentNullException("foundSeat");
            if (foundClient == null) throw new ArgumentNullException("foundClient");
            if (foundProjection == null) throw new ArgumentNullException("foundProjection");

            new Reservation(foundClient, foundProjection, foundSeat);
        }

    }
}
