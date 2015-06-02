using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;
using en.AndrewTorski.CineOS.Logic.Model;

namespace en.AndrewTorski.CineOS.Client.MainApp
{
	class Program
	{
		static void Main(string[] args)
		{

		    var list = new List<SeatViewModel>()
		    {
		        new SeatViewModel("1", "2"), new SeatViewModel("1", "3"), new SeatViewModel("1", "2"),
                new SeatViewModel("3", "2"), new SeatViewModel("2", "1"), new SeatViewModel("2", "2"),
                new SeatViewModel("2", "3"), new SeatViewModel("3", "1"), new SeatViewModel("3", "3"), 
                new SeatViewModel("4", "3"), new SeatViewModel("4", "2"), new SeatViewModel("4", "1")
		    };

            var query = list.OrderBy(seat => seat.RowColumn).ToList();

		    var rowMax = 3;
		    var colMax = 4;

            var seat2dArray = new SeatViewModel[rowMax, colMax];

		    var rows = new List <IEnumerable<SeatViewModel>>();

		    for (int i = 0; i < colMax; i++)
		    {
		        var take = query.Take(rowMax).ToList();
                query.RemoveRange(0, rowMax);
		        rows.Add(take);
		    }

		    foreach (var row in rows)
		    {
		        foreach (var seatViewModel in row)
		        {
		            Console.Write("{0}, ", seatViewModel.RowColumn);
		        }
		        Console.WriteLine();
		    }
            
		    Console.ReadKey();
		}

		static void WriteExtents(string path, DictionaryContainer dictionaryContainer)
		{
			try
			{
				using(var fileStream = new FileStream(path, FileMode.OpenOrCreate))
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

		static DictionaryContainer ReadExtents(string path)
		{
			try
			{
				DictionaryContainer dictionaryContainer;
				using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
				using (var xmlReader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
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
	}
}
