using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
	/// <summary>
	///		Serves as the base class for class that come into associations with eachother.
	/// </summary>
	public abstract class ObjectWithAssociations : ObjectWithExtent
	{
		/// <summary>
		///     Dictionary of Owner Object and their Part Objects pairs.
		/// </summary>
		private static readonly Dictionary<ObjectWithAssociations, List<ObjectWithAssociations>> OwnerAndPartsDictionary;

		/// <summary>
		///     Collection of typed(named) associations between object(which may be either this object or a qualifier) and any
		///     other number of objects.
		/// </summary>
		private readonly Dictionary<Association, Dictionary<Object, ObjectWithAssociations>> _associations;

		/// <summary>
		///     Initializes the static Owner and Parts dictionary.
		/// </summary>
		static ObjectWithAssociations()
		{
			OwnerAndPartsDictionary = new Dictionary<ObjectWithAssociations, List<ObjectWithAssociations>>();
		}

		/// <summary>
		///     Initializes the class for use by an inherited class instance.
		/// </summary>
		/// <remarks>
		///		This constructor can only be called by an inherited class.
		/// </remarks>
		protected ObjectWithAssociations()
		{
			_associations = new Dictionary<Association, Dictionary<object, ObjectWithAssociations>>();
		}

		/// <summary>
		///     Creates a two way association betwen this Object and the Target Object by using a qualifier.
		/// </summary>
		/// <param name="association">
		///     Association type from this object's direction.
		/// </param>
		/// <param name="reverseAssociation">
		///     Association type from targetPartObject's direction.
		/// </param>
		/// <param name="targetObject">
		///     The ObjectWithAssociations refernce with which we create a association.
		/// </param>
		/// <param name="qualifier">
		///     The object which serves as a qualifier in qualified association.
		/// </param>
		/// <param name="counter">
		///     Counter to prevent infinite callbacks(?).
		/// </param>
		private void AddAssociation(Association association, Association reverseAssociation, ObjectWithAssociations targetObject, object qualifier, int counter)
		{
			if (targetObject == null) throw new ArgumentNullException("targetObject");
			if (qualifier == null) throw new ArgumentNullException("qualifier");

			if (counter < 1) return;
			Dictionary<object, ObjectWithAssociations> thisObjectsAssociations;

			if (_associations.ContainsKey(association))
			{
				thisObjectsAssociations = _associations[association];
			}
			else
			{
				thisObjectsAssociations = new Dictionary<object, ObjectWithAssociations>();
				_associations.Add(association, thisObjectsAssociations);
			}

			if (!thisObjectsAssociations.ContainsKey(qualifier))
			{
				thisObjectsAssociations.Add(qualifier, targetObject);
				targetObject.AddAssociation(reverseAssociation, association, this, this, counter - 1);
			}
		}

		/// <summary>
		///     Creates a two way association betwen this Object and the Target Object with the explicit use of qualifier.
		/// </summary>
		/// <param name="association">
		///     Association type from this object's direction.
		/// </param>
		/// <param name="reverseAssociation">
		///     Association type from targetPartObject's direction.
		/// </param>
		/// <param name="targetObject">
		///     The ObjectWithAssociations refernce with which we create a association.
		/// </param>
		/// <param name="qualifier">
		///     The object which serves as a qualifier in qualified association.
		/// </param>
		protected void AddAssociation(Association association, Association reverseAssociation, ObjectWithAssociations targetObject, object qualifier)
		{
			AddAssociation(association, reverseAssociation, targetObject, qualifier, 2);
		}

		/// <summary>
		///     Creates a two way association betwen this Object and the Target Object.
		/// </summary>
		/// <param name="association">
		///     Association type from this object's direction.
		/// </param>
		/// <param name="reverseAssociation">
		///     Association type from targetPartObject's direction.
		/// </param>
		/// <param name="targetObject">
		///     The ObjectWithAssociations refernce with which we create a association.
		/// </param>
		/// <remarks>
		///     We call AddAssociation(Association,Association,ObjectWithAssociations,object) method inside this method
		///     but we pass the targetPartObject as the qualifier implicitly.
		/// </remarks>
		protected void AddAssociation(Association association, Association reverseAssociation, ObjectWithAssociations targetObject)
		{
			AddAssociation(association, reverseAssociation, targetObject, targetObject);
		}

		/// <summary>
		///     Add the parametrized ObjectWithExtension into this ObjectWithExtension's composition association.
		/// </summary>
		/// <param name="association">
		///     Association type from this object's direction.
		/// </param>
		/// <param name="reverseAssociation">
		///     Association type from targetPartObject's direction.
		/// </param>
		/// <param name="targetPartObject">
		///     The ObjectWithAssociations reference with which we create a association.
		/// </param>
		/// <param name="qualifier">
		///     The object which serves as a qualifier in qualified association
		/// </param>
		protected void AddPart(Association association, Association reverseAssociation, ObjectWithAssociations targetPartObject, object qualifier)
		{
			if (targetPartObject == null) throw new ArgumentNullException("targetPartObject");

			if (OwnerAndPartsDictionary.Values.Any(ownerPartCollection => ownerPartCollection.Contains(targetPartObject)))
			{
				throw new PartAlreadyOwnedException();
			}
			AddAssociation(association, reverseAssociation, targetPartObject, qualifier);

			if (!OwnerAndPartsDictionary.ContainsKey(this))
			{
				OwnerAndPartsDictionary.Add(this, new List<ObjectWithAssociations> {targetPartObject});
				return;
			}
			OwnerAndPartsDictionary[this].Add(targetPartObject);
		}

		/// <summary>
		///     Adds the ObjectWithExtension into this ObjectWithExtension's composition.
		/// </summary>
		/// <param name="association">
		///     Association type from this object's direction.
		/// </param>
		/// <param name="reverseAssociation">
		///     Association type from targetPartObject's direction.
		/// </param>
		/// <param name="targetPartObject">
		///     The ObjectWithAssociations reference with which we create a association.
		/// </param>
		protected void AddPart(Association association, Association reverseAssociation, ObjectWithAssociations targetPartObject)
		{
			AddPart(association, reverseAssociation, targetPartObject, targetPartObject);
		}

		/// <summary>
		///		Adds this Part object to the given Owner object's composition.
		/// </summary>
		/// <param name="association">
		///     Association type from this object's direction.
		/// </param>
		/// <param name="reverseAssociation">
		///     Association type from targetPartObject's direction.
		/// </param>
		/// <param name="targetOwnerObject">
		///     The ObjectWithAssociations reference with which we create a association.
		/// </param>
		protected void AddThisAsPartOf(Association association, Association reverseAssociation, ObjectWithAssociations targetOwnerObject)
		{
			targetOwnerObject.AddPart(association, reverseAssociation, this);
		}


		/// <summary>
		///		Returns the collection of ObjectWithAssociation references which are connected with this ObjectWithAssociations
		///		by the given Association.
		/// </summary>
		/// <returns>
		///		The collection of ObjectWithAssociation references.
		/// </returns>
		protected IEnumerable<ObjectWithAssociations> GetAssociations(Association association)
		{
			var dictonary = _associations[association].Values;
			return dictonary;
		}

		protected ObjectWithAssociations GetQualifiedAssociation(Association association, object qualifier)
		{
			var qualifierDictionary = _associations[association];

			return qualifierDictionary.ContainsKey(qualifier) ? qualifierDictionary[qualifier] : null;
		}

		protected ObjectWithAssociations GetQualifiedAssociation<T>(Association association, T qualifier, IEqualityComparer<T> equalityComparer)
		{
			if (qualifier == null) throw new ArgumentNullException("qualifier");
			if (equalityComparer == null) throw new ArgumentNullException("equalityComparer");
			var qualifierDictonary = _associations[association];

			return (from qualifierObjectPair in qualifierDictonary
					where equalityComparer.Equals((T) qualifierObjectPair.Key, qualifier)
						select qualifierObjectPair.Value)
						.FirstOrDefault();
		}
	}
}