using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.Association;

namespace en.AndrewTorski.CineOS.Logic.Model
{
	/// <summary>
	///		Container for dictionaries used in implementation of object-oriented
	///		implementations of class extensions and associations.
	/// </summary>
	[DataContract]
	public class DictionaryContainer
	{
		/// <summary>
		///     Dictionary of Associations' names and their correspondent Associations.
		/// </summary>
		[DataMember]
		public Dictionary<string, AssociationBase> AssociationsDictionary { get; set; }

		/// <summary>
		///		Gets or sets the Dictionary of Key: Types and Values: Collection of objects which are of this tTpe.
		/// </summary>
		[DataMember]
		public Dictionary<Type, List<object>> ExtentDictionary { get; set; } 
	}
}