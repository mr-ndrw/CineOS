using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	[DataContract]
	public class Client : BusinessObject
	{
		public Client(string password, Person person)
		{
		    Password = password;
		    Person = person;
		}

        /// <summary>
        ///     Password of this Client.
        /// </summary>
        [DataMember]
	    public string Password { get; set; }

        /// <summary>
        ///     Reference to the Person which this Client is.
        [DataMember]
	    public Person Person { get; private set; }

        [DataMember]
        public static string ClientToReserevationAssociationName { get; set; }

        [DataMember]
	    public static string ClientToFilmAssociationName { get; set; }

        /// <summary>
        ///     Checks if a Client of provided email and password exists in the system.
        ///     If such client was found, method will return this Client's Id number.
        ///     If not, it will return -1;
        /// </summary>
	    public static int Exists(string email, string password)
	    {
	        var clients = RetrieveExtentFor(typeof (Client)).Cast<Client>();
            var foundClient = clients.FirstOrDefault(client => Regex.Replace(client.Person.Email, @"\s+", "") == email && client.Password == password);
	        return foundClient != null
	            ? foundClient.Person.Id
	            : -1;
	    }

	    public static IEnumerable<Client> Extent
	    {
	        get { return RetrieveExtentFor(typeof (Client)).Cast<Client>(); }
	    }
	}
}