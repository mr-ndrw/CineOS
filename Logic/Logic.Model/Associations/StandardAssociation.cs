using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	/// <summary>
	///		Provides means of containing data about an association such as: what is the associations name, what classes does it
	///		associate and what are the numeric amounts and bounds for each of the classes.
	/// </summary>
	public class StandardAssociation<T1, T2> : StandardAssociationBase 
		where T1 : class 
		where T2 : class
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
		///		Initializes an object of association with specified name and all boundaries for parametrized types.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="lowerBoundForFirstType">Lower cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForFirstType">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="lowerBoundForSecondType">Lower cardinality boundary on the side of Second Type.</param>
		/// <param name="upperBoundForSecondType">Upper cardinality boundary on the side of Second Type.</param>
		public StandardAssociation(string name, int lowerBoundForFirstType, int upperBoundForFirstType, int lowerBoundForSecondType, int upperBoundForSecondType)
			:base(typeof(T1), typeof(T2), name, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType)
		{
			_firstTypeDictionary = new Dictionary<T1, List<T2>>();
			_secondTypeDictionary = new Dictionary<T2, List<T1>>();
		}

		/// <summary>
		///		Initializes an object of association with specified name and upper boundaries for parametrized types. 
		///		Lower boundaries are set by default to 0.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="upperBoundForT1">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForT2">Upper cardinality boundary on the side of Second Type.</param>
		public StandardAssociation(string name, int upperBoundForT1, int upperBoundForT2)
			: this(name, 0, upperBoundForT1, 0, upperBoundForT2){}

		/// <summary>
		///		Initializes an object of association with specified name. 
		///		Lower boundaries are set by default to 0.
		///		Upper boundaries are set by default to int's max value.
		/// </summary>
		public StandardAssociation(string name) :
			this(name, 0, int.MaxValue, 0, int.MaxValue){} 

		#endregion //	Constructors

		#region Methods

		/// <summary>
		///		Returns the collection of objects assocaited with provided object.
		/// </summary>
		/// <param name="obj">
		///		Object for which we retrieve linked objects.
		/// </param>
		/// <returns>
		///		Collection of objects.
		/// </returns>
		/// <remarks>
		///		You may provide either of the types as the parametrized object. Method will recognize
		///		what type was provided(and whether it is conforming with the association.
		/// 
		///		If the provided object doesn't exist in the association's, method will return an empty collection.
		/// </remarks>
		public override List<object> GetAssociatedObjects(object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			if (!ConformsWith(obj.GetType()))
			{
				throw new ObjectTypeDoesntConformAssociationTypesException(this, obj.GetType(), obj);
			}

			if (obj is T1)
			{
				var result = !_firstTypeDictionary.ContainsKey((T1) obj) ? new List<object>() : _firstTypeDictionary[(T1) obj].Cast<object>()
						.ToList();
				return result;
			}

			return !_secondTypeDictionary.ContainsKey((T2)obj) ? new List<object>() : _secondTypeDictionary[(T2)obj].Cast<object>().ToList();
		}

		/// <summary>
		///		Links two typed object within the association.
		/// </summary>
		/// <param name="firstObject">
		///		First typed object.
		/// </param>
		/// <param name="secondObject">
		///		Second typed object.
		/// </param>
		public void Link(T1 firstObject, T2 secondObject)
		{
			LinkObjects(this, _firstTypeDictionary, _secondTypeDictionary, firstObject, secondObject);
		}

		/// <summary>
		///		Creates a link between two objects within this association.
		/// </summary>
		/// <param name="firstObject">
		///		First object.
		/// </param>
		/// <param name="secondObject">
		///		Second object.
		/// </param>
		public override void Link(object firstObject, object secondObject)
		{
			if (firstObject == null) throw new ArgumentNullException("firstObject");
			if (secondObject == null) throw new ArgumentNullException("secondObject");

			//	First identify whether these objects conform with this AssociationRole's types.
			if (!ConformsWith(firstObject.GetType(), secondObject.GetType()))
			{
				throw new TypesNotConformingWithAssociationException(this, firstObject.GetType(), secondObject.GetType());
			}

			//	Now check which is which
			//	And link them
			//	TODO recurrent association might prove silly here, innit?
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
		#endregion //	Methods	
	}
}
