using System;
using System.Collections.Generic;
using en.AndrewTorski.CineOS.Logic.Model.Association;

namespace en.AndrewTorski.CineOS.Logic.Model
{
	/// <summary>
	///		Container for dictionaries used in implementation of object-oriented
	///		implementations of class extensions and associations.
	/// </summary>
	public class DictionaryContainer
	{
		/// <summary>
		///     Dictionary of Associations' names and their correspondent Associations.
		/// </summary>
		public Dictionary<string, AssociationBase> AssociationsDictionary { get; set; }

		/// <summary>
		///		Gets or sets the Dictionary of Key: Types and Values: Collection of objects which are of this tTpe.
		/// </summary>
		public Dictionary<Type, List<object>> ExtentDictionary { get; set; } 
	}
}