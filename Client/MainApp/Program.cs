using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using en.AndrewTorski.CineOS.Logic.Model;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Client.MainApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*#region associations
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
             
            BusinessObject.RegisterComposition<Projection, Reservation>(projectionToReservationAssociationName,0, Int32.MaxValue);
            BusinessObject.RegisterComposition<Film, Medium>(filmToMediumAssociationName, 0, Int32.MaxValue);

            BusinessObject.RegisterAssociation<Region, Cinema>(cinemaToRegionAssociationName);
            BusinessObject.RegisterAssociation<ProjectionRoom, Projection>(projectionRoomToProjectionAssociationName);
            BusinessObject.RegisterAssociation<Seat, Reservation>(reservationsToSeatsAssociationName);
            BusinessObject.RegisterAssociation<Logic.Model.Entity.Client, Reservation>(clientToReservationAssociationName);
            BusinessObject.RegisterAssociation<Projection, Film>(filmToProjectionAssociationName);
            BusinessObject.RegisterAssociation<Projection, Medium>(projectionToMediumAssociationName, 0, Int32.MaxValue, 0, 2);
            
            BusinessObject.RegisterQualifiedAssociation<Cinema, ProjectionRoom, ProjectionRoomQualifier>(cinemaToProjectionRoomAssociationName, 20, 20, ProjectionRoomQualifier.EqualityComparer);
            BusinessObject.RegisterQualifiedAssociation<ProjectionRoom, Seat, SeatQualifier>(projectionRoomToSeatAssociationName, 20, 20, SeatQualifier.EqualityComparer);
            
            BusinessObject.RegisterAttributeAssociation<Logic.Model.Entity.Client, Film, int>(clientToFilmAssociatioName);
            BusinessObject.RegisterAttributeAssociation<Employee, Cinema, Employment>(employeeToCinemaAssociationName);



		    #endregion

            var credsAndrewTorski = new PersonCredentials(){Email="andrew.torski@gmail.com", Name = "Andrzej", Surname = "Torski", TelephoneNo = "233"};
            var credsDrake = new PersonCredentials(){Email="drake.wriston@gmail.com", Name = "Andrzej", Surname = "Torski", TelephoneNo = "233"};

		    Person.CreateClient(credsAndrewTorski, "password");
            Person.CreateClient(credsDrake, "password");

		    var eastUsRegion = new Region {Name = "East US"};
		    var westUsRegion = new Region {Name = "West US"};

            var odeon = new Cinema(eastUsRegion){Name = "Odeon", Address = "225 22th Street, NYC"};
            var hollywoodBowl = new Cinema(westUsRegion){Name = "Hollywood Bowl", Address = "1 Sunset Blvd., LA"};
            var cinePlex = new Cinema(westUsRegion){Name = "CinePlex", Address = "45 Central Plaza, Redmond"};
            var rockCinema = new Cinema(eastUsRegion){Name = "Rockefeller Cinema", Address = "3 Beach Alley, Miami"};

            var projectionRoomInOdeon = new ProjectionRoom("I", odeon){RowCount =  5, ColumnCount = 5};
            var projectionRoomInHollyWoodBowl = new ProjectionRoom("1", hollywoodBowl){RowCount = 3, ColumnCount = 3};
            var projectionRoomInCinePlex = new ProjectionRoom("Two", cinePlex){RowCount = 2, ColumnCount = 2};
            var projectionRoomInRockCinema = new ProjectionRoom("II", rockCinema){RowCount = 2, ColumnCount = 2};

		    CreateSeats(projectionRoomInOdeon);
            CreateSeats(projectionRoomInHollyWoodBowl);
            CreateSeats(projectionRoomInCinePlex);
            CreateSeats(projectionRoomInRockCinema);

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

		    new Projection(projectionRoomInOdeon, film1, new DateTime(2015, 06, 22, 15, 15, 0));
            new Projection(projectionRoomInOdeon, film1, new DateTime(2015, 06, 22, 20, 15, 0));
            new Projection(projectionRoomInOdeon, film1, new DateTime(2015, 06, 23, 15, 15, 0));
            new Projection(projectionRoomInOdeon, film2, new DateTime(2015, 06, 22, 10, 15, 0));
            new Projection(projectionRoomInOdeon, film3, new DateTime(2015, 06, 25, 15, 15, 0));
            new Projection(projectionRoomInOdeon, film4, new DateTime(2015, 06, 27, 15, 15, 0));
            new Projection(projectionRoomInOdeon, film5, new DateTime(2015, 06, 28, 15, 15, 0));*/

            //WriteExtents("persistance.xml", BusinessObject.DictionaryContainer);
            //var dictionary = ReadExtents(@"C:\Users\Andrew\Documents\Visual Studio 2013\Projects\CineOS\Client\CineOsWebApiService\persistance.xml");
            BusinessObject.DictionaryContainer = ReadExtents("persistance.xml");


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


            foreach (var region in Region.Extent)
            {
                Console.WriteLine(region.Id);
		        foreach (var cinema in region.Cinemas)
		        {
		            Console.WriteLine("/t{0}", cinema.Name);
		        }
            }


            Console.ReadKey();
        }

        private static void CreateSeats(ProjectionRoom room)
        {
            for (var i = 1; i <= room.RowCount; i++)
            {
                for (var j = 1; j <= room.ColumnCount; j++)
                {
                    new Seat(i.ToString(), j.ToString(), room);
                }
            }
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
                    dictionaryContainer = (DictionaryContainer) objectSerializer.ReadObject(xmlReader, true);
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
    }
}
