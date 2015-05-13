using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using en.AndrewTorski.CineOS.Logic.Model;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using en.AndrewTorski.CineOS.Shared.HelperLibrary;

namespace en.AndrewTorski.CineOS.Client.MainApp
{
	class Program
	{
		static void Main(string[] args)
		{
			const string name = "Best Thing";

			AssociatedObject.DictionaryContainer = ReadExtents("BestThing.xml");

			Console.WriteLine("{0} exists? = {1}", name, AssociatedObject.ContainsAssociation(name));

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
