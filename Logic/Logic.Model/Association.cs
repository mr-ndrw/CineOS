using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Associations;

namespace en.AndrewTorski.CineOS.Logic.Model
{
	/// <summary>
	///		Provides means of containing data about an association such as: what is the associations name, what classes does it
	///		associate and what are the numeric amounts and bounds for each of the classes.
	/// </summary>
	public class Association<T1, T2> : Association, IEquatable<Association<T1, T2>> where T1 : class where T2 : class
	{
		#region Private Fields

		/// <summary>
		///		Collection of First Type objects registered with this AssociationRole.
		/// </summary>
		private readonly Dictionary<T1, List<T2>> _firstTypeDictionary;

		/// <summary>
		///		Collection of Second Type objects registered with this AssociationRole.
		/// </summary>
		private readonly Dictionary<T2, List<T1>> _secondTypeDictionary;

		#endregion //	Private Fields

		#region Constructors

		/// <summary>
		///		Initializes an object of Association with specified name and all boundaries for parametrized types.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="lowerBoundForFirstType">Lower cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForFirstType">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="lowerBoundForSecondType">Lower cardinality boundary on the side of Second Type.</param>
		/// <param name="upperBoundForSecondType">Upper cardinality boundary on the side of Second Type.</param>
		public Association(string name, int lowerBoundForFirstType, int upperBoundForFirstType, int lowerBoundForSecondType, int upperBoundForSecondType)
			:base(typeof(T1), typeof(T2), name, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType)
		{
			_firstTypeDictionary = new Dictionary<T1, List<T2>>();
			_secondTypeDictionary = new Dictionary<T2, List<T1>>();
		}

		/// <summary>
		///		Initializes an object of Association with specified name and upper boundaries for parametrized types. 
		///		Lower boundaries are set by default to 0.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="upperBoundForT1">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForT2">Upper cardinality boundary on the side of Second Type.</param>
		public Association(string name, int upperBoundForT1, int upperBoundForT2)
			: this(name, 0, upperBoundForT1, 0, upperBoundForT2){}

		/// <summary>
		///		Initializes an object of Association with specified name. 
		///		Lower boundaries are set by default to 0.
		///		Upper boundaries are set by default to int's max value.
		/// </summary>
		public Association(string name) :
			this(name, 0, int.MaxValue, 0, int.MaxValue){} 

		#endregion //	Constructors

		#region Methods

		/// <summary>
		///		Compares whether this Assocation is equal to other Assocation.
		/// </summary>
		/// <param name="other">
		///		Other association.
		/// </param>
		/// <returns>
		///		Bool value.
		/// </returns>
		public bool Equals(Association<T1, T2> other)
		{
			return other != null && Name == other.Name;	//	Lazy evaluation here.
		}

		public bool ConformsWith(Type firstType)
		{
			return firstType == Type1 || firstType == Type2;
		}

		public bool ConformsWith(Type firstType, Type secondType)
		{
			return (firstType == Type1 || firstType == Type2) || (firstType == Type2 || firstType == Type1);
		}

		public List<object> GetAssociatedObjects(object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			if (obj is T1)
			{
				var result = !_firstTypeDictionary.ContainsKey((T1) obj) ? new List<object>() : _firstTypeDictionary[(T1) obj].Cast<object>()
						.ToList();
				return result;
			}

			return !_secondTypeDictionary.ContainsKey((T2)obj) ? new List<object>() : _secondTypeDictionary[(T2)obj].Cast<object>().ToList();
		}

		public void Link(T1 firstObject, T2 secondObject)
		{
			List<T2> firstList;
			List<T1> secondList;
			//	Check if we can link these objects basing on the boundaries.
			if (!_firstTypeDictionary.ContainsKey(firstObject))
			{
				firstList = new List<T2>();
				_firstTypeDictionary.Add(firstObject, firstList);
			}
			else
			{
				firstList = _firstTypeDictionary[firstObject];
			}

			if (!_secondTypeDictionary.ContainsKey(secondObject))
			{
				secondList = new List<T1>();
				_secondTypeDictionary.Add(secondObject, secondList);
			}
			else
			{
				secondList = _secondTypeDictionary[secondObject];
			}

			if (firstList.Count + 1 > UpperBoundaryForSecondType || secondList.Count + 1 > UpperBoundaryForFirstType)
			{
				//	TODO Implemenet new exception for boundary excess event.
				throw new Exception("Boundaries exceeded");
			}

			firstList.Add(secondObject);
			secondList.Add(firstObject);

		}
		#endregion //	Methods

		public void Link(object firstObject, object secondObject)
		{
			if (firstObject == null) throw new ArgumentNullException("firstObject");
			if (secondObject == null) throw new ArgumentNullException("secondObject");

			//	First identify whether these objects conform with this AssociationRole's types.
			if (ConformsWith(firstObject.GetType(), secondObject.GetType()))
			{
				throw new Exception();
			}

			//	Now check which is which
			//	And link them
			if (firstObject is T1)
			{
				var firstTypedObject = firstObject as T1;
				var secondTypedObject = secondObject as T2;
				Link(firstTypedObject, secondTypedObject);
			}
			else
			{
				var firstTypedObject = firstObject as T2;
				var secondTypedObject = secondObject as T1;
				Link(secondTypedObject, firstTypedObject);
			}
		}
	}
}
