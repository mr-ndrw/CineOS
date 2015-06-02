using System;
using System.Web;
using System.Web.Http;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.EntityHelpers;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            PopulateSystemWithData();
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }

        private void PopulateSystemWithData()
        {
            var regionCinemaAssociationName = "RCC";
            Region.RegionToCinemaAssociationName = Cinema.CinemaToRegionAssociationName = regionCinemaAssociationName;
            BusinessObject.RegisterComposition<Region, Cinema>(regionCinemaAssociationName, 0, int.MaxValue);

            var region1 = new Region {Name = "Nice region"};
            var region2 = new Region {Name = "Best region"};
            var region3 = new Region {Name = "Smelly region"};
            var region4 = new Region {Name = "Hello region"};

            var cinema1 = new Cinema(region1){Address = "227 E 17th St, NYC", Name = "Odeon"};
            var cinema2 = new Cinema(region1) { Address = "223 Sunshine BLVD, LA", Name = "West Best Cinema" };
            var cinema3 = new Cinema(region2) { Address = "Placeholder", Name = "Hello3" };
            var cinema4 = new Cinema(region2) { Address = "Placeholder", Name = "Hello4" };
            var cinema5 = new Cinema(region3) { Address = "Placeholder", Name = "Hello5" };
            var cinema6 = new Cinema(region1) { Address = "Placeholder", Name = "Hello6" };
            var cinema7 = new Cinema(region4) { Address = "Placeholder", Name = "Hello7" };

            var cinemaProjectionRoomAssociatioName = "CPR";
            Cinema.CinemaToProjectionRoomAssociationName =
                ProjectionRoom.ProjectionRoomToCinemaAssociationName = cinemaProjectionRoomAssociatioName;

            BusinessObject.RegisterQualifiedAssociation<Cinema, ProjectionRoom, ProjectionRoomCoordinates>
                (cinemaProjectionRoomAssociatioName, 0, 1, 0, 1, ProjectionRoomCoordinates.EqualityComparer);

            var pr1 = new ProjectionRoom("1", cinema1);
            var pr2 = new ProjectionRoom("1", cinema2);
            var pr3 = new ProjectionRoom("1", cinema3);
            var pr4 = new ProjectionRoom("1", cinema4);
            var pr5 = new ProjectionRoom("2", cinema1);
            var pr6 = new ProjectionRoom("2", cinema2);
            var pr7 = new ProjectionRoom("2", cinema3);
            var pr8 = new ProjectionRoom("2", cinema4);

            var projectionRoomSeatAssociationName = "prs";
            ProjectionRoom.ProjectionRoomToSeatAssociationName =
                Seat.SeatToProjectionRoomAssociationName = projectionRoomSeatAssociationName;

            BusinessObject.RegisterQualifiedAssociation<ProjectionRoom, Seat, SeatQualifier>(projectionRoomSeatAssociationName, 1, 1, SeatQualifier.EqualityComparer);

            var seat111 = new Seat("1", "1", pr1);
            var seat121 = new Seat("1", "2", pr1);
            var seat131 = new Seat("1", "3", pr1);
            var seat141 = new Seat("1", "4", pr1);
            var seat151 = new Seat("1", "5", pr1);

            var seat112 = new Seat("1", "1", pr2);
            var seat122 = new Seat("1", "2", pr2);
            var seat132 = new Seat("1", "3", pr2);
            var seat142 = new Seat("1", "4", pr2);
            var seat152 = new Seat("1", "5", pr2);

            var seat113 = new Seat("1", "1", pr3);
            var seat123 = new Seat("1", "2", pr3);
            var seat133 = new Seat("1", "3", pr3);
            var seat143 = new Seat("1", "4", pr3);
            var seat153 = new Seat("1", "5", pr3);

            var seat114 = new Seat("1", "1", pr4);
            var seat124 = new Seat("1", "2", pr4);
            var seat134 = new Seat("1", "3", pr4);
            var seat144 = new Seat("1", "4", pr4);
            var seat154 = new Seat("1", "5", pr4);

            var seat115 = new Seat("1", "1", pr5);
            var seat125 = new Seat("1", "2", pr5);
            var seat135 = new Seat("1", "3", pr5);
            var seat145 = new Seat("1", "4", pr5);
            var seat155 = new Seat("1", "5", pr5);

            var seat116 = new Seat("1", "1", pr6);
            var seat126 = new Seat("1", "2", pr6);
            var seat136 = new Seat("1", "3", pr6);
            var seat146 = new Seat("1", "4", pr6);
            var seat156 = new Seat("1", "5", pr6);

            string scifi = "SciFi", adventure = "Adventure", hero = "Hero", romance = "Romance", thriller = "Thriller";
            string steven = "Steven Spielberg", joss = "Joss Whedon", max = "Max Power", clint = "Clint Eastwood";
            string[] actors =
            {
                "Robert Downey Jr.", "Chris Evans", "Chris Hemsworth", "Scarlett Johannson",
                "Mark Ruffalo"
            };

            string mature = "M", children = "C", adult = "A", teen = "Teen";

            string avengers = "Avengers", madmax = "Mad Max: Road Fury", starwars = "Star Wars IV: A New Hope", up = "Up!";

            var film1 = new Film {Genre = hero, Title = avengers, Director = joss, Actors = actors, EsrbRating = teen, Length = 120};
            var film2 = new Film { Genre = thriller, Title = madmax, Director = clint, Actors = actors, EsrbRating = mature, Length = 120 };
            var film3 = new Film { Genre = scifi, Title = starwars, Director = steven, Actors = actors, EsrbRating = teen, Length = 120 };
            var film4 = new Film { Genre = adventure, Title = up, Director = max, Actors = actors, EsrbRating = children, Length = 120 };
            var film5 = new Film { Genre = hero, Title = avengers, Director = joss, Actors = actors, EsrbRating = teen, Length = 120 };

            

            var now = DateTime.Now;
            var inmonth = now.AddMonths(1);
            var monthago = new DateTime(now.Year, now.Month-1, now.Day);

            var projectionProjectionRoomAssociationName = "PPR";

            Projection.ProjectionToProjectionRoomAssociationName =
                ProjectionRoom.ProjectionRoomToProjectionAssociationName = projectionProjectionRoomAssociationName;

            BusinessObject.RegisterAssociation<Projection, ProjectionRoom>(projectionProjectionRoomAssociationName, int.MaxValue, 1);

            var filmToProjectionRoomAssociationName = "FPR";
            Film.FilmToProjectionAssociationName =
                Projection.ProjectionToFilmAssociationName = filmToProjectionRoomAssociationName;

            BusinessObject.RegisterAssociation<Film, Projection>(filmToProjectionRoomAssociationName, 0, 1, 0, int.MaxValue);

            new Projection(pr1, film2, now);
            new Projection(pr1, film3, now);
            new Projection(pr1, film1, monthago);
            new Projection(pr1, film4, now);
            new Projection(pr1, film4, inmonth);

            new Projection(pr2, film1, now);
            new Projection(pr2, film2, now);
            new Projection(pr2, film3, now);
            new Projection(pr2, film1, monthago);
            new Projection(pr2, film4, now);
            new Projection(pr2, film4, inmonth);

            new Projection(pr3, film3, monthago);
            new Projection(pr3, film1, monthago);
            new Projection(pr3, film4, now);
            new Projection(pr3, film4, inmonth);

            new Projection(pr4, film1, now);
            new Projection(pr4, film2, now);
            new Projection(pr4, film3, inmonth);
            new Projection(pr4, film1, monthago);
            new Projection(pr4, film4, now);
            new Projection(pr4, film4, inmonth);

            new Projection(pr5, film1, inmonth);
            new Projection(pr5, film2, now);
            new Projection(pr5, film3, now);
            new Projection(pr5, film1, monthago);
            new Projection(pr5, film4, now);
            new Projection(pr5, film4, inmonth);



        }
    }
}
