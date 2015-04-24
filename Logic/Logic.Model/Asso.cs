using System;
using System.Collections;
using System.Collections.Generic;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;

namespace en.AndrewTorski.CineOS.Logic.Model
{
	/// <summary>
	///		Provides means of containing data about an association such as: what is the associations name, what classes does it
	///		associate and what are the numeric amounts and bounds for each of the classes.
	/// </summary>
	public class Asso<T1, T2> : IEquatable<Asso<T1, T2>> 
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
		private readonly HashSet<AssociatedObject> _firsTypeHashSet;

		/// <summary>
		///		Collection of Second Type objects registered with this Association.
		/// </summary>
		private readonly HashSet<AssociatedObject> _secondTypeHashSet;

		/// <summary>
		///		Name of the association.
		/// </summary>
		private readonly string _name;

		/// <summary>
		///		Lower cardinality boundary on the side of First Type.
		/// </summary>
		private readonly int _lowerBoundForFirstType;

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
		///		Initializes an object of type Asso with specified name and all boundaries for parametrized types.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="lowerBoundForFirstType">Lower cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForFirstType">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="lowerBoundForSecondType">Lower cardinality boundary on the side of Second Type.</param>
		/// <param name="upperBoundForSecondType">Upper cardinality boundary on the side of Second Type.</param>
		public Asso(string name, Type firstType, Type secondType, int lowerBoundForFirstType, int upperBoundForFirstType, int lowerBoundForSecondType, int upperBoundForSecondType)
		{
			_name = name;

			_lowerBoundForFirstType = lowerBoundForFirstType;
			_upperBoundForFirstType = upperBoundForFirstType;
			_lowerBoundForSecondType = lowerBoundForSecondType;
			_upperBoundForSecondType = upperBoundForSecondType;

			_firsTypeHashSet = new HashSet<AssociatedObject>();
			_secondTypeHashSet = new HashSet<AssociatedObject>();

			_type1 = firstType;
			_type2 = secondType;
		}

		/// <summary>
		///		Initializes an object of type Asso with specified name and upper boundaries for parametrized types. 
		///		Lower boundaries are set by default to 0.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="upperBoundForT1">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForT2">Upper cardinality boundary on the side of Second Type.</param>
		public Asso(string name, Type firstType, Type secondType, int upperBoundForT1, int upperBoundForT2)
			: this(name, firstType, secondType, 0, upperBoundForT1, 0, upperBoundForT2)
		{
		}
		/// <summary>
		///		Initializes an object of type Asso with specified name. 
		///		Lower boundaries are set by default to 0.
		///		Upper boundaries are set by default to int's max value.
		/// </summary>
		public Asso(string name, Type firstType, Type secondType) :
			this(name, firstType, secondType, 0, int.MaxValue, 0, int.MaxValue)
		{
		} 

		#endregion //	Constructors

		#region Properties
		/// <summary>
		///		Gets the name of the association.
		/// </summary>
		public string Name { get { return _name; } }

		/// <summary>
		///		Gets the first type registered with this Association.
		/// </summary>
		public Type Type1 { get { return _type1; } }

		/// <summary>
		///		Gets the second type registered with this Association.
		/// </summary>
		public Type Type2 { get { return _type2; } }

		/// <summary>
		///		Gets the lower boundary on the side of First Type
		/// </summary>
		public int GetLowerBoundaryForFirstType { get { return _lowerBoundForFirstType; } }

		/// <summary>
		///		Gets the Upper boundary on the side of First Type
		/// </summary>
		public int GetUpperBoundaryForFirstType { get { return _upperBoundForFirstType; } }

		/// <summary>
		///		Gets the lower boundary on the side of Second Type
		/// </summary>
		public int GetLowerBoundaryForSecondType { get { return _lowerBoundForSecondType;  } }

		/// <summary>
		///		Gets the Upper boundary on the side of Second Type
		/// </summary>
		public int GetUpperBoundaryForSecondType { get { return _upperBoundForSecondType; } }

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

		public void CreateLink(AssociatedObject firstObject, AssociatedObject secondObject)
		{
			if (!_firsTypeHashSet.Contains(firstObject))
			{
				_firsTypeHashSet.Add(firstObject);
			}
			if (!_secondTypeHashSet.Contains(secondObject))
			{
				_secondTypeHashSet.Add(secondObject);
			}
		}

		#endregion //	Methods
	}
}
