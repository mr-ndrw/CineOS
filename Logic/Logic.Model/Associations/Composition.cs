using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	/// <summary>
	///		Association which strongly connects Owners and Parts allowing a Part to be linked with only one Owner.
	///		Morever Parts cannot exist without a Owner, but that cannot be directly implemented in this class.
	/// </summary>
	public class Composition<TOwner, TPart> : StandardAssociationBase where TOwner : class where TPart : class
	{
		#region Private Fields

		/// <summary>
		///		Dictionary composed of TOwner objects as keys and collections of TPart objects as values.
		/// </summary>
		private Dictionary<TOwner, List<TPart>> _ownersAndCollectionsOfPartsDictionary;

		/// <summary>
		///		Dictionary which is used to quickly identify if Part object already is composed into some Owner and
		///		is also used to determine what Owner object is the owner of given Part.
		/// </summary>
		private Dictionary<TPart, TOwner> _partsToOwnersDictionary; 

		#endregion
		
		/// <summary>
		///		Initializes a new instance of the Composition class using the name of the association and bounds on the side of the Part.
		/// </summary>
		/// <param name="name">
		///		Name of the association.
		/// </param>
		/// <param name="partLowerAmountBoundary">
		///		Lower amount bound on the side of the Part.
		///		Greater or equal to 0.
		/// </param>
		/// <param name="partUpperAmountBoundary">
		///		Upper amount bound on the side of the Part.
		///		Greater than 0.
		/// </param>
		public Composition(string name, int partLowerAmountBoundary, int partUpperAmountBoundary)
			: base(typeof(TOwner), typeof(TPart), name, 0, 1, partLowerAmountBoundary, partUpperAmountBoundary)
		{
			_ownersAndCollectionsOfPartsDictionary = new Dictionary<TOwner, List<TPart>>();
			_partsToOwnersDictionary = new Dictionary<TPart, TOwner>();
		}

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

			List<object> result;

			if (obj is TOwner)
			{
				var owner = (TOwner)obj;

				if (!_ownersAndCollectionsOfPartsDictionary.ContainsKey(owner))
				{
					result = new List<object>();
				}
				else
				{
					result = _ownersAndCollectionsOfPartsDictionary[owner].Cast<object>()
						.ToList();
				}
			}
			else if (obj is TPart)
			{
				var part = (TPart)obj;
				if (!_partsToOwnersDictionary.ContainsKey(part))
				{
					result = new List<object>();
				}
				else
				{
					var foundOwner = _partsToOwnersDictionary[part];
					result = new List<object>() { foundOwner };
				}

			}
			else
			{
				throw new ObjectTypeDoesntConformAssociationTypesException(this, obj.GetType(), obj);
			}

			return result;
		}

		/// <summary>
		///		Creates a link between two objects within this composition.
		///		This method has means of determining which object is owner and which one is part.
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

			if (firstObject is TOwner && secondObject is TPart)
			{
				var owner = (TOwner)firstObject;
				var part = (TPart)secondObject;
				Link(owner, part);
			}
			else if (firstObject is TPart && secondObject is TOwner)
			{
				var owner = (TOwner)secondObject;
				var part = (TPart)firstObject;
				Link(owner, part);
			}
			else
			{
				throw new TypesNotConformingWithAssociationException(this, firstObject.GetType(), secondObject.GetType());
			}
		}

		/// <summary>
		///		Create a composition link between the objects within this Composition association.
		/// </summary>
		/// <param name="owner">
		///		The owner of the Part.
		/// </param>
		/// <param name="part">
		///		The Part that will be owned.
		/// </param>
		public void Link(TOwner owner, TPart part)
		{
			if (owner == null) throw new ArgumentNullException("owner");
			if (part == null) throw new ArgumentNullException("part");
			//	Check if Part is already associatied with any Owner.
			if (_partsToOwnersDictionary.ContainsKey(part))
			{
				throw new PartAlreadyOwnedException(part);
			}

			List<TPart> ownersPartList;
			//	Check if the owner exists somewhere in the Composition.
			if (!_ownersAndCollectionsOfPartsDictionary.ContainsKey(owner))
			{
				ownersPartList = new List<TPart>();
				_ownersAndCollectionsOfPartsDictionary.Add(owner, ownersPartList);
			}
			else
			{
				ownersPartList = _ownersAndCollectionsOfPartsDictionary[owner];
			}
			//	Check if adding a new Part to Owner's collection would result in PartCollection.Count being greater than 
			//	upper amount bound.
			if (ownersPartList.Count + 1 > SecondTypeUpperAmountBoundary)
			{
				throw new AssociationAmountBoundariesExceededException(this, 1, SecondTypeUpperAmountBoundary);
			}
			//	if it doesn't exceed the upper boundary, add the part to collection and create a new entry in partsToOwnersDictionary
			ownersPartList.Add(part);
			_partsToOwnersDictionary.Add(part, owner);
		} 

		#endregion

	}
}