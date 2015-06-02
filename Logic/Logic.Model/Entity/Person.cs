using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model.Entity
{
	[DataContract]
	public abstract class Person : BusinessObject
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Surname { get; set; }
		[DataMember]
		public string TelephoneNo { get; set; }
		[DataMember]
		public string Email { get; set; }
	}
}
