using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	/// <summary>
	///     Serves as a base class for classes which will provide means of containing data about an association such as:
	///     what is the association name, what classes does it associate and what are the numeric amounts and bounds for each
	///     of the classes.
	/// </summary>
	public abstract class AssociationBase : IEquatable<AssociationBase>
	{
		#region Private Fields

		/// <summary>
		///     First Type registered with this AssociationRole.
		/// </summary>
		private readonly Type _type1;

		/// <summary>
		///     First Type registered with this AssociationRole.
		/// </summary>
		private readonly Type _type2;

		/// <summary>
		///     Name of the association.
		/// </summary>
		private readonly string _name;

		/// <summary>
		///     Lower cardinality boundary on the side of First Type.
		/// </summary>
		private readonly int _firstTypeLowerAmountBoundary;

		/// <summary>
		///     Upper cardinality boundary on the side of First Type.
		/// </summary>
		private readonly int _firstTypeUpperAmountBoundary;

		/// <summary>
		///     Lower cardinality boundary on the side of Second Type.
		/// </summary>
		private readonly int _secondTypeLowerAmountBoundary;

		/// <summary>
		///     Upper cardinality boundary on the side of Second Type.
		/// </summary>
		private readonly int _secondTypeUpperAmountBoundary;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes an object of association with specified name and all boundaries for parametrized types.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="firstTypeLowerAmountBoundary">Lower cardinality boundary on the side of First Type.</param>
		/// <param name="firstTypeUpperAmountBoundary">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="secondTypeLowerAmountBoundary">Lower cardinality boundary on the side of Second Type.</param>
		/// <param name="secondTypeUpperAmountBoundary">Upper cardinality boundary on the side of Second Type.</param>
		/// <param name="type1"></param>
		/// <param name="type2"></param>
		protected AssociationBase(Type type1, Type type2, string name, int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary, int secondTypeLowerAmountBoundary, int secondTypeUpperAmountBoundary)
		{
			_type1 = type1;
			_type2 = type2;
			_name = name;
			_firstTypeLowerAmountBoundary = firstTypeLowerAmountBoundary;
			_firstTypeUpperAmountBoundary = firstTypeUpperAmountBoundary;
			_secondTypeLowerAmountBoundary = secondTypeLowerAmountBoundary;
			_secondTypeUpperAmountBoundary = secondTypeUpperAmountBoundary;
		}

		/// <summary>
		///     Initializes an object of association with specified name and upper amount boundaries for parametrized types. Lower
		///     ones are by default set to 0.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="upperBoundForFirstType">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForSecondType">Upper cardinality boundary on the side of Second Type.</param>
		/// <param name="type1"></param>
		/// <param name="type2"></param>
		protected AssociationBase(Type type1, Type type2, string name, int upperBoundForFirstType, int upperBoundForSecondType)
			: this(type1, type2, name, 0, upperBoundForFirstType, 0, upperBoundForSecondType)
		{
		}

		/// <summary>
		///     Initializes an object of association with specified name and default values for boundaries. Upper boundaries are
		///     set to int.MaxValue and lower boundaries to 0.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="type1"></param>
		/// <param name="type2"></param>
		protected AssociationBase(Type type1, Type type2, string name)
			: this(type1, type2, name, int.MaxValue, int.MaxValue)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		///     Gets the name of the association.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		///     Gets the first firstType registered with this AssociationRole.
		/// </summary>
		public Type Type1
		{
			get { return _type1; }
		}

		/// <summary>
		///     Gets the second firstType registered with this AssociationRole.
		/// </summary>
		public Type Type2
		{
			get { return _type2; }
		}

		/// <summary>
		///     Gets the lower boundary on the side of First Type.
		/// </summary>
		public int FirstTypeLowerAmountBoundary
		{
			get { return _firstTypeLowerAmountBoundary; }
		}

		/// <summary>
		///     Gets the Upper boundary on the side of First Type
		/// </summary>
		public int FirstTypeUpperAmountBoundary
		{
			get { return _firstTypeUpperAmountBoundary; }
		}

		/// <summary>
		///     Gets the lower boundary on the side of Second Type
		/// </summary>
		public int SecondTypeLowerAmountBoundary
		{
			get { return _secondTypeLowerAmountBoundary; }
		}

		/// <summary>
		///     Gets the Upper boundary on the side of Second Type
		/// </summary>
		public int SecondTypeUpperAmountBoundary
		{
			get { return _secondTypeUpperAmountBoundary; }
		}

		#endregion //	Properties

		#region Methods

		/// <summary>
		///     Returns the amount boundaries for this association in a Tuple in the following order:
		///     1. Lower boundary for the first type, 2. Upper boundary for the first type, 3. Lower boundary for the second type,
		///     4. Upper boundary for the second type.
		/// </summary>
		/// <returns>
		///     Tuple of four integers.
		/// </returns>
		public Tuple<int, int, int, int> GetAmountBoundaries()
		{
			return new Tuple<int, int, int, int>(_firstTypeLowerAmountBoundary, _firstTypeUpperAmountBoundary, _secondTypeLowerAmountBoundary, _secondTypeUpperAmountBoundary);
		}

		/// <summary>
		///     Compares whether this Assocation is equal to other Assocation.
		/// </summary>
		/// <param name="other">
		///     Other association.
		/// </param>
		/// <returns>
		///     Bool value.
		/// </returns>
		public bool Equals(AssociationBase other)
		{
			return other != null && Name == other.Name; //	Lazy evaluation here.
		}

		/// <summary>
		///     Checks if provided Types conforms any of the Types in association.
		/// </summary>
		/// <param name="type">
		///     Type of object to check.
		/// </param>
		/// <returns>
		///     True if it matches any the Types, false otherwise.
		/// </returns>
		public bool ConformsWith(Type type)
		{
			return type == Type1 || type == Type2;
		}

		/// <summary>
		///     Provides means of determining if provided types conform with those of association's.
		/// </summary>
		/// <param name="type">
		///     First type.
		/// </param>
		/// <param name="secondType">
		///     Second type.
		/// </param>
		/// <returns>
		///     True if types conform with this association, false if they do not.
		/// </returns>
		public bool ConformsWith(Type type, Type secondType)
		{
			return (type == Type1 || type == Type2) || (type == Type2 || type == Type1);
		}

		/// TODO Change to object method?
		/// <summary>
		///     Generic and universal method of linking the objects into dictionaries.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="firstTypeToCollectionOfSecondTypeDictionary"></param>
		/// <param name="secondTypeToCollectionOfFirstTypeDictionary"></param>
		/// <param name="firstObject"></param>
		/// <param name="secondObject"></param>
		protected static void LinkObjects<T1, T2>(AssociationBase association, Dictionary<T1, List<T2>> firstTypeToCollectionOfSecondTypeDictionary,
			Dictionary<T2, List<T1>> secondTypeToCollectionOfFirstTypeDictionary,
			T1 firstObject, T2 secondObject)
		{
			List<T2> firstList;
			List<T1> secondList;

			//	Check if parametrized objects exist at all in this association.
			//	If they don't -  associate a list of opposite type with this object
			//	in the dictionary.
			//	If they do however, just retrive the list of objects of opposite type
			//	from the dictionary.
			//	TODO think about making following more universal - the operation of linking Dictionary's key object and value - linked objec lists seems to be pretty generic and universal
			if (!firstTypeToCollectionOfSecondTypeDictionary.ContainsKey(firstObject))
			{
				firstList = new List<T2>();
				firstTypeToCollectionOfSecondTypeDictionary.Add(firstObject, firstList);
			}
			else
			{
				firstList = firstTypeToCollectionOfSecondTypeDictionary[firstObject];
			}

			if (!secondTypeToCollectionOfFirstTypeDictionary.ContainsKey(secondObject))
			{
				secondList = new List<T1>();
				secondTypeToCollectionOfFirstTypeDictionary.Add(secondObject, secondList);
			}
			else
			{
				secondList = secondTypeToCollectionOfFirstTypeDictionary[secondObject];
			}

			//	Check if they exceed boundaries and throw excpetion if such thing occurs.
			if (firstList.Count + 1 > association.SecondTypeUpperAmountBoundary || secondList.Count + 1 > association.FirstTypeUpperAmountBoundary)
			{
				throw new AssociationAmountBoundariesExceededException(association, (firstList.Count + 1), (secondList.Count + 1));
			}

			//	Add parameter objects to opposite object's collection.
			firstList.Add(secondObject);
			secondList.Add(firstObject);
		}

		/// <summary>
		///     TODO COMMENT
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="association"></param>
		/// <param name="firstTypeObjetsToCollectionOfSecondTypeObjectsDictionary"></param>
		/// <param name="secondTypeObjectsToCollectionOfFirstTypeObjectsDictionary"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		protected static List<object> GetLinkedObjects<T1, T2>(AssociationBase association, Dictionary<T1, List<T2>> firstTypeObjetsToCollectionOfSecondTypeObjectsDictionary, Dictionary<T2, List<T1>> secondTypeObjectsToCollectionOfFirstTypeObjectsDictionary, object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			if (!association.ConformsWith(obj.GetType()))
			{
				throw new ObjectTypeDoesntConformAssociationTypesException(association, obj.GetType(), obj);
			}

			if (obj is T1)
			{
				var result = !firstTypeObjetsToCollectionOfSecondTypeObjectsDictionary.ContainsKey((T1) obj) ? new List<object>() : firstTypeObjetsToCollectionOfSecondTypeObjectsDictionary[(T1) obj].Cast<object>()
					.ToList();
				return result;
			}

			return !secondTypeObjectsToCollectionOfFirstTypeObjectsDictionary.ContainsKey((T2) obj) ? new List<object>() : secondTypeObjectsToCollectionOfFirstTypeObjectsDictionary[(T2) obj].Cast<object>()
				.ToList();
		}

		#endregion
	}
}