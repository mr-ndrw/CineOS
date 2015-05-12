using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
	/// <summary>
	///		Base class for all the classes which need quick access to all the objects of their class.
	/// </summary>
	[DataContract]
    public abstract class PartOfExtent
	{
		/// <summary>
		///		Gets or sets the Dictionary of Key: Types and Values: Collection of objects which are of this tTpe.
		/// </summary>
		[DataMember]
		public static Dictionary<Type, List<object>> ExtentDictionary { get; set; }

		static PartOfExtent()
		{
			ExtentDictionary = new Dictionary<Type, List<object>>();
		}

		/// <summary>
		///		Intializes a new instance of the PartOfExtent class and subscribes this object
		///		to the Dictionary of Types and Collection of Objects of that type.
		/// </summary>
	    protected PartOfExtent()
        {
            var self = this;
            var selfType = self.GetType();

            List<object> classExtentList;
            if (ExtentDictionary.ContainsKey(selfType))
            {
                classExtentList = ExtentDictionary[selfType];
            }
            else
            {             
                classExtentList = new List<object>();
                ExtentDictionary.Add(selfType, classExtentList);
            }

            classExtentList.Add(self);
        }

    }
}