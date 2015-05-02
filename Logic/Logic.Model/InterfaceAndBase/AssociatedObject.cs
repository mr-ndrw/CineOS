using System;
using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Associations;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
	/// <summary>
	///		TODO COMMENT
	/// </summary>
	public class AssociatedObject : ObjectWithExtent
	{
		#region Private Fields

		/// <summary>
		///     Dictionary of Owner Object and their Part Objects pairs.
		/// </summary>
		private static readonly Dictionary<AssociatedObject, List<AssociatedObject>> OwnerAndPartsDictionary;

		/// <summary>
		///     Dictionary of Associations' names and their correspondent Associations.
		/// </summary>
		private static readonly Dictionary<string, AssociationBase> Assos;

		#endregion //	Private Fields

		#region Constructors

		/// <summary>
		/// </summary>
		protected AssociatedObject()
		{
		}

		/// <summary>
		/// </summary>
		static AssociatedObject()
		{
			OwnerAndPartsDictionary = new Dictionary<AssociatedObject, List<AssociatedObject>>();
			Assos = new Dictionary<string, AssociationBase>();
		}

		#endregion //	Constructors

		#region Static Helpers

		/// <summary>
		///     Commonly used method by static methods which construct Associations. This prevents the multiplication of code in
		///     each of association construction methods.
		/// </summary>
		/// <param name="associationName">
		///     Name of the AssociationRole.
		/// </param>
		/// <param name="lowerBoundForFirstType">
		///     Lower bound for First class.
		///     Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForFirstType">
		///     Upper bound for First class.
		///     Should be greater than zero.
		/// </param>
		/// <param name="lowerBoundForSecondType">
		///     Lower bound for Second class.
		///     Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForSecondType">
		///     Upper bound for Second class.
		///     Should be greater than zero.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///     Thrown if any of Type's are null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///     Thrown if any bounds do not adhere to predefined constaints.
		/// </exception>
		/// <exception cref="ArgumentException">
		///     Thrown if associationName is null, empty or whitespace.
		/// </exception>
		private static void CheckRegistrationParameters(string associationName, int lowerBoundForFirstType, int upperBoundForFirstType,
			int lowerBoundForSecondType, int upperBoundForSecondType)
		{
			if (lowerBoundForFirstType < 0) throw new ArgumentOutOfRangeException("lowerBoundForFirstType", "Lower bound for first type must be greater or equal to zero.");
			if (upperBoundForFirstType <= 0) throw new ArgumentOutOfRangeException("upperBoundForFirstType", "Lower bound for first type must be greater than zero.");
			if (lowerBoundForSecondType < 0) throw new ArgumentOutOfRangeException("lowerBoundForSecondType", "Lower bound for first type must be greater or equal to zero.");
			if (upperBoundForSecondType <= 0) throw new ArgumentOutOfRangeException("upperBoundForSecondType", "Lower bound for first type must be greater than zero.");
			if (string.IsNullOrWhiteSpace(associationName))
			{
				throw new ArgumentException("AssociationRole name cannot be null, empty or whitespace.", "associationName");
			}
			if (DoesAssociationExist(associationName))
			{
				throw new Exception("AssociationRole by such name already exists.");
			}
		}

		#endregion

		#region Static Methods

		/// <summary>
		///     Returns initialized new instance of Association class using provided name and all boundaries for first class type
		///     and second class type.
		///     This method is the core of all AssociationRole registration process and it's here where all associated logic is
		///     contained.
		/// </summary>
		/// <param name="associationName">
		///     Name of the AssociationRole.
		/// </param>
		/// <param name="lowerBoundForFirstType">
		///     Lower bound for First class.
		///     Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForFirstType">
		///     Upper bound for First class.
		///     Should be greater than zero.
		/// </param>
		/// <param name="lowerBoundForSecondType">
		///     Lower bound for Second class.
		///     Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForSecondType">
		///     Upper bound for Second class.
		///     Should be greater than zero.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///     Thrown if any of Type's are null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///     Thrown if any bounds do not adhere to predefined constaints.
		/// </exception>
		/// <exception cref="ArgumentException">
		///     Thrown if associationName is null, empty or whitespace.
		/// </exception>
		private static StandardAssociationBase ConstructStandardAssociation<T1, T2>(string associationName, int lowerBoundForFirstType, int upperBoundForFirstType, int lowerBoundForSecondType, int upperBoundForSecondType)
			where T1 : class
			where T2 : class
		{
			CheckRegistrationParameters(associationName, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType);

			var standardAssociation = new StandardAssociation<T1, T2>(associationName, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType);

			return standardAssociation;
		}

		/// <summary>
		///     Returns new instance of QualifiedAssociation class using provided name and all boundaries for Identifier and
		///     Identifaible as well
		///     as comparer for the Qualifier type.
		/// </summary>
		/// <typeparam name="TIdentifier">
		///     Type of the class that will be the Idenditifer in this Association.
		/// </typeparam>
		/// <typeparam name="TIdentifiable">
		///     Type of the class that will be the Identified in this Association.
		/// </typeparam>
		/// <typeparam name="TQualifier">
		///     Type of the Qualifier which will serve in the search of Identifiables.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="identifierLowerAmountBound">
		///     Lower bound on the Identifier side.
		///     Should be equal or greater than 0.
		/// </param>
		/// <param name="identifierUpperAmountBound">
		///     Upper bound on the Identifier side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="identifiableLowerAmountBound">
		///     Lower bound on the Identifiable side.
		///     Should be equal or greater than 0.
		/// </param>
		/// <param name="identifiableUpperAmountBound">
		///     Upper bound on the Identifiable side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer of the qualifier.
		/// </param>
		/// <returns>
		///     New instance of the QualifiedAssociation class.
		/// </returns>
		private static QualifiedAssociationBase<TQualifier> ConstructQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(string associationName, int identifierLowerAmountBound, int identifierUpperAmountBound, int identifiableLowerAmountBound, int identifiableUpperAmountBound, IEqualityComparer<TQualifier> qualifierEqualityComparer)
			where TIdentifier : class
			where TIdentifiable : class
		{
			if (qualifierEqualityComparer == null) throw new ArgumentNullException("qualifierEqualityComparer");

			CheckRegistrationParameters(associationName, identifierLowerAmountBound, identifierUpperAmountBound, identifiableLowerAmountBound, identifiableUpperAmountBound);

			var qualifiedAssociation = new QualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(associationName, identifierLowerAmountBound, identifierUpperAmountBound, identifiableLowerAmountBound, identifiableUpperAmountBound, qualifierEqualityComparer);

			return qualifiedAssociation;
		}

		/// <summary>
		///     Returns new instance of QualifiedAssociation class using provided name and upper boundaries for Identifier and
		///     Identifaible as well
		///     as comparer for the Qualifier type. Lower boundaries are by default set to 0.
		/// </summary>
		/// <typeparam name="TIdentifier">
		///     Type of the class that will be the Idenditifer in this Association.
		/// </typeparam>
		/// <typeparam name="TIdentifiable">
		///     Type of the class that will be the Identified in this Association.
		/// </typeparam>
		/// <typeparam name="TQualifier">
		///     Type of the Qualifier which will serve in the search of Identifiables.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="identifierUpperAmountBound">
		///     Upper bound on the Identifier side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="identifiableUpperAmountBound">
		///     Upper bound on the Identifiable side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer of the qualifier.
		/// </param>
		/// <returns>
		///     New instance of the QualifiedAssociation class.
		/// </returns>
		private static QualifiedAssociationBase<TQualifier> ConstructQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(string associationName, int identifierUpperAmountBound, int identifiableUpperAmountBound, IEqualityComparer<TQualifier> qualifierEqualityComparer)
			where TIdentifier : class
			where TIdentifiable : class
		{
			return ConstructQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(associationName, 0, identifierUpperAmountBound, 0, identifiableUpperAmountBound, qualifierEqualityComparer);
		}

		#region Association Registration Methods

		/// <summary>
		///     Registers new AssociationRole with specified name, classes used on both ends and all amount boundaries for said
		///     classes.
		/// </summary>
		/// <param name="associationName">
		///     Name of the AssociationRole.
		/// </param>
		/// <param name="lowerBoundForFirstType">
		///     Lower bound for First class.
		///     Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForFirstType">
		///     Upper bound for First class.
		///     Should be greater than zero.
		/// </param>
		/// <param name="lowerBoundForSecondType">
		///     Lower bound for Second class.
		///     Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForSecondType">
		///     Upper bound for Second class.
		///     Should be greater than zero.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///     Thrown if any of Type's are null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///     Thrown if any bounds do not adhere to predefined constaints.
		/// </exception>
		/// <exception cref="ArgumentException">
		///     Thrown if associationName is null, empty or whitespace.
		/// </exception>
		public static void RegisterAssociation<T1, T2>(string associationName,
			int lowerBoundForFirstType, int upperBoundForFirstType,
			int lowerBoundForSecondType, int upperBoundForSecondType)
			where T1 : class
			where T2 : class
		{
			var association = ConstructStandardAssociation<T1, T2>(associationName, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType);

			Assos.Add(associationName, association);
		}

		/// <summary>
		///     Registers new AssociationRole with specified name, classes used on both ends and upper amount boundaries for said
		///     classes with
		///     lower boundaries by default set to 0.
		/// </summary>
		/// <param name="associationName">
		///     Name of the AssociationRole.
		/// </param>
		/// <param name="upperBoundForFirstType">
		///     Upper bound for First class.
		///     Should be greater than zero.
		/// </param>
		/// <param name="upperBoundForSecondType">
		///     Upper bound for Second class.
		///     Should be greater than zero.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///     Thrown if any of Type's are null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///     Thrown if any bounds do not adhere to predefined constaints.
		/// </exception>
		/// <exception cref="ArgumentException">
		///     Thrown if associationName is null, empty or whitespace.
		/// </exception>
		public static void RegisterAssociation<T1, T2>(string associationName, int upperBoundForFirstType, int upperBoundForSecondType)
			where T1 : class
			where T2 : class
		{
			RegisterAssociation<T1, T2>(associationName, 0, upperBoundForFirstType, 0, upperBoundForSecondType);
		}

		/// <summary>
		///     Registers new AssociationRole with specified name, classes used on both ends and boundaries set to many-to-many -
		///     upper boundaries by default are set to int.maxValue and lower boundaries to 0.
		/// </summary>
		/// <param name="associationName">
		///     Name of the AssociationRole.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///     Thrown if any of Type's are null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///     Thrown if any bounds do not adhere to predefined constaints.
		/// </exception>
		/// <exception cref="ArgumentException">
		///     Thrown if associationName is null, empty or whitespace.
		/// </exception>
		public static void RegisterAssociation<T1, T2>(string associationName)
			where T1 : class
			where T2 : class
		{
			RegisterAssociation<T1, T2>(associationName, int.MaxValue, int.MaxValue);
		}

		#endregion //	Associations Registration Methods

		#region Association Link Methods

		public static void Link<T1, T2>(string associationName, T1 firstObject, T2 secondObject)
			where T1 : AssociatedObject
			where T2 : AssociatedObject
		{
			//	First check if such association exists. Else throw an exception.
			if (!DoesAssociationExist(associationName))
			{
				throw new Exception("Association of name " + associationName + " doesn't exist.");
			}
			//	Get this association from dictionary.
			var association = (StandardAssociationBase) Assos[associationName];
			//	And link objects.
			association.Link(firstObject, secondObject);
		}

		public static void Link<T1, T2>(string firstTypeRoleName, T1 firstObject, string secondTypeRoleName, T2 secondObject)
		{
			//	recurrent association
		}

		#endregion

		#region Qualified Associations

		#region Qualifiead Associations Registration Methods

		/// <summary>
		///     Registers a qualified association with specified name, classes used on both ends and all amount boundaries for
		///     Identifier and Identifiable.
		/// </summary>
		/// <typeparam name="TIdentifier">
		///     Type of the class that will be the Idenditifer in this Association.
		/// </typeparam>
		/// <typeparam name="TIdentifiable">
		///     Type of the class that will be the Identified in this Association.
		/// </typeparam>
		/// <typeparam name="TQualifier">
		///     Type of the Qualifier which will serve in the search of Identifiables.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="identifierLowerAmountBound">
		///     Lower bound on the Identifier side.
		///     Should be equal or greater than 0.
		/// </param>
		/// <param name="identifierUpperAmountBound">
		///     Upper bound on the Identifier side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="identifiableLowerAmountBound">
		///     Lower bound on the Identifiable side.
		///     Should be equal or greater than 0.
		/// </param>
		/// <param name="identifiableUpperAmountBound">
		///     Upper bound on the Identifiable side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer of the qualifier.
		/// </param>
		public static void RegisterQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(string associationName, int identifierLowerAmountBound, int identifierUpperAmountBound, int identifiableLowerAmountBound, int identifiableUpperAmountBound, IEqualityComparer<TQualifier> qualifierEqualityComparer)
			where TIdentifier : class
			where TIdentifiable : class
		{
			var qualifiedAssociation = ConstructQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(associationName, identifierLowerAmountBound, identifierUpperAmountBound, identifiableLowerAmountBound, identifiableUpperAmountBound, qualifierEqualityComparer);

			Assos.Add(associationName, qualifiedAssociation);
		}

		/// <summary>
		///     Registers a qualified association with specified name, classes used on both ends and upper amount
		///     boundaries for Identifier and Identifiable. Lower amounts bounds are by default set to 0.
		/// </summary>
		/// <typeparam name="TIdentifier">
		///     Type of the class that will be the Idenditifer in this Association.
		/// </typeparam>
		/// <typeparam name="TIdentifiable">
		///     Type of the class that will be the Identified in this Association.
		/// </typeparam>
		/// <typeparam name="TQualifier">
		///     Type of the Qualifier which will serve in the search of Identifiables.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="identifierUpperAmountBound">
		///     Upper bound on the Identifier side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="identifiableUpperAmountBound">
		///     Upper bound on the Identifiable side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer of the qualifier.
		/// </param>
		public static void RegisterQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(string associationName, int identifierUpperAmountBound, int identifiableUpperAmountBound, IEqualityComparer<TQualifier> qualifierEqualityComparer)
			where TIdentifier : class
			where TIdentifiable : class
		{
			var qualifiedAssociation = ConstructQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(associationName, identifierUpperAmountBound, identifiableUpperAmountBound, qualifierEqualityComparer);

			Assos.Add(associationName, qualifiedAssociation);
		}

		#region QUalified Association Link Methods

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TIdentifier">
		///     Type of the class that will be the Idenditifer in this Association.
		/// </typeparam>
		/// <typeparam name="TIdentifiable">
		///     Type of the class that will be the Identified in this Association.
		/// </typeparam>
		/// <typeparam name="TQualifier">
		///     Type of the Qualifier which will serve in the search of Identifiables.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="identifier"></param>
		/// <param name="identifiable"></param>
		/// <param name="qualifier"></param>
		public static void Link<TIdentifier, TIdentifiable, TQualifier>(string associationName, TIdentifier identifier, TIdentifiable identifiable, TQualifier qualifier) 
			where TIdentifier : class 
			where TIdentifiable : class
		{
			//	Check if association exists, else throw exception
			if (!DoesAssociationExist(associationName))
			{
				throw new Exception("Association of name " + associationName + " doesn't exist.");
			}
			//	If it exists, retrieve it and check if it's a qualified association of paramterized types. 
			//	Perform safe cast.
			var association = Assos[associationName];

			if (!(association is QualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>))
			{
				/*TODO invalid association cast exception?*/throw new Exception("Invalid association cast exception?");
			}
			var qualifiedAssociation = (QualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>)association;
			//	And link it.
			qualifiedAssociation.Link(identifier, identifiable, qualifier);
		}

		public static void Link<TQualifier>(string associationName, object firstObject, object secondObject, TQualifier qualifier)
		{
			//	Check if it exists.
			if (!DoesAssociationExist(associationName))
			{
				throw new Exception("Association of name " + associationName + " doesn't exist.");
			}
			//	If it exists, retrieve it and check if it's a qualified association of parametrized Qualifier type.. 
			//	Perform safe cast.
			var association = Assos[associationName];

			if (!(association is QualifiedAssociationBase<TQualifier>))
			{
				/*TODO invalid association cast exception?*/
				throw new Exception("Invalid association cast exception?");
			}

			var qualifiedAssociation = (QualifiedAssociationBase<TQualifier>) association;

			//	Link it!
			qualifiedAssociation.Link(firstObject, secondObject, qualifier);
		}

		#endregion

		#endregion

		#endregion

		#region Public Helper Static Methods

		/// <summary>
		///     Allows checking whether association by given name exists.
		/// </summary>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <returns>
		///     Bool value.
		/// </returns>
		public static bool DoesAssociationExist(string associationName)
		{
			return Assos.ContainsKey(associationName);
		}

		/// <summary>
		///     Returns the amount boundaries for Association specified by it's name in a Tuple in the following order: 1. Lower
		///     boundary for the first type, 2. Upper boundary for the first type,
		///     3. Lower boundary for the second type,  4. Upper boundary for the second type.
		/// </summary>
		/// <remarks>
		///     If no such Association exists a tuple of four -1's is returned.
		/// </remarks>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <returns>
		///     Tuple of four integers.
		/// </returns>
		public static Tuple<int, int, int, int> GetAmountBoundariesForAssociation(string associationName)
		{
			if (!DoesAssociationExist(associationName))
			{
				return new Tuple<int, int, int, int>(-1, -1, -1, -1);
			}

			var association = Assos[associationName];

			return association.GetAmountBoundaries();
		}

		#endregion

		#endregion //	Static Methods

		#region Methods

		/// <summary>
		///     Returns the collection of linked objects in the specified association.
		/// </summary>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <returns>
		///     Collection of AssociatedObjects.
		/// </returns>
		public List<AssociatedObject> GetLinkedObjects(string associationName)
		{
			if (!DoesAssociationExist(associationName))
			{
				throw new AssociationNotFoundException(associationName);
			}
			var association = (StandardAssociationBase) Assos[associationName];

			var collectionOfLinkedObjects = association.GetAssociatedObjects(this)
				.Cast<AssociatedObject>()
				.ToList();

			return collectionOfLinkedObjects;
		}

		/// <summary>
		///     Links this AssociatedObject with another AssociatedObject in the specified Association.
		/// </summary>
		/// <param name="associationName">
		///     Name of the Association in which we are linking these AssociatedObjects..
		/// </param>
		/// <param name="linkedObject">
		///     Reference to the AssociatedObject with which we want to link this AssociatedObject.
		/// </param>
		public void Link(string associationName, AssociatedObject linkedObject)
		{
			Link(associationName, this, linkedObject);
		}

		public void Link<TQualifier>(string associationName, AssociatedObject linkedObject, TQualifier qualifier)
		{
			Link<TQualifier>(associationName, this, linkedObject, qualifier);
		}

		#endregion
	}
}