using System;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;
using System.Xml;
using en.AndrewTorski.CineOS.Logic.Model;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.EntityHelpers;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var cinemaToRegionAssociationName = "RegionToCinemaComposition";
            var cinemaToProjectionRoomAssociationName = "CinemaToProjectionRoomQualified";
            var projectionRoomToProjectionAssociationName = "ProjectionRoomToProjectionAssociation";
            var projectionRoomToSeatAssociationName = "ProjectionRoomToSeatQualified";

            var projectionToReservationAssociationName = "ProjectionToReservationAssociation";
            var projectionToMediumAssociationName = "ProjectionToMediumAssociation";
            var clientToReservationAssociationName = "ClientToReservationAssociation";
            var reservationsToSeatsAssociationName = "ReservationToSeatAssociation";

            var filmToProjectionAssociationName = "FilmToProjectionAssociation";
            var filmToMediumAssociationName = "FilmToMediumComposition";
            var employeeToCinemaAssociationName = "EmployeeToCinemaAttribute";
            var clientToFilmAssociatioName = "ClientToFilmAttribute";

            Reservation.ReservationToProjectionAssociationName =
                Projection.ProjectionToReservationAssociationName = projectionToReservationAssociationName;
            Film.FilmToMediumAssociationName = Medium.MediumToFilmAssociationName = filmToMediumAssociationName;
            Region.RegionToCinemaAssociationName =
                Cinema.CinemaToRegionAssociationName = cinemaToRegionAssociationName;

            ProjectionRoom.ProjectionRoomToProjectionAssociationName =
                Projection.ProjectionToProjectionRoomAssociationName = projectionRoomToProjectionAssociationName;
            Reservation.ReservationToSeatAssociationName =
                Seat.SeatToReservationAssociationName = reservationsToSeatsAssociationName;
            Reservation.ReserevationToClientAssociationName =
                Logic.Model.Entity.Client.ClientToReserevationAssociationName = clientToReservationAssociationName;
            Film.FilmToProjectionAssociationName =
                Projection.ProjectionToFilmAssociationName = filmToProjectionAssociationName;
            Projection.ProjectionToMediumAssociationName =
                Medium.MediumToProjectionAssociationName = projectionToMediumAssociationName;

            Cinema.CinemaToProjectionRoomAssociationName =
                ProjectionRoom.ProjectionRoomToCinemaAssociationName = cinemaToProjectionRoomAssociationName;
            ProjectionRoom.ProjectionRoomToSeatAssociationName =
                Seat.SeatToProjectionRoomAssociationName = projectionRoomToSeatAssociationName;

            Logic.Model.Entity.Client.ClientToFilmAssociationName =
                Film.FilmToClientAssociationName = clientToFilmAssociatioName;
            Employee.EmployeeToCinemaAssociationName =
                Cinema.CinemaToEmployeeAssociationName = employeeToCinemaAssociationName;

            var path =
                @"C:\Users\Andrew\Documents\Visual Studio 2013\Projects\CineOS\Client\CineOsWebApiService\persistance.xml";

            BusinessObject.DictionaryContainer = ReadExtents(path);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private static void WriteExtents(string path, DictionaryContainer dictionaryContainer)
        {
            try
            {
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                using (var xmlWriter = XmlDictionaryWriter.CreateTextWriter(fileStream))
                {
                    var serializer = new NetDataContractSerializer();
                    serializer.WriteObject(xmlWriter, dictionaryContainer);
                }
            }
            catch (SerializationException se)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Serialization failed");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(se.Message);
                throw se;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Serialization operation: {0} StackTrace: {1}", e.Message, e.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static DictionaryContainer ReadExtents(string path)
        {
            try
            {
                DictionaryContainer dictionaryContainer;
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                using (var xmlReader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas())
                    )
                {
                    var objectSerializer = new NetDataContractSerializer();
                    dictionaryContainer = (DictionaryContainer)objectSerializer.ReadObject(xmlReader, true);
                }

                return dictionaryContainer;
            }
            catch (SerializationException se)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Serialization failed");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(se.Message);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Serialization operation: {0} StackTrace: {1}", e.Message, e.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
            }

            return null;
        }

        private void exemplearydata()
        {
            Person.CreateClient(new PersonCredentials { Email = "Hello" }, "Hello");
            Person.CreateClient(new PersonCredentials { Email = "Hello2" }, "Hello2");
            Person.CreateClient(new PersonCredentials { Email = "Hello3" }, "Hello4");
            Person.CreateClient(new PersonCredentials { Email = "Hello4" }, "Hello5");
            Person.CreateClient(new PersonCredentials { Email = "Hello5" }, "Hello7");

            var regionCinemaAssociationName = "RCC";
            Region.RegionToCinemaAssociationName = Cinema.CinemaToRegionAssociationName = regionCinemaAssociationName;
            BusinessObject.RegisterComposition<Region, Cinema>(regionCinemaAssociationName, 0, int.MaxValue);

            var region1 = new Region { Name = "Nice region" };
            var region2 = new Region { Name = "Best region" };
            var region3 = new Region { Name = "Smelly region" };
            var region4 = new Region { Name = "Hello region" };

            var cinema1 = new Cinema(region1) { Address = "227 E 17th St, NYC", Name = "Odeon" };
            var cinema2 = new Cinema(region1) { Address = "223 Sunshine BLVD, LA", Name = "West Best Cinema" };
            var cinema3 = new Cinema(region2) { Address = "24 Washington St, Chicago", Name = "Pulp Cinema" };
            var cinema4 = new Cinema(region2) { Address = "223 Sunshine BLVD, LA", Name = "Superplex" };
            var cinema5 = new Cinema(region3) { Address = "123 Old World St., SantaMonica", Name = "CineX" };
            var cinema6 = new Cinema(region1) { Address = "2 Graner Plaza, Plott City", Name = "CineLot" };
            var cinema7 = new Cinema(region4) { Address = "34 5th Alley, NYC", Name = "CineLex" };

            var cinemaProjectionRoomAssociatioName = "CPR";
            Cinema.CinemaToProjectionRoomAssociationName =
                ProjectionRoom.ProjectionRoomToCinemaAssociationName = cinemaProjectionRoomAssociatioName;

            BusinessObject.RegisterQualifiedAssociation<Cinema, ProjectionRoom, ProjectionRoomQualifier>
                (cinemaProjectionRoomAssociatioName, 0, 1, 0, 1, ProjectionRoomQualifier.EqualityComparer);

            var pr1 = new ProjectionRoom("1", cinema1);
            var pr2 = new ProjectionRoom("1", cinema2);
            var pr3 = new ProjectionRoom("1", cinema3);
            var pr4 = new ProjectionRoom("1", cinema4);
            var pr5 = new ProjectionRoom("2", cinema1);
            var pr6 = new ProjectionRoom("2", cinema2);
            var pr7 = new ProjectionRoom("2", cinema3);
            var pr8 = new ProjectionRoom("2", cinema4);

            pr1.ColumnCount =
                pr2.ColumnCount = pr3.ColumnCount = pr4.ColumnCount = pr5.ColumnCount = pr6.ColumnCount = 5;

            pr1.RowCount =
                pr2.RowCount = pr3.RowCount = pr4.RowCount = pr5.RowCount = pr6.RowCount = 1;

            var projectionRoomSeatAssociationName = "prs";
            ProjectionRoom.ProjectionRoomToSeatAssociationName =
                Seat.SeatToProjectionRoomAssociationName = projectionRoomSeatAssociationName;

            BusinessObject.RegisterQualifiedAssociation<ProjectionRoom, Seat, SeatQualifier>(
                projectionRoomSeatAssociationName, 1, 1, SeatQualifier.EqualityComparer);

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

            var film1 = new Film
            {
                Title = "Up!",
                Director = "Joss Whedon",
                Genre = "Animated",
                Year = 2009,
                Description =
                    "Carl Fredricksen (Ed Asner), a 78-year-old balloon salesman, is about to fulfill a lifelong dream. Tying thousands of balloons to his house, he flies away to the South American wilderness. But curmudgeonly Carl's worst nightmare comes true when he discovers a little boy named Russell is a stowaway aboard the balloon-powered house.",
                EsrbRating = "K",
                PosterUrl = "http://i.imgur.com/mPnk2rm.jpg",
                Length = 90,
                Actors = actors
            };


            var film2 = new Film
            {
                Title = "Ironman",
                Director = "JossWhedon",
                Genre = "SuperHero",
                Year = 2011,
                Description =
                    @"Tony Stark. Genius, billionaire, playboy, philanthropist. Son of legendary inventor and weapons contractor Howard Stark. When Tony Stark is assigned to give a weapons presentation to an Iraqi unit led by Lt. Col. James Rhodes, he's given a ride on enemy lines. That ride ends badly when Stark's Humvee that he's riding in is attacked by enemy combatants. He survives - barely - with a chest full of shrapnel and a car battery attached to his heart. In order to survive he comes up with a way to miniaturize the battery and figures out that the battery can power something else. Thus Iron Man is born. He uses the primitive device to escape from the cave in Iraq. Once back home, he then begins work on perfecting the Iron Man suit. But the man who was put in charge of Stark Industries has plans of his own to take over Tony's technology for other matters.",
                EsrbRating = "Teen",
                PosterUrl = "http://i.imgur.com/3DQPyP8.jpg",
                Length = 120,
                Actors = actors
            };

            var film3 = new Film
            {
                Title = "Star Wars IV: New Hope",
                Genre = "Sci-Fi",
                Year = 1977,
                Director = "George Lucas",
                Description =
                    "Luke Skywalker stays with his foster aunt and uncle on a farm on Tatooine. He is desperate to get off this planet and get to the Academy like his friends, but his uncle needs him for the next harvest. Meanwhile, an evil emperor has taken over the galaxy, and has constructed a formidable \"Death Star\" capable of destroying whole planets. Princess Leia, a leader in the resistance movement, acquires plans of the Death Star, places them in R2-D2, a droid, and sends him off to find Obi-Wan Kenobi. Before he finds him, R2-D2 ends up on the Skywalkers' farm with his friend C-3PO. R2-D2 then wanders into the desert, and when Luke follows, they eventually come across Obi-Wan. Will Luke, Obi-Wan and the two droids be able to destroy the Death Star, or will the Emperor rule forever?",
                EsrbRating = "M",
                Length = 150,
                PosterUrl = @"http://i.imgur.com/bEg5DNf.jpg",
                Actors = actors
            };

            var film4 = new Film
            {
                Title = "Les Miserables",
                Genre = "Musical",
                Year = 2013,
                Director = "Alfie Bowe",
                Description =
                    "After 19 years as a prisoner, Jean Valjean (Hugh Jackman) is freed by Javert (Russell Crowe), the officer in charge of the prison workforce. Valjean promptly breaks parole but later uses money from stolen silver to reinvent himself as a mayor and factory owner. Javert vows to bring Valjean back to prison. Eight years later, Valjean becomes the guardian of a child named Cosette after her mother's (Anne Hathaway) death, but Javert's relentless pursuit means that peace will be a long time coming.",
                Actors = actors,
                EsrbRating = "M",
                Length = 120,
                PosterUrl = @"http://i.imgur.com/XJO3leE.jpg"
            };
            var film5 = new Film
            {
                Title = "Pulp Fiction",
                Year = 1991,
                Genre = "Thriller",
                Director = "Quentin Tarantino",
                Description =
                    "Vincent Vega (John Travolta) and Jules Winnfield (Samuel L. Jackson) are hitmen with a penchant for philosophical discussions. In this ultra-hip, multi-strand crime movie, their storyline is interwoven with those of their boss, gangster Marsellus Wallace (Ving Rhames) ; his actress wife, Mia (Uma Thurman) ; struggling boxer Butch Coolidge (Bruce Willis) ; master fixer Winston Wolfe (Harvey Keitel) and a nervous pair of armed robbers, \"Pumpkin\" (Tim Roth) and \"Honey Bunny\" (Amanda Plummer).",
                Actors = actors,
                EsrbRating = "M",
                Length = 120,
                PosterUrl = @"http://i.imgur.com/SvSE5mE.jpg"
            };


            var now = DateTime.Now;
            var inmonth = now.AddMonths(1);
            var monthago = new DateTime(now.Year, now.Month - 1, now.Day);

            var projectionProjectionRoomAssociationName = "PPR";

            Projection.ProjectionToProjectionRoomAssociationName =
                ProjectionRoom.ProjectionRoomToProjectionAssociationName = projectionProjectionRoomAssociationName;

            BusinessObject.RegisterAssociation<Projection, ProjectionRoom>(projectionProjectionRoomAssociationName,
                int.MaxValue, 1);

            var filmToProjectionRoomAssociationName = "FPR";
            Film.FilmToProjectionAssociationName =
                Projection.ProjectionToFilmAssociationName = filmToProjectionRoomAssociationName;

            BusinessObject.RegisterAssociation<Film, Projection>(filmToProjectionRoomAssociationName, 0, 1, 0,
                int.MaxValue);

            new Projection(pr1, film2, inmonth);
            new Projection(pr1, film3, now);
            new Projection(pr1, film1, monthago);
            new Projection(pr1, film4, now);
            new Projection(pr1, film4, inmonth);
            new Projection(pr1, film5, inmonth);

            new Projection(pr2, film1, now);
            new Projection(pr2, film2, now);
            new Projection(pr2, film3, now);
            new Projection(pr2, film1, monthago);
            new Projection(pr2, film4, now);
            new Projection(pr2, film4, inmonth);
            new Projection(pr2, film5, inmonth);

            new Projection(pr3, film3, monthago);
            new Projection(pr3, film1, monthago);
            new Projection(pr3, film4, now);
            new Projection(pr3, film4, inmonth);
            new Projection(pr3, film5, inmonth);

            new Projection(pr4, film1, now);
            new Projection(pr4, film2, now);
            new Projection(pr4, film3, inmonth);
            new Projection(pr4, film1, monthago);
            new Projection(pr4, film4, now);
            new Projection(pr4, film4, inmonth);
            new Projection(pr4, film5, inmonth);

            new Projection(pr5, film1, inmonth);
            new Projection(pr5, film2, now);
            new Projection(pr5, film3, now);
            new Projection(pr5, film1, monthago);
            new Projection(pr5, film4, now);
            new Projection(pr5, film4, inmonth);
            new Projection(pr5, film5, inmonth);

            var reservationprojectionnassociation = "rpaaa";
            Reservation.ReservationToProjectionAssociationName =
                Projection.ProjectionToReservationAssociationName = reservationprojectionnassociation;

            BusinessObject.RegisterAssociation<Projection, Reservation>(reservationprojectionnassociation);
        }
    }
}