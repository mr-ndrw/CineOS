using System;
using System.Collections.Generic;
using System.Diagnostics;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.Association
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
	public class QualifiedAssociation<TIdentifier, TIdentifable, TQualifier> : QualifiedAssociationBase<TQualifier>
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
		private readonly Dictionary<TIdentifier, Dictionary<TQualifier, TQualifier>> _identiferToDictionaryOfQualifiersDictionary;

		/// <summary>
		///     Dictionary of TQualifiers and associated Collections of TIdentifiables.
		/// </summary>
		/// <remarks>
		///     When searching for certain TIdentifiable(s) qualified by TQualifier, this Dictionary will serve as our end point.
		/// </remarks>
		private readonly Dictionary<TQualifier, List<TIdentifable>> _qualifierToCollectionOfIdentifiablesDictonary;

		#endregion

		#region Constructors
		/// <summary>
		///     Intitializes a new instance of QualifiedAssociation class with specified name and specified amount bounds for
		///     Identifiers and for Identifiables.
		/// </summary>
		/// <param name="name">
		///     Name of the association.
		/// </param>
		/// <param name="identifierLowerAmountBoundary">
		///     Lower bound on the side of the the Identifier.
		///     Sets the least amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifierUpperAmountBoundary">
		///     Upper bound on the side of the the Identifier.
		///     Sets the maximum amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifiableLowerAmountBoundary">
		///     Lower bound on the side of the the Identifiable.
		///     Sets the least amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		/// <param name="identifiableUpperAmountBoundary">
		///     Upper bound on the side of the the Identifiable.
		///     Sets the maximum amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer for the qualifier.
		/// </param>
		public QualifiedAssociation(string name, int identifierLowerAmountBoundary, int identifierUpperAmountBoundary, int identifiableLowerAmountBoundary, int identifiableUpperAmountBoundary,
			IEqualityComparer<TQualifier> qualifierEqualityComparer)
			: base(typeof(TIdentifier), typeof(TIdentifable), name, identifierLowerAmountBoundary, identifierUpperAmountBoundary, identifiableLowerAmountBoundary, identifiableUpperAmountBoundary)
		{
			if (qualifierEqualityComparer == null) throw new ArgumentNullException("qualifierEqualityComparer");
			_qualifierEqualityComparer = qualifierEqualityComparer;
			_identifierToIdentifiablesDictionary = new Dictionary<TIdentifier, List<TIdentifable>>();
			_identifiableToIdentifiersDictionary = new Dictionary<TIdentifable, List<TIdentifier>>();
			_identiferToDictionaryOfQualifiersDictionary = new Dictionary<TIdentifier, Dictionary<TQualifier, TQualifier>>();
			_qualifierToCollectionOfIdentifiablesDictonary = new Dictionary<TQualifier, List<TIdentifable>>();
		}

		/// <summary>
		///     Intitializes a new instance of QualifiedAssociation class with specified name and specified upper amount bounds for
		///     Identifiers and for Identifiables.
		///     Lower amount bounds are by default set to 0.
		/// </summary>
		/// <param name="name">
		///     Name of the association.
		/// </param>
		/// <param name="identifierUpperAmountBoundary">
		///     Upper bound on the side of the the Identifier.
		///     Sets the maximum amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifiableUpperAmountBoundary">
		///     Upper bound on the side of the the Identifiable.
		///     Sets the maximum amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer for the qualifier.
		/// </param>
		public QualifiedAssociation(string name, int identifierUpperAmountBoundary, int identifiableUpperAmountBoundary, IEqualityComparer<TQualifier> qualifierEqualityComparer)
			: this(name, 0, identifierUpperAmountBoundary, 0, identifiableUpperAmountBoundary, qualifierEqualityComparer)
		{
		} 
		#endregion

		#region Methods

		/// <summary>
		///     Links Identifier and Identifiable using a Qualifier.
		/// </summary>
		/// <param name="identifier">
		///		Object which is the identifier in newly created link.
		/// </param>
		/// <param name="identifable">
		///		Object will be Identified in newly created link.
		/// </param>
		/// <param name="qualifier">
		///		Qualifier which will be used to identifiy the Identifiable.
		/// </param>
		public void Link(TIdentifier identifier, TIdentifable identifable, TQualifier qualifier)
		{
			if (identifier == null) throw new ArgumentNullException("identifier");
			if (identifable == null) throw new ArgumentNullException("identifable");
			if (qualifier == null) throw new ArgumentNullException("qualifier");

			LinkObjects(this, _identifierToIdentifiablesDictionary, _identifiableToIdentifiersDictionary, identifier, identifable);

			var identifierExistsInAssociation = _identiferToDictionaryOfQualifiersDictionary.ContainsKey(identifier);

			Dictionary<TQualifier, TQualifier> qualifiersDictionary;

			TQualifier associationQualifier;

			if (!identifierExistsInAssociation)
			{
				qualifiersDictionary = new Dictionary<TQualifier, TQualifier>(_qualifierEqualityComparer);
				_identiferToDictionaryOfQualifiersDictionary.Add(identifier, qualifiersDictionary);
				qualifiersDictionary.Add(qualifier, qualifier);
				associationQualifier = qualifier;
			}
			else
			{
				qualifiersDictionary = _identiferToDictionaryOfQualifiersDictionary[identifier];
				/*	If Upper Amount Bound on Identifiable side is greater than 1, we expect that multiple objects(up to Identifiable's Upper amount Bound)
				 *	will be identified by the provided Qualifier.
				 */
				if (!(IdentifiableUpperAmountBoundary > 1) && qualifiersDictionary.ContainsKey(qualifier))
				{
					throw new InvalidQualifiedLinkingOperationException(this);
				}
				//	Retrieve the qualifier from the dictionary
				associationQualifier = qualifiersDictionary[qualifier];
			}

			List<TIdentifable> identifiables;

			if (!identifierExistsInAssociation)
			{
				identifiables = new List<TIdentifable>();
				_qualifierToCollectionOfIdentifiablesDictonary.Add(associationQualifier, identifiables);
			}
			else
			{
				identifiables = _qualifierToCollectionOfIdentifiablesDictonary[associationQualifier];
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
			if (firstObject is TIdentifier && secondObject is TIdentifable)
			{
				var identifier = firstObject as TIdentifier;
				var identifable = secondObject as TIdentifable;
				Link(identifier, identifable, qualifierObject);
			}
			else if (firstObject is TIdentifable && secondObject is TIdentifier)
			{
				var identifable = firstObject as TIdentifable;
				var identifier = secondObject as TIdentifier;
				Link(identifier, identifable, qualifierObject);
			}
			else // Possibly both are identifiers or both are identifiables.
			{
				throw new TypesNotConformingWithAssociationException(this, firstObject.GetType(), secondObject.GetType());
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

			if (identifierObject.GetType() != typeof(TIdentifier)) throw new ObjectTypeDoesntConformAssociationTypesException(this, identifierObject.GetType(), identifierObject);

			return GetQualifiedLinkedObjects((TIdentifier)identifierObject, qualifier);
		}

		/// <summary>
		///     Get all Identifiable objects linked with the Idetnifier using the qualifier object.
		/// </summary>
		/// <param name="identifier">
		///     Identifier for which we are looking for Identifiables.
		/// </param>
		/// <param name="qualifier">
		///     Qualifier with which Identifiables are connected.
		/// </param>
		/// <returns>
		///     Collection of objects.
		/// </returns>
		/// <remarks>
		///     If identifier is not present or qualifier is not connected with identifier in the association, an empty Collection
		///     will be returned.
		/// </remarks>
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
			var qualifiersDictionaryForProvidedIdentifier = _identiferToDictionaryOfQualifiersDictionary[identifier];
			var qualifierExistsInIdentifierAssociatedQualifierHashSet = qualifiersDictionaryForProvidedIdentifier.ContainsKey(qualifier);
			//	If it doesn't exist, return empty collection.
			if (!qualifierExistsInIdentifierAssociatedQualifierHashSet) return new List<object>();
			//	If it does - retrieve it from the HashSet. We're doing it because we want the specific qualifier instance which later is used in qualifier-collection of identifiables
			//	dictionary.
			qualifier = qualifiersDictionaryForProvidedIdentifier[qualifier];
			//	retrieve the collection of identifiables from qualifier-collection of identifaibles dictionary.
			Debug.Assert(qualifier != null, "qualifier != null");
			var identifiedIdenfiables = new List<object>(_qualifierToCollectionOfIdentifiablesDictonary[qualifier]);
			//	And return it.
			return identifiedIdenfiables;
		}

		/// <summary>
		///     Returns
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override List<object> GetLinkedObjects(object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			return GetLinkedObjects(this, _identifierToIdentifiablesDictionary, _identifiableToIdentifiersDictionary, obj);
		}

		#endregion
	}
}