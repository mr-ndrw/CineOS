using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using en.AndrewTorski.CineOS.Logic.Model.Association;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;

namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
	/// <summary>
	///     Serves as the base class for class that come into associations with eachother.
	/// </summary>
	public class AssociatedObject
	{
		public static DictionaryContainer DictionaryContainer { get; set; }

		/// <summary>
		///     Gets the Dictionary of Associations' names and their correspondent Associations.
		/// </summary>
		public static Dictionary<string, AssociationBase> AssociationsDictionary { get { return DictionaryContainer.AssociationsDictionary; } }

		/// <summary>
		///		Gets the Dictionary of Key: Types and Values: Collection of objects which are of this tTpe.
		/// </summary>
		public static Dictionary<Type, List<object>> ExtentDictionary { get { return DictionaryContainer.ExtentDictionary; } }

		#region Constructors

		/// <summary>
		///		Intializes a new instance of the PartOfExtent class and subscribes this object
		///		to the Dictionary of Types and Collection of Objects of that type.
		/// </summary>
	    protected AssociatedObject()
        {
            var self = this;
            var selfType = self.GetType();

            List<object> classExtentList;
            if (ExtentDictionary.ContainsKey(selfType))
            {
                classExtentList = ExtentDictionary[selfType];
            }
            else
            {             
                classExtentList = new List<object>();
                ExtentDictionary.Add(selfType, classExtentList);
            }

            classExtentList.Add(self);
        }


		/// <summary>
		/// </summary>
		static AssociatedObject()
		{
			DictionaryContainer = new DictionaryContainer(){AssociationsDictionary =  new Dictionary<string, AssociationBase>(), ExtentDictionary = new Dictionary<Type, List<object>>()};
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
		/// <param name="firstTypeLowerAmountBoundary">
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
		private static void CheckRegistrationParameters(string associationName,
			int firstTypeLowerAmountBoundary, int upperBoundForFirstType,
			int lowerBoundForSecondType, int upperBoundForSecondType)
		{
			if (firstTypeLowerAmountBoundary < 0)
				throw new ArgumentOutOfRangeException("firstTypeLowerAmountBoundary",
					"Lower bound for first type must be greater or equal to zero.");
			if (upperBoundForFirstType <= 0)
				throw new ArgumentOutOfRangeException("upperBoundForFirstType",
					"Lower bound for first type must be greater than zero.");
			if (lowerBoundForSecondType < 0)
				throw new ArgumentOutOfRangeException("lowerBoundForSecondType",
					"Lower bound for first type must be greater or equal to zero.");
			if (upperBoundForSecondType <= 0)
				throw new ArgumentOutOfRangeException("upperBoundForSecondType",
					"Lower bound for first type must be greater than zero.");
			if (string.IsNullOrWhiteSpace(associationName))
			{
				throw new ArgumentException("AssociationRole name cannot be null, empty or whitespace.", "associationName");
			}
			if (ContainsAssociation(associationName))
			{
				throw new AssociationOfProvidedNameAlreadyExistsException(associationName);
			}
		}

		#endregion

		#region Static Methods

		#region Construction Methods
		/// <summary>
		///     Returns initialized new instance of Association class using provided name and all boundaries for first class type
		///     and second class type.
		///     This method is the core of all AssociationRole registration process and it's here where all associated logic is
		///     contained.
		/// </summary>
		/// <param name="associationName">
		///     Name of the AssociationRole.
		/// </param>
		/// <param name="firsTypeLowerAmountBoundary">
		///     Lower bound for First class.
		///     Should be greater, equal to zero
		/// </param>
		/// <param name="firstTypeUpperAmountBoundary">
		///     Upper bound for First class.
		///     Should be greater than zero.
		/// </param>
		/// <param name="secondTypeLowerAmountBoundary">
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
		private static StandardAssociationBase ConstructStandardAssociation<T1, T2>(string associationName,
			int firsTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary,
			int secondTypeLowerAmountBoundary, int upperBoundForSecondType)
			where T1 : class
			where T2 : class
		{
			CheckRegistrationParameters(associationName, firsTypeLowerAmountBoundary, firstTypeUpperAmountBoundary,
				secondTypeLowerAmountBoundary, upperBoundForSecondType);

			var standardAssociation = new StandardAssociation<T1, T2>(associationName,
				firsTypeLowerAmountBoundary, firstTypeUpperAmountBoundary,
				secondTypeLowerAmountBoundary, upperBoundForSecondType);

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
		/// <param name="identifierLowerAmountBoundary">
		///     Lower bound on the Identifier side.
		///     Should be equal or greater than 0.
		/// </param>
		/// <param name="identifierUpperAmountBoundary">
		///     Upper bound on the Identifier side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="identifiableLowerAmountBoundary">
		///     Lower bound on the Identifiable side.
		///     Should be equal or greater than 0.
		/// </param>
		/// <param name="identifiableUpperAmountBoundary">
		///     Upper bound on the Identifiable side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer of the qualifier.
		/// </param>
		/// <returns>
		///     New instance of the QualifiedAssociation class.
		/// </returns>
		private static QualifiedAssociationBase<TQualifier>
			ConstructQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(string associationName,
				int identifierLowerAmountBoundary, int identifierUpperAmountBoundary,
				int identifiableLowerAmountBoundary, int identifiableUpperAmountBoundary,
				IEqualityComparer<TQualifier> qualifierEqualityComparer)
			where TIdentifier : class
			where TIdentifiable : class
		{
			if (qualifierEqualityComparer == null) throw new ArgumentNullException("qualifierEqualityComparer");

			CheckRegistrationParameters(associationName,
				identifierLowerAmountBoundary, identifierUpperAmountBoundary,
				identifiableLowerAmountBoundary, identifiableUpperAmountBoundary);

			var qualifiedAssociation = new QualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(associationName,
				identifierLowerAmountBoundary, identifierUpperAmountBoundary,
				identifiableLowerAmountBoundary, identifiableUpperAmountBoundary,
				qualifierEqualityComparer);

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
		/// <param name="identifierUpperAmountBoundary">
		///     Upper bound on the Identifier side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="identifiableUpperAmountBoundary">
		///     Upper bound on the Identifiable side.
		///     Should be greater than zero.
		/// </param>
		/// <param name="qualifierEqualityComparer">
		///     Comprarer of the qualifier.
		/// </param>
		/// <returns>
		///     New instance of the QualifiedAssociation class.
		/// </returns>
		private static QualifiedAssociationBase<TQualifier> ConstructQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(string associationName,
			int identifierUpperAmountBoundary, int identifiableUpperAmountBoundary,
			IEqualityComparer<TQualifier> qualifierEqualityComparer)
			where TIdentifier : class
			where TIdentifiable : class
		{
			return ConstructQualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>(associationName, 0, identifierUpperAmountBoundary,
				0, identifiableUpperAmountBoundary,
				qualifierEqualityComparer);
		}

		/// <summary>
		///     Constructs and returns an instance of the AttributeAssociation class.
		/// </summary>
		/// <typeparam name="T1">
		///     First class to associate.
		/// </typeparam>
		/// <typeparam name="T2">
		///     Second class to associate.
		/// </typeparam>
		/// <typeparam name="TAttribute">
		///     Type of the attribute which will provide more information about the relationship between two objects of T1 and T2.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="firstTypeLowerAmountBoundary">
		///     Lower amount boundary on the side of the T1 class.
		/// </param>
		/// <param name="firstTypeUpperAmountBoundary">
		///     Upper amount boundary on the side of the T1 class.
		/// </param>
		/// <param name="secondTypeLowerAmountBoundary">
		///     Lower amount boundary on the side of the T2 class.
		/// </param>
		/// <param name="secondTypeUpperAmountBoundary">
		///     Upper amount boundary on the side of the T2 class.
		/// </param>
		/// <returns>
		///     Reference to an object of AttributeAssociation class.
		/// </returns>
		private static AttributeAssociationBase<TAttribute> ConstructAttributeAssociation<T1, T2, TAttribute>(string associationName,
			int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary,
			int secondTypeLowerAmountBoundary, int secondTypeUpperAmountBoundary)
			where T1 : class
			where T2 : class
		{
			CheckRegistrationParameters(associationName, firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary, secondTypeLowerAmountBoundary, secondTypeUpperAmountBoundary);

			var attributeAssociaton = new AttributeAssociation<T1, T2, TAttribute>(associationName, firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary,
				secondTypeLowerAmountBoundary, secondTypeUpperAmountBoundary);

			return attributeAssociaton;
		} 
		#endregion

		#region Standard Association

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
			var association = ConstructStandardAssociation<T1, T2>(associationName,
				lowerBoundForFirstType, upperBoundForFirstType,
				lowerBoundForSecondType, upperBoundForSecondType);

			AssociationsDictionary.Add(associationName, association);
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

		public static void Link(string associationName, AssociatedObject firstObject, AssociatedObject secondObject)
		{
			//	First check if such association exists. Else throw an exception.
			if (!ContainsAssociation(associationName))
			{
				throw new Exception("Association of name " + associationName + " doesn't exist.");
			}
			var foundAssociation = AssociationsDictionary[associationName];
			if (!(foundAssociation is StandardAssociationBase))
			{
				throw new WrongAssociationTypeException(associationName, foundAssociation.GetType(), typeof(StandardAssociationBase));
			}
			//	Get this association from dictionary.
			var association = (StandardAssociationBase)AssociationsDictionary[associationName];
			//	And link objects.
			association.Link(firstObject, secondObject);
		}

		#endregion //	Standard Association

		#region Qualified Associations

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

			AssociationsDictionary.Add(associationName, qualifiedAssociation);
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

			AssociationsDictionary.Add(associationName, qualifiedAssociation);
		}

		/// <summary>
		///     Links two object using the qualifier in the qualified association specified by it's name.
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
		/// <param name="identifier">
		///     Identifier which identifies Identifiables using Qualifier.
		/// </param>
		/// <param name="identifiable">
		///     Object which is identifiable by the qualifier.
		/// </param>
		/// <param name="qualifier">
		///     Qualifier which is used to identify linked objects.
		/// </param>
		public static void LinkWithQualifier<TIdentifier, TIdentifiable, TQualifier>(string associationName, TIdentifier identifier, TIdentifiable identifiable, TQualifier qualifier)
			where TIdentifier : class
			where TIdentifiable : class
		{
			//	Check if association exists, else throw exception
			if (!ContainsAssociation(associationName))
			{
				throw new Exception("Association of name " + associationName + " doesn't exist.");
			}
			//	If it exists, retrieve it and check if it's a qualified association of paramterized types. 
			//	Perform safe cast.
			var association = AssociationsDictionary[associationName];

			if (!(association is QualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>))
			{
				throw new WrongAssociationTypeException(associationName, association.GetType(), typeof (QualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>));
			}
			var qualifiedAssociation = (QualifiedAssociation<TIdentifier, TIdentifiable, TQualifier>) association;
			//	And link it.
			qualifiedAssociation.Link(identifier, identifiable, qualifier);
		}

		/// <summary>
		///     Links two object using the qualifier in the qualified association specified by it's name.
		/// </summary>
		/// <typeparam name="TQualifier">
		///     Type of the Qualifier which is used to identify linked objects.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="firstObject">
		///     First object to link. May be either identifier or identifiable.
		/// </param>
		/// <param name="secondObject">
		///     Second object to link. May be either identifier or identifiable.
		/// </param>
		/// <param name="qualifier">
		///     Qualifier which will be used to determine the identifiable.
		/// </param>
		/// <remarks>
		///     Qualified Association has means of determining which provided object is identifier and which one is a identifiable.
		///     The order in which you will supply the identifier or identifiable to the method doesn't matter.
		/// </remarks>
		public static void LinkWithQualifier<TQualifier>(string associationName, object firstObject, object secondObject, TQualifier qualifier)
		{
			//	Check if it exists.
			if (!ContainsAssociation(associationName))
			{
				throw new Exception("Association of name " + associationName + " doesn't exist.");
			}
			//	If it exists, retrieve it and check if it's a qualified association of parametrized Qualifier type.. 
			//	Perform safe cast.
			var association = AssociationsDictionary[associationName];

			if (!(association is QualifiedAssociationBase<TQualifier>))
			{
				/*TODO invalid association cast exception?*/
				throw new Exception("Invalid association cast exception?");
			}

			var qualifiedAssociation = (QualifiedAssociationBase<TQualifier>) association;

			//	Link it!
			qualifiedAssociation.Link(firstObject, secondObject, qualifier);
		}

		/// <summary>
		///     Returns the Collection of objects linked with this AssociatedObject from the qualified association using the
		///     provided qualifier.
		/// </summary>
		/// <typeparam name="TQualifier">
		///     Type of the Qualifier which is used to identify linked objects.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="qualifier">
		///     Qualifier which is used to identify linked objects.
		/// </param>
		/// <returns>
		///     Collection of objects.
		/// </returns>
		public List<object> GetQualifiedLinkedObject<TQualifier>(string associationName, TQualifier qualifier)
		{
			if (!ContainsAssociation(associationName))
			{
				throw new AssociationNotFoundException(associationName);
			}

			var qualifiedAssociation = (QualifiedAssociationBase<TQualifier>) AssociationsDictionary[associationName];

			var result = qualifiedAssociation.GetQualifiedLinkedObjects(this, qualifier);

			return result;
		}

		#endregion

		#region Composition Association

		/// <summary>
		///     Registers a composition in the system.
		/// </summary>
		/// <typeparam name="TOwner">
		///     Object that fulfills the role of the owner in this association.
		/// </typeparam>
		/// <typeparam name="TPart">
		///     Object that fills the role of the part in this association. Can only be owned by one owner.
		/// </typeparam>
		/// <param name="associatioName">
		///     Name of the new Composition association.
		/// </param>
		/// <param name="partLowerAmountBoundary">
		///     Lower amount boundary on the side of the part.
		///     Should be greater or equal to 0.
		/// </param>
		/// <param name="partUpperAmountBoundary">
		///     Upper amount boundary on the side of the part.
		///     Should be greater than 0.
		/// </param>
		/// <remarks>
		///     Upper and lower amount boundaries on the side of the owner are equal to 1, which is compliant with the
		///     idea of a compositon - a part may only be owned by one owner and cannot exist without a owner.
		///     Use standard Link(string,AssociatedObject,AssociatedObject) method to link objects which come into relation
		///     within this association.
		/// </remarks>
		public static void RegisterComposition<TOwner, TPart>(string associatioName, int partLowerAmountBoundary, int partUpperAmountBoundary)
			where TOwner : class
			where TPart : class
		{
			if (AssociationsDictionary.ContainsKey(associatioName))
			{
				throw new AssociationOfProvidedNameAlreadyExistsException(associatioName);
			}

			var composition = new Composition<TOwner, TPart>(associatioName, partLowerAmountBoundary, partUpperAmountBoundary);

			AssociationsDictionary.Add(composition.Name, composition);
		}

		#endregion	// /Composition Association

		#region Attribute Association

		/// <summary>
		///     Registers an attribute association of provided names, types and amount boundaries.
		///     Attribute association allows linking objects of two classes with an attribute which provides
		///     more information about the link between these two objects.
		/// </summary>
		/// <typeparam name="T1">
		///     Type of the first class to be associated.
		/// </typeparam>
		/// <typeparam name="T2">
		///     Type of the first class to be associated.
		/// </typeparam>
		/// <typeparam name="TAttribute">
		///     Type to be used as the attribute.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="firstTypeLowerAmountBoundary">
		///     Lower amount boundary on the side of the T1 class.
		/// </param>
		/// <param name="firstTypeUpperAmountBoundary">
		///     Upper amount boundary on the side of the T1 class.
		/// </param>
		/// <param name="secondTypeLowerAmountBoundary">
		///     Lower amount boundary on the side of the T2 class.
		/// </param>
		/// <param name="secondTypeUpperAmountBoundary">
		///     Upper amount boundary on the side of the T2 class.
		/// </param>
		public static void RegisterAttributeAssociation<T1, T2, TAttribute>(string associationName,
			int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary,
			int secondTypeLowerAmountBoundary, int secondTypeUpperAmountBoundary)
			where T1 : class
			where T2 : class
		{
			var attributeAssociation = ConstructAttributeAssociation<T1, T2, TAttribute>(associationName, firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary,
				secondTypeLowerAmountBoundary, secondTypeUpperAmountBoundary);

			AssociationsDictionary.Add(attributeAssociation.Name, attributeAssociation);
		}

		/// <summary>
		///     Registers an attribute association of provided names, types and amount boundaries.
		///     Attribute association allows linking objects of two classes with an attribute which provides
		///     more information about the link between these two objects. Lower boundaries on the side of T1 and T2 are
		///     set by default to 0.
		/// </summary>
		/// <typeparam name="T1">
		///     Type of the first class to be associated.
		/// </typeparam>
		/// <typeparam name="T2">
		///     Type of the first class to be associated.
		/// </typeparam>
		/// <typeparam name="TAttribute">
		///     Type to be used as the attribute.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="firstTypeUpperAmountBoundary">
		///     Upper amount boundary on the side of the T1 class.
		/// </param>
		/// <param name="secondTypeUpperAmountBoundary">
		///     Upper amount boundary on the side of the T2 class.
		/// </param>
		public static void RegisterAttributeAssociation<T1, T2, TAttribute>(string associationName, int firstTypeUpperAmountBoundary, int secondTypeUpperAmountBoundary)
			where T1 : class
			where T2 : class
		{
			RegisterAttributeAssociation<T1, T2, TAttribute>(associationName, 0, firstTypeUpperAmountBoundary, 0, secondTypeUpperAmountBoundary);
		}

		/// <summary>
		///     Registers an attribute association of provided names, types and amount boundaries.
		///     Attribute association allows linking objects of two classes with an attribute which provides
		///     more information about the link between these two objects. Lower boundaries on the side of T1 and T2 are
		///     by default set to 0 and upper bounadaries are by default set to int.MaxValue.
		/// </summary>
		/// <typeparam name="T1">
		///     Type of the first class to be associated.
		/// </typeparam>
		/// <typeparam name="T2">
		///     Type of the first class to be associated.
		/// </typeparam>
		/// <typeparam name="TAttribute">
		///     Type to be used as the attribute.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		public static void RegisterAttributeAssociation<T1, T2, TAttribute>(string associationName)
			where T1 : class
			where T2 : class
		{
			RegisterAttributeAssociation<T1, T2, TAttribute>(associationName, int.MaxValue, int.MaxValue);
		}

		/// <summary>
		///     Links two anonymously typed objects within the scope of selected association with an attribute.
		/// </summary>
		/// <typeparam name="TAttribute">
		///     Type to be used as the attribute.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association to use for linking.
		/// </param>
		/// <param name="firstObject">
		///     First object to be linked.
		/// </param>
		/// <param name="secondObject">
		///     Second object to be linked.
		/// </param>
		/// <param name="attribute">
		///     Attribute for new link.
		/// </param>
		public static void LinkWithAttribute<TAttribute>(string associationName, object firstObject, object secondObject, TAttribute attribute)
		{
			if (!ContainsAssociation(associationName))
			{
				throw new AssociationNotFoundException(associationName);
			}
			var association = AssociationsDictionary[associationName];

			if (!(association is AttributeAssociationBase<TAttribute>))
			{
				throw new WrongAssociationTypeException(associationName, association.GetType(), typeof (AttributeAssociationBase<TAttribute>));
			}

			var attributeAssociation = (AttributeAssociationBase<TAttribute>) association;
			attributeAssociation.Link(firstObject, secondObject, attribute);
		}

		/// <summary>
		///     Links this Associated object with provided object in the specified association with a provided attribute.
		/// </summary>
		/// <typeparam name="TAttribute">
		///     Type of the attribute.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association in which objects should be linked.
		/// </param>
		/// <param name="objectToLink">
		///     Object to link with this object.
		/// </param>
		/// <param name="attribute">
		///     Attribute object.
		/// </param>
		public void LinkWithAttribute<TAttribute>(string associationName, AssociatedObject objectToLink, TAttribute attribute)
		{
			LinkWithAttribute(associationName, this, objectToLink, attribute);
		}

		/// <summary>
		///     Returns the collection of Attribute and AssociatedObject pairs which are linked with this object within
		///     the scope of the provided association.
		/// </summary>
		/// <typeparam name="TAttribute">
		///     Type of the attribute.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <returns>
		///     Collection of Tuples of TAttribute and AssociatedObject.
		/// </returns>
		public List<Tuple<TAttribute, AssociatedObject>> GetLinkedAttributesAndObjects<TAttribute>(string associationName)
		{
			if (!ContainsAssociation(associationName))
			{
				throw new AssociationNotFoundException(associationName);
			}
			var association = AssociationsDictionary[associationName];
			if (!(association is AttributeAssociationBase<TAttribute>))
			{
				throw new WrongAssociationTypeException(associationName, association.GetType(), typeof (AttributeAssociationBase<TAttribute>));
			}
			var attributeAssociation = (AttributeAssociationBase<TAttribute>) association;

			return attributeAssociation.GetLinkedAttributesAndObjects(this)
				.Select(tuple => new Tuple<TAttribute, AssociatedObject>(tuple.Item1, (AssociatedObject) tuple.Item2))
				.ToList();
		}

		/// <summary>
		///     Returns the collection of attributes linked with this object within the scope of provided association.
		/// </summary>
		/// <typeparam name="TAttribute">
		///     Type of the attribute.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <returns>
		///     Collection of TAttributes.
		/// </returns>
		public List<TAttribute> GetLinkedAttributes<TAttribute>(string associationName)
		{
			if (!ContainsAssociation(associationName))
			{
				throw new AssociationNotFoundException(associationName);
			}
			var association = AssociationsDictionary[associationName];
			if (!(association is AttributeAssociationBase<TAttribute>))
			{
				throw new WrongAssociationTypeException(associationName, association.GetType(), typeof (AttributeAssociationBase<TAttribute>));
			}
			var attributeAssociation = (AttributeAssociationBase<TAttribute>) association;

			return attributeAssociation.GetLinkedAttributes(this);
		}

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
		public static bool ContainsAssociation(string associationName)
		{
			return AssociationsDictionary.ContainsKey(associationName);
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
			if (!ContainsAssociation(associationName))
			{
				return new Tuple<int, int, int, int>(-1, -1, -1, -1);
			}

			var association = AssociationsDictionary[associationName];

			return association.GetAmountBoundaries();
		}

		#endregion

		#endregion //	Static Methods

		#region Methods

		/// <summary>
		///		Returns the collection of all existing objects of given type in the system.
		/// </summary>
		/// <returns>
		///		Collection of objects.
		/// </returns>
		/// <remarks>
		///		Returns empty collection if no such class exists in the system..
		/// </remarks>
		protected static IEnumerable<object> RetrieveExtentFor(Type type)
		{
			return !ExtentDictionary.ContainsKey(type) ? new List<object>() : ExtentDictionary[type];
		}

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
			if (!ContainsAssociation(associationName))
			{
				throw new AssociationNotFoundException(associationName);
			}

			var association = AssociationsDictionary[associationName];

			var collectionOfLinkedObjects = association.GetLinkedObjects(this)
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

		/// <summary>
		///     Links this AssociatedObject and the supplied AssociatedObject using the qualifier in the qualified association
		///     specified
		///     by it's name.
		/// </summary>
		/// <typeparam name="TQualifier">
		///     Type of the Qualifier which is used to identify linked objects.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the association.
		/// </param>
		/// <param name="linkedObject">
		///     Object which we are going to link with this AssociatedObject.
		/// </param>
		/// <param name="qualifier">
		///     Qualifier which will be used to determine the identifiable.
		/// </param>
		/// <remarks>
		///     Qualified Association has means of determining which provided object is identifier and which one is a identifiable.
		///     It won't matter if you'll call this method on the identifier or the identifiable.
		/// </remarks>
		public void LinkWithQualifier<TQualifier>(string associationName, AssociatedObject linkedObject, TQualifier qualifier)
		{
			LinkWithQualifier<TQualifier>(associationName, this, linkedObject, qualifier);
		}

		#endregion
	}
}