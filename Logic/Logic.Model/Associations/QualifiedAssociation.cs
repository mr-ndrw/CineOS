using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	/// <summary>
	///     Association which enables objects to interact with each other on grounds of  identification of objects.
	/// </summary>
	/// <typeparam name="TIdentifier">
	///     Class type which will identify opposite class by means of qualifiers.
	/// </typeparam>
	/// <typeparam name="TIdentifable">
	///     Class type which will identified by the means of qualifiers.
	/// </typeparam>
	/// <typeparam name="TQualifier">
	///     Type which will be used in identifiaction process.
	/// </typeparam>
	/// <remarks>
	///     IMPORTANT!: Make sure that the interface IEqualityComparer for TQualifier that you will implement and
	///     provide implements correctly GetHashCode(obj) method. By correct what is meant is that for different objects
	/// </remarks>
	public class QualifiedAssociation<TIdentifier, TIdentifable, TQualifier>
		: QualifiedAssociationBase<TQualifier>
		where TIdentifier : class
		where TIdentifable : class
	{
		#region Private Fields

		/// <summary>
		///     Equality comparer for TQualifier which will help HashSet associated with TQualifier properly
		///     administrate memory assignment.
		/// </summary>
		private readonly IEqualityComparer<TQualifier> _qualifierEqualityComparer;

		//	Note: Below two dictionaries exist for the sole purpose of providing us the ability to quickly retrieve linked objects.
		/// <summary>
		///     Dictionary of TIdentifiers and associated Collections of TIdentifiables.
		/// </summary>
		private readonly Dictionary<TIdentifier, List<TIdentifable>> _identifierToIdentifiablesDictionary;

		/// <summary>
		///     Dictionary of TIdentifiables and associated Collections of TIdentifiers.
		/// </summary>
		private readonly Dictionary<TIdentifable, List<TIdentifier>> _identifiableToIdentifiersDictionary;

		/// <summary>
		///     Dictionary of TIdentifiables and associated HashSets of TQualifiers.
		/// </summary>
		/// <remarks>
		///     When searching for certain TIdentifiable(s) for certain TIdenifier using TQualifier, this Dictionary serves as our
		///     starting point.
		/// </remarks>
		private readonly Dictionary<TIdentifier, HashSet<TQualifier>> _identiferToHashSetOfQualifiersDictionary;

		/// <summary>
		///     Dictionary of TQualifiers and associated Collections of TIdentifiables.
		/// </summary>
		/// <remarks>
		///     When searching for certain TIdentifiable(s) qualified by TQualifier, this Dictionary will serve as our end point.
		/// </remarks>
		private readonly Dictionary<TQualifier, List<TIdentifable>> _qualifierToCollectionOfIdentifiablesDictonary;

		#endregion

		/// <summary>
		///     Intitializes a new instance of QualifiedAssociation class with specified name and specified amount bounds for
		///     Identifiers and for Identifiables.
		/// </summary>
		/// <param name="name">
		///     Name of the association.
		/// </param>
		/// <param name="identifierLowerAmountBound">
		///     Lower bound on the side of the the Identifier.
		///     Sets the least amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifierUpperAmountBound">
		///     Upper bound on the side of the the Identifier.
		///     Sets the maximum amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifiableLowerAmountBound">
		///     Lower bound on the side of the the Identifiable.
		///     Sets the least amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		/// <param name="identifiableUpperAmountBound">
		///     Upper bound on the side of the the Identifiable.
		///     Sets the maximum amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer for the qualifier.
		/// </param>
		public QualifiedAssociation(string name, int identifierLowerAmountBound, int identifierUpperAmountBound, int identifiableLowerAmountBound, int identifiableUpperAmountBound,
			IEqualityComparer<TQualifier> qualifierEqualityComparer)
			: base(typeof (TIdentifier), typeof (TIdentifable), name, identifierLowerAmountBound, identifierUpperAmountBound, identifiableLowerAmountBound, identifiableUpperAmountBound)
		{
			_qualifierEqualityComparer = qualifierEqualityComparer;
			_identifierToIdentifiablesDictionary = new Dictionary<TIdentifier, List<TIdentifable>>();
			_identifiableToIdentifiersDictionary = new Dictionary<TIdentifable, List<TIdentifier>>();
			_identiferToHashSetOfQualifiersDictionary = new Dictionary<TIdentifier, HashSet<TQualifier>>();
			_qualifierToCollectionOfIdentifiablesDictonary = new Dictionary<TQualifier, List<TIdentifable>>(_qualifierEqualityComparer);
		}

		/// <summary>
		///     Intitializes a new instance of QualifiedAssociation class with specified name and specified upper amount bounds for
		///     Identifiers and for Identifiables.
		///     Lower amount bounds are by default set to 0.
		/// </summary>
		/// <param name="name">
		///     Name of the association.
		/// </param>
		/// <param name="identifierUpperAmountBound">
		///     Upper bound on the side of the the Identifier.
		///     Sets the maximum amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifiableUpperAmountBound">
		///     Upper bound on the side of the the Identifiable.
		///     Sets the maximum amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///		Comprarer for the qualifier.
		/// </param>
		public QualifiedAssociation(string name, int identifierUpperAmountBound, int identifiableUpperAmountBound, IEqualityComparer<TQualifier> qualifierEqualityComparer)
			: this(name, 0, identifierUpperAmountBound, 0, identifiableUpperAmountBound, qualifierEqualityComparer)
		{
		}

		/// <summary>
		///     TODO Comment
		/// </summary>
		/// <param name="identifier"></param>
		/// <param name="identifable"></param>
		/// <param name="qualifier"></param>
		public void Link(TIdentifier identifier, TIdentifable identifable, TQualifier qualifier)
		{
			LinkObjects(this, _identifierToIdentifiablesDictionary, _identifiableToIdentifiersDictionary, identifier, identifable);
			var identifierExistsInAssociation = _identifierToIdentifiablesDictionary.ContainsKey(identifier);

			HashSet<TQualifier> qualifierHashSet;

			if (!identifierExistsInAssociation)
			{
				qualifierHashSet = new HashSet<TQualifier>(_qualifierEqualityComparer);
				_identiferToHashSetOfQualifiersDictionary.Add(identifier, qualifierHashSet);
			}
			else
			{
				qualifierHashSet = _identiferToHashSetOfQualifiersDictionary[identifier];
				// TODO check if hashSetAlready contains such qualifier and throw exception?
			}
			qualifierHashSet.Add(qualifier);

			List<TIdentifable> identifiables;

			if (!identifierExistsInAssociation)
			{
				identifiables = new List<TIdentifable>();
				_qualifierToCollectionOfIdentifiablesDictonary.Add(qualifier, identifiables);
			}
			else
			{
				identifiables = _qualifierToCollectionOfIdentifiablesDictonary[qualifier];
			}
			identifiables.Add(identifable);
		}

		/// <summary>
		///     TODO COmment
		/// </summary>
		/// <param name="firstObject"></param>
		/// <param name="secondObject"></param>
		/// <param name="qualifierObject"></param>
		public override void Link(object firstObject, object secondObject, TQualifier qualifierObject)
		{
			//	First determine which object is identifier and which one is identifiable.
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
			if (firstObject is TIdentifier)
			{
				var identifier = firstObject as TIdentifier;
				var identifable = secondObject as TIdentifable;
				Link(identifier, identifable, qualifierObject);
			}
			else
			{
				var identifable = firstObject as TIdentifable;
				var identifier = secondObject as TIdentifier;
				Link(identifier, identifable, qualifierObject);
			}
		}

		/// <summary>
		///     TODO Comment
		/// </summary>
		/// <param name="identifierObject"></param>
		/// <param name="qualifier"></param>
		/// <returns></returns>
		public override List<object> GetQualifiedLinkedObjects(object identifierObject, TQualifier qualifier)
		{
			if (identifierObject == null) throw new ArgumentNullException("identifierObject");
			if (qualifier == null) throw new ArgumentNullException("qualifier");

			if (identifierObject.GetType() == typeof (TIdentifier)) throw new ObjectTypeDoesntConformAssociationTypesException(this, identifierObject.GetType(), identifierObject);

			return GetQualifiedLinkedObjects((TIdentifier) identifierObject, qualifier);
		}

		/// <summary>
		///     TODO Comment
		/// </summary>
		/// <param name="identifier"></param>
		/// <param name="qualifier"></param>
		/// <returns></returns>
		public List<object> GetQualifiedLinkedObjects(TIdentifier identifier, TQualifier qualifier)
		{
			if (identifier == null) throw new ArgumentNullException("identifier");
			if (qualifier == null) throw new ArgumentNullException("qualifier");
			//	Check if identifier is present in association
			var identifierExistsInAssociation = _identifierToIdentifiablesDictionary.ContainsKey(identifier);
			//	If it's not - return empty collection.
			if (!identifierExistsInAssociation) return new List<object>();
			//	If it is present, retrieve the hashset of qualifiers associated with it and check if provided qualifier exists 
			//	said hashset.
			var qualifierHashSetForProvidedIdentifier = _identiferToHashSetOfQualifiersDictionary[identifier];
			var qualifierExistsInIdentifierAssociatedQualifierHashSet = qualifierHashSetForProvidedIdentifier.Contains(qualifier);
			//	If it doesn't exist, return empty collection.
			if (!qualifierExistsInIdentifierAssociatedQualifierHashSet) return new List<object>();
			//	If it does - retrieve it from the HashSet. We're doing it because we want the specific qualifier instance which later is used in qualifier-collection of identifiables
			//	dictionary.
			qualifier = qualifierHashSetForProvidedIdentifier.FirstOrDefault(qlfr => _qualifierEqualityComparer.Equals(qlfr, qualifier));
			//	retrieve the collection of identifiables from qualifier-collection of identifaibles dictionary.
			Debug.Assert(qualifier != null, "qualifier != null");
			var identifiedIdenfiables = new List<object>(_qualifierToCollectionOfIdentifiablesDictonary[qualifier]);
			//	And return it.
			return identifiedIdenfiables;
		}

		/// <summary>
		///     TODO Comment
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override List<object> GetLinkedObjects(object obj)
		{
			return GetLinkedObjects(this, _identifierToIdentifiablesDictionary, _identifiableToIdentifiersDictionary, obj);
		}
	}
}