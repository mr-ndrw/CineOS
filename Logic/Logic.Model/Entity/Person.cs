using System;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
    [DataContract]
    public class Person : BusinessObject
    {
        public Person(PersonCredentials personCredentials)
        {
            Id = NextFreeId;
            NextFreeId++;

            Name = personCredentials.Name;
            Surname = personCredentials.Surname;
            TelephoneNo = personCredentials.TelephoneNo;
            Email = personCredentials.Email;
        }

        #region Properties

        /// <summary>
        ///     Unique identifier of this object.
        /// </summary>
        [DataMember]
        public int Id { get; private set; }

        /// <summary>
        ///     Next free identifier number which will be ascribed to next newly created instance of this class.
        /// </summary>
        [DataMember]
        public static int NextFreeId { get; set; }

        /// <summary>
        ///     Name of the Person.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     Surname of the Person.
        /// </summary>
        [DataMember]
        public string Surname { get; set; }

        /// <summary>
        ///     Telephone number of this Person.
        /// </summary>
        [DataMember]
        public string TelephoneNo { get; set; }

        /// <summary>
        ///     E-mail address of this Person.
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        ///     Client subclass object of this Person.
        /// </summary>
        [DataMember]
        public Client Client { get; private set; }

        /// <summary>
        ///     Employee subclass object of this Person.
        /// </summary>
        [DataMember]
        public Employee Employee { get; private set; }

        #endregion

        #region Static Methods

        public static Person CreateEmployee(PersonCredentials personCredentials, string address, DateTime dateOfBirth)
        {
            var person = new Person(personCredentials);
            person.MakeEmployee(address, dateOfBirth);
            return person;
        }

        public static Person CreateClient(PersonCredentials personCredentials, string password)
        {
            var person = new Person(personCredentials);
            person.MakeClient(password);
            return person;
        }

        public static Person CreateClientAndEmployee(PersonCredentials personCredentials, string address,
            DateTime dateOfBirth, string password)
        {
            var person = new Person(personCredentials);
            person.MakeClient(password);
            person.MakeEmployee(address, dateOfBirth);
            return person;
        }

        #endregion

        #region Methods

        public bool IsEmployee()
        {
            return Employee != null;
        }

        public bool IsClient()
        {
            return Client != null;
        }

        public bool IsEmployeeAndClient()
        {
            return IsEmployee() && IsClient();
        }
        public void MakeEmployee(string address, DateTime dateOfBirth)
        {
            var employee = new Employee(address, dateOfBirth, this);
            Employee = employee;
        }

        public void MakeClient(string password)
        {
            var client = new Client(password, this);
            Client = client;
        }

        #endregion
    }
    public class PersonCredentials
    {
        /// <summary>
        ///     Name of the Person.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     Surname of the Person.
        /// </summary>
        [DataMember]
        public string Surname { get; set; }

        /// <summary>
        ///     Telephone number of this Person.
        /// </summary>
        [DataMember]
        public string TelephoneNo { get; set; }

        /// <summary>
        ///     E-mail address of this Person.
        /// </summary>
        [DataMember]
        public string Email { get; set; }
    }
}
