using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model
{
	/// <summary>
	///		Provides means of containing data about an association such as: what is the associations name, what classes does it
	///		associate and what are the numeric amounts and bounds for each of the classes.
	/// </summary>
	public class Asso<T1, T2> : IEquatable<Asso<T1, T2>> where T1 : class where T2 : class
	{
		#region Private Fields

		/// <summary>
		///		First Type registered with this Association.
		/// </summary>
		private readonly Type _type1;

		/// <summary>
		///		First Type registered with this Association.
		/// </summary>
		private readonly Type _type2;

		/// <summary>
		///		Collection of First Type objects registered with this Association.
		/// </summary>
		private readonly Dictionary<T1, List<T2>> _firstTypeDictionary;

		/// <summary>
		///		Collection of Second Type objects registered with this Association.
		/// </summary>
		private readonly Dictionary<T2, List<T1>> _secondTypeDictionary;

		/// <summary>
		///		Name of the association.
		/// </summary>
		private readonly string _name;

		/// <summary>
		///		Lower cardinality boundary on the side of First Type.
		/// </summary>
		private readonly int _boundForFirstType;

		/// <summary>
		///		Upper cardinality boundary on the side of First Type.
		/// </summary>
		private readonly int _upperBoundForFirstType;

		/// <summary>
		///		Lower cardinality boundary on the side of Second Type.
		/// </summary>
		private readonly int _lowerBoundForSecondType;

		/// <summary>
		///		Upper cardinality boundary on the side of Second Type.
		/// </summary>
		private readonly int _upperBoundForSecondType; 

		#endregion //	Private Fields

		#region Constructors

		/// <summary>
		///		Initializes an object of firstType Asso with specified name and all boundaries for parametrized types.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="boundForFirstType">Lower cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForFirstType">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="lowerBoundForSecondType">Lower cardinality boundary on the side of Second Type.</param>
		/// <param name="upperBoundForSecondType">Upper cardinality boundary on the side of Second Type.</param>
		public Asso(string name, int boundForFirstType, int upperBoundForFirstType, int lowerBoundForSecondType, int upperBoundForSecondType)
		{
			_name = name;

			_boundForFirstType = boundForFirstType;
			_upperBoundForFirstType = upperBoundForFirstType;
			_lowerBoundForSecondType = lowerBoundForSecondType;
			_upperBoundForSecondType = upperBoundForSecondType;

			_firstTypeDictionary = new Dictionary<T1, List<T2>>();
			_secondTypeDictionary = new Dictionary<T2, List<T1>>();

			_type1 = typeof(T1);
			_type2 = typeof(T2);
		}

		/// <summary>
		///		Initializes an object of firstType Asso with specified name and upper boundaries for parametrized types. 
		///		Lower boundaries are set by default to 0.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="upperBoundForT1">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForT2">Upper cardinality boundary on the side of Second Type.</param>
		public Asso(string name, int upperBoundForT1, int upperBoundForT2)
			: this(name, 0, upperBoundForT1, 0, upperBoundForT2)
		{
		}
		/// <summary>
		///		Initializes an object of firstType Asso with specified name. 
		///		Lower boundaries are set by default to 0.
		///		Upper boundaries are set by default to int's max value.
		/// </summary>
		public Asso(string name) :
			this(name, 0, int.MaxValue, 0, int.MaxValue)
		{
		} 

		#endregion //	Constructors

		#region Properties
		/// <summary>
		///		Gets the name of the association.
		/// </summary>
		public string Name { get { return _name; } }

		/// <summary>
		///		Gets the first firstType registered with this Association.
		/// </summary>
		public Type Type1 { get { return _type1; } }

		/// <summary>
		///		Gets the second firstType registered with this Association.
		/// </summary>
		public Type Type2 { get { return _type2; } }

		/// <summary>
		///		Gets the lower boundary on the side of First Type
		/// </summary>
		public int GetBoundaryForFirstType { get { return _boundForFirstType; } }

		/// <summary>
		///		Gets the Upper boundary on the side of First Type
		/// </summary>
		public int UpperBoundaryForFirstType { get { return _upperBoundForFirstType; } }

		/// <summary>
		///		Gets the lower boundary on the side of Second Type
		/// </summary>
		public int LowerBoundaryForSecondType { get { return _lowerBoundForSecondType;  } }

		/// <summary>
		///		Gets the Upper boundary on the side of Second Type
		/// </summary>
		public int UpperBoundaryForSecondType { get { return _upperBoundForSecondType; } }

		#endregion //	Properties

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
		public bool Equals(Asso<T1, T2> other)
		{
			return other != null && Name == other.Name;	//	Lazy evaluation here.
		}

		public bool ConformsWith(Type firstType)
		{
			return firstType == _type1 || firstType == _type2;
		}

		public bool ConformsWith(Type firstType, Type secondType)
		{
			return (firstType == _type1 || firstType == _type2) || (firstType == _type2 || firstType == _type1);
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

			//	First identify whether these objects conform with this Association's types.
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
