using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.Association
{
	/// <summary>
	///     Association with an attribute allows linking two objects with an intermediary class which
	///     provides additional information about the relationship of two objects.
	/// </summary>
	/// <typeparam name="TAttribute">
	///     Type which will serve as the attribute in this association. Provides more information about the
	///     relationship between two objects.
	/// </typeparam>
	/// <typeparam name="T1">
	///     First class to associate with an attribute.
	/// </typeparam>
	/// <typeparam name="T2">
	///     Second class to associated with an attribute.
	/// </typeparam>
	public class AttributeAssociation<T1, T2, TAttribute> : AttributeAssociationBase<TAttribute>
		where T1 : class
		where T2 : class
	{
		#region Private Fields

		/// <summary>
		///		Dictionary of links between objects of type T1 and objects of type T2 as well as
		///		attributes for each link.
		/// </summary>
		private readonly Dictionary<T1, List<Tuple<TAttribute, T2>>> _firstTypeToTuplesDictionary;

		/// <summary>
		///		Dictionary of links between objects of type T2 and objects of type T1 as well as
		///		attributes for each link.
		/// </summary>
		private readonly Dictionary<T2, List<Tuple<TAttribute, T1>>> _secondTypeToTuplesDictionary;

		#endregion

		/// <summary>
		///     Initializes an object of association with specified name and all boundaries for parametrized types.
		/// </summary>
		/// <param name="name">
		///		Name of the association.
		/// </param>
		/// <param name="firstTypeLowerAmountBoundary">
		///		Lower cardinality boundary on the side of First Type.
		/// </param>
		/// <param name="firstTypeUpperAmountBoundary">
		///		Upper cardinality boundary on the side of First Type.
		/// </param>
		/// <param name="secondTypeLowerAmountBoundary">
		///		Lower cardinality boundary on the side of Second Type.
		/// </param>
		/// <param name="secondTypeUpperAmountBoundary">
		///		Upper cardinality boundary on the side of Second Type.
		/// </param>
		public AttributeAssociation(string name,
			int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary,
			int secondTypeLowerAmountBoundary, int secondTypeUpperAmountBoundary)
			: base(typeof (T1), typeof (T2), name,
				firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary,
				secondTypeLowerAmountBoundary, secondTypeUpperAmountBoundary)
		{
			_firstTypeToTuplesDictionary = new Dictionary<T1, List<Tuple<TAttribute, T2>>>();
			_secondTypeToTuplesDictionary = new Dictionary<T2, List<Tuple<TAttribute, T1>>>();
		}

		/// <summary>
		///     Initializes an object of association with specified name and upper amount boundaries for parametrized types. Lower
		///     ones are by default set to 0.
		/// </summary>
		/// <param name="name">
		///		Name of the association.
		/// </param>
		/// <param name="upperBoundForFirstType">
		///		Upper cardinality boundary on the side of First Type.
		/// </param>
		/// <param name="upperBoundForSecondType">
		///		Upper cardinality boundary on the side of Second Type.
		/// </param>
		public AttributeAssociation(string name,
			int upperBoundForFirstType, int upperBoundForSecondType)
			: this(name, 0, upperBoundForFirstType, 0, upperBoundForSecondType)
		{
		}

		/// <summary>
		///     Initializes an object of association with specified name and default values for boundaries. Upper boundaries are
		///     set to int.MaxValue and lower boundaries to 0.
		/// </summary>
		/// <param name="name">
		/// Name of the association.
		/// </param>
		public AttributeAssociation(string name)
			: this(name, int.MaxValue, int.MaxValue)
		{
		}

		/// <summary>
		///     Creates a Link between two objects along with a attribute.
		/// </summary>
		/// <param name="firstObject">
		///     Reference to the first object to link.
		/// </param>
		/// <param name="secondObject">
		///     Reference to the second object to link.
		/// </param>
		/// <param name="attribute">
		///     Reference to the attribute providing more information about the link.
		/// </param>
		public override void Link(object firstObject, object secondObject, TAttribute attribute)
		{
			if (firstObject == null) throw new ArgumentNullException("firstObject");
			if (secondObject == null) throw new ArgumentNullException("secondObject");
			if (attribute == null) throw new ArgumentNullException("attribute");

			T1 firstTypedObject;
			T2 secondTypedObject;
			if (firstObject is T1 && secondObject is T2)
			{
				firstTypedObject = (T1) firstObject;
				secondTypedObject = (T2) secondObject;
			}
			else if (firstObject is T2 && secondObject is T1)
			{
				firstTypedObject = (T1) secondObject;
				secondTypedObject = (T2) firstObject;
			}
			else
			{
				throw new TypesNotConformingWithAssociationException(this, firstObject.GetType(), secondObject.GetType());
			}
			Link(firstTypedObject, secondTypedObject, attribute);
		}

		/// <summary>
		///     Creates a Link between two typed objects along with a attribute.
		/// </summary>
		/// <param name="firstTypedObject">
		///     Reference to the first object to link.
		/// </param>
		/// <param name="secondTypedObject">
		///     Reference to the second object to link.
		/// </param>
		/// <param name="attribute">
		///     Reference to the attribute providing more information about the link.
		/// </param>
		public void Link(T1 firstTypedObject, T2 secondTypedObject, TAttribute attribute)
		{
			if (firstTypedObject == null) throw new ArgumentNullException("firstTypedObject");
			if (secondTypedObject == null) throw new ArgumentNullException("secondTypedObject");
			if (attribute == null) throw new ArgumentNullException("attribute");

			List<Tuple<TAttribute, T2>> firstTypedObjectCollectionOfTuples;
			List<Tuple<TAttribute, T1>> secondTypedObjectCollectionOfTuples;

			if (!_firstTypeToTuplesDictionary.ContainsKey(firstTypedObject))
			{
				firstTypedObjectCollectionOfTuples = new List<Tuple<TAttribute, T2>>();
				_firstTypeToTuplesDictionary.Add(firstTypedObject, firstTypedObjectCollectionOfTuples);
			}
			else
			{
				firstTypedObjectCollectionOfTuples = _firstTypeToTuplesDictionary[firstTypedObject];
			}

			if (!_secondTypeToTuplesDictionary.ContainsKey(secondTypedObject))
			{
				secondTypedObjectCollectionOfTuples = new List<Tuple<TAttribute, T1>>();
				_secondTypeToTuplesDictionary.Add(secondTypedObject, secondTypedObjectCollectionOfTuples);
			}
			else
			{
				secondTypedObjectCollectionOfTuples = _secondTypeToTuplesDictionary[secondTypedObject];
			}

			var firstTypeObjectAndAttributeTuple = new Tuple<TAttribute, T1>(attribute, firstTypedObject);
			var secondTypeObjectAndAttributeTuple = new Tuple<TAttribute, T2>(attribute, secondTypedObject);

			firstTypedObjectCollectionOfTuples.Add(secondTypeObjectAndAttributeTuple);
			secondTypedObjectCollectionOfTuples.Add(firstTypeObjectAndAttributeTuple);
		}

		/// <summary>
		///     Returns the collection linked objects to the provided object along with the attribute for each
		///     indivdual link.
		/// </summary>
		/// <param name="obj">
		///     Objects for which a search for linked objects is performed.
		/// </param>
		/// <returns>
		///     Returns the collection of Tuples of attributes and linked objects.
		/// </returns>
		public override List<Tuple<TAttribute, object>> GetLinkedAttributesAndObjects(object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			List<Tuple<TAttribute, object>> result;

			if (obj is T1)
			{
				result = !_firstTypeToTuplesDictionary.ContainsKey((T1) obj)
					? new List<Tuple<TAttribute, object>>()
					: _firstTypeToTuplesDictionary[(T1) obj].Select(tuple => new Tuple<TAttribute, object>(tuple.Item1, tuple.Item2))
						.ToList();
			}
			else if (obj is T2)
			{
				result = !_secondTypeToTuplesDictionary.ContainsKey((T2) obj)
					? new List<Tuple<TAttribute, object>>()
					:_secondTypeToTuplesDictionary[(T2) obj].Select(tuple => new Tuple<TAttribute, object>(tuple.Item1, tuple.Item2)).ToList();
			}
			else
			{
				throw new ObjectTypeDoesntConformAssociationTypesException(this, obj.GetType(), obj);
			}
			return result;
		}

		/// <summary>
		///     Returns the collection of objects associated with provided object.
		/// </summary>
		/// <param name="obj">
		///     Object for which we are looking for linked objects.
		/// </param>
		/// <returns>
		///     Collection of objects.
		/// </returns>
		public override List<object> GetLinkedObjects(object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			var tupleCollectionForObject = GetLinkedAttributesAndObjects(obj);

			var result = tupleCollectionForObject.Select(tuple => tuple.Item2)
				.ToList();

			return result;
		}

		/// <summary>
		///     Returns a collection of all attributes for all the links created for the provided object.
		/// </summary>
		/// <param name="obj">
		///     Object for which a search for attributes is perfomed.
		/// </param>
		/// <returns>
		///     Collection of attributes.
		/// </returns>
		public override List<TAttribute> GetLinkedAttributes(object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			var tupleCollectionForObject = GetLinkedAttributesAndObjects(obj);

			var result = tupleCollectionForObject.Select(tuple => tuple.Item1)
				.ToList();

			return result;
		}
	}
}