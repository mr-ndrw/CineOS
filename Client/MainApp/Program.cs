using System;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Concrete;
using en.AndrewTorski.CineOS.Logic.Model.Enums;

namespace en.AndrewTorski.CineOS.Client.MainApp
{
	class Program
	{
		static void Main(string[] args)
		{
		    Cinema cinema1 = new Cinema {Name = "Nice"}, cinema2 = new Cinema {Name = "Cinema2"};

		    var region1 = new Region {Name = "HelloRegion"};

            region1.AddPart(Association.Regions, Association.Cinemas, cinema1);
            region1.AddPart(Association.Regions, Association.Cinemas, cinema2);

		    var cinemasInRegion = region1.GetAssociations(Association.Regions).Cast<Cinema>();

		    

		    foreach (var cinema in cinemasInRegion)
		    {
		        Console.WriteLine(cinema.Name);
		    }

		    var cinema1Region = cinema1.GetAssociations(Association.Cinemas).Cast<Region>();

		    foreach (var region in cinema1Region)
		    {
		        Console.WriteLine(region.Name);
		    }

		    Console.ReadKey();
		}
	}
}
