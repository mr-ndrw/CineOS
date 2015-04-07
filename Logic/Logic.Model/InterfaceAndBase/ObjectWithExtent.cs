using System;
using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
    public abstract class ObjectWithExtent
    {
        public static Dictionary<Type, List<object>> ExtentDictionary { get; private set; }

	    protected ObjectWithExtent()
        {
            if (ExtentDictionary == null)
            {
                CreateExtentDictionary();
            }
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

            //Console.WriteLine("Current object type: {0}", selfType);
        }

        public static void CreateExtentDictionary()
        {
            if (ExtentDictionary == null)
            {
                ExtentDictionary = new Dictionary<Type, List<object>>();
            }
        }

        public static void CreateExtentDictionary(Dictionary<Type, List<Object>> extentDictionary )
        {
            if (ExtentDictionary == null)
            {
                ExtentDictionary = extentDictionary;
            }
        }

    }
}