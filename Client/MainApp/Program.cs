using System;
using System.Linq;
using System.Runtime.InteropServices;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Shared.HelperLibrary;

namespace en.AndrewTorski.CineOS.Client.MainApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var region1 = new Region();

		    Cinema cinema1 = new Cinema(region1) {Name = "Nice"}, cinema2 = new Cinema(region1) {Name = "Cinema2"};

			ProjectionRoom projectionRoom1 = new ProjectionRoom("1", cinema1), projectionRoom2 = new ProjectionRoom("2", cinema1);

			var projectionRoom1InCinema1 = cinema1.GetProjectionRoom("1");

			Console.WriteLine("Projection room 1 number: {0}", projectionRoom1InCinema1.Number);


			Seat seat1 = new Seat("1", "1", projectionRoom1), seat2 = new Seat("1", "2", projectionRoom1);

			var foundSeat = projectionRoom1.GetSeat("1", "1");


			if (foundSeat != null)
				Console.WriteLine("Found seat's row number: {0} & column number: {1}", foundSeat.RowNumber, foundSeat.ColumnNumber);
			else
				Console.WriteLine("Was not found... :(");


			var fromDate = new DateTime(2015, 1, 1);
			var toDate = new DateTime(2015, 12, 1);

			Console.WriteLine("Does it work? {0}", DateTime.Now.IsBetween(fromDate, toDate));

		    Console.ReadKey();
		}
	}
}
