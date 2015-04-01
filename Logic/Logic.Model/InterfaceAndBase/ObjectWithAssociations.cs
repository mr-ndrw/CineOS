using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Enums;

namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{

	public abstract class ObjectWithAssociations : ObjectWithExtent
	{
		/// <summary>
		///		Collection of typed(named) associations between this object and any other number of objects.
		/// </summary>
		private Dictionary<Association, Dictionary<Object, ObjectWithAssociations>> _associations;

		/// <summary>
		///		Dictionary of Owner Object and their Part Objects pairs.
		/// </summary>
		private static Dictionary<ObjectWithAssociations, List<ObjectWithAssociations>> _ownerAndPartsDictionary;

		/// <summary>
		///		Initializes the static Owner and Parts dictionary.
		/// </summary>
		static ObjectWithAssociations()
		{
			_ownerAndPartsDictionary = new Dictionary<ObjectWithAssociations, List<ObjectWithAssociations>>();
		}

		/// <summary>
		///		Initializes the object.
		/// </summary>
		protected ObjectWithAssociations()
			: base()
		{
			_associations = new Dictionary<Association, Dictionary<object, ObjectWithAssociations>>();
		}

		private void AddAssociation(Association association, Association reverseAssociation, ObjectWithAssociations targetObject, object qualifier, int counter)
		{
			if (counter < 1) return;
			Dictionary<object, ObjectWithAssociations> thisObjectsAssociations;

			if (_associations.ContainsKey(association))
			{
				thisObjectsAssociations = _associations[association];
			}
			else
			{
				_associations.Add(association, thisObjectsAssociations = new Dictionary<object, ObjectWithAssociations>());
			}

			if (!thisObjectsAssociations.ContainsKey(qualifier))
			{
				thisObjectsAssociations.Add(qualifier, targetObject);
				targetObject.AddAssociation(reverseAssociation, association, this, this, counter -1);
			}
		}

		public void AddAssociation(Association association, Association reverseAssociation, ObjectWithAssociations targetObject, object qualifier)
		{
			AddAssociation(association, reverseAssociation, targetObject, qualifier, 2);
		}

		public void AddAssociation(Association association, Association reverseAssociation, ObjectWithAssociations targetObject)
		{
			AddAssociation(association, association, targetObject, targetObject);
		}

		public void AddPart(Association association, Association reverseAssociation, ObjectWithAssociations targetObject)
		{
			if (_ownerAndPartsDictionary.Values.Any(ownerPartCollection => ownerPartCollection.Contains(targetObject)))
			{
				throw new Exception("Provided object is already a part of some owner object.");
			}
			AddAssociation(association, reverseAssociation, targetObject);

			if (!_ownerAndPartsDictionary.ContainsKey(this))
			{
				_ownerAndPartsDictionary.Add(this, new List<ObjectWithAssociations>{targetObject});
				return;
			}
			_ownerAndPartsDictionary[this].Add(targetObject);
		}

	}
}