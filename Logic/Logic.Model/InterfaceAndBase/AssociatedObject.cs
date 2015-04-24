using System;
using System.Collections.Generic;
using en.AndrewTorski.CineOS.Logic.Model.Enums;

namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
	public class AssociatedObject : ObjectWithExtent
	{
		#region Private Fields

		/// <summary>
		///     Dictionary of Owner Object and their Part Objects pairs.
		/// </summary>
		private static readonly Dictionary<AssociatedObject, List<AssociatedObject>> OwnerAndPartsDictionary;

		/// <summary>
		///		Dictionary of Associations' names and their correspondent Associations.
		/// </summary>
		private static readonly Dictionary<string, Asso> Assos;

		/// <summary>
		///     Collection of typed(named) associations between object(which may be either this object or a qualifier) and any
		///     other number of objects.
		/// </summary>
		private readonly Dictionary<Association, Dictionary<Object, AssociatedObject>> _associations;

		#endregion //	Private Fields

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		protected AssociatedObject()
		{
			_associations = new Dictionary<Association, Dictionary<object, AssociatedObject>>();
		}

		/// <summary>
		/// 
		/// </summary>
		static AssociatedObject()
		{
			OwnerAndPartsDictionary = new Dictionary<AssociatedObject, List<AssociatedObject>>();
			Assos = new Dictionary<string, Asso>();
		}

		#endregion //	Constructors

		#region Static Methods

		/// <summary>
		///		Returns initialized new instance of Asso class using provided name and all boundaries for first class type and second class type.
		///		This method is the core of all Association registration process and it's here where all associated logic is contained.
		/// </summary>
		/// <param name="associationName">
		///		Name of the Association.
		/// </param>
		/// <param name="firstType">
		///		First class which will be used in new Association.
		/// </param>
		/// <param name="secondType">
		///		Second class which will be used in new Association.
		/// </param>
		/// <param name="lowerBoundForFirstType">
		///		Lower bound for First class.
		///		Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForFirstType">
		///		Upper bound for First class.
		///		Should be greater than zero.
		/// </param>
		/// <param name="lowerBoundForSecondType">
		///		Lower bound for Second class.
		///		Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForSecondType">
		///		Upper bound for Second class.
		///		Should be greater than zero.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///		Thrown if any of Type's are null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///		Thrown if any bounds do not adhere to predefined constaints.
		/// </exception>
		/// <exception cref="ArgumentException">
		///		Thrown if associationName is null, empty or whitespace.
		/// </exception>
		private static Asso ConstructAsso(string associationName, Type firstType, Type secondType,
			int lowerBoundForFirstType, int upperBoundForFirstType,
			int lowerBoundForSecondType, int upperBoundForSecondType)
		{
			if (firstType == null) throw new ArgumentNullException("firstType");
			if (secondType == null) throw new ArgumentNullException("secondType");
			if (lowerBoundForFirstType < 0) throw new ArgumentOutOfRangeException("lowerBoundForFirstType", "Lower bound for first type must be greater or equal to zero.");
			if (upperBoundForFirstType <= 0) throw new ArgumentOutOfRangeException("upperBoundForFirstType", "Lower bound for first type must be greater than zero.");
			if (lowerBoundForSecondType < 0) throw new ArgumentOutOfRangeException("lowerBoundForSecondType", "Lower bound for first type must be greater or equal to zero.");
			if (upperBoundForSecondType <= 0) throw new ArgumentOutOfRangeException("upperBoundForSecondType", "Lower bound for first type must be greater than zero.");
			if (string.IsNullOrWhiteSpace(associationName))
			{
				throw new ArgumentException("Association name cannot be null, empty or whitespace.", "associationName");
			}
			if (Assos.ContainsKey(associationName))
			{
				throw new Exception("Association by such name already exists.");
			}

			Asso association = new Asso(associationName, firstType, secondType, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType);

			return association;
		}

		#region Association

		/// <summary>
		///		Registers new Association with specified name, classes used on both ends and all amount boundaries for said classes.
		/// </summary>
		/// <param name="associationName">
		///		Name of the Association.
		/// </param>
		/// <param name="firstType">
		///		First class which will be used in new Association.
		/// </param>
		/// <param name="secondType">
		///		Second class which will be used in new Association.
		/// </param>
		/// <param name="lowerBoundForFirstType">
		///		Lower bound for First class.
		///		Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForFirstType">
		///		Upper bound for First class.
		///		Should be greater than zero.
		/// </param>
		/// <param name="lowerBoundForSecondType">
		///		Lower bound for Second class.
		///		Should be greater, equal to zero
		/// </param>
		/// <param name="upperBoundForSecondType">
		///		Upper bound for Second class.
		///		Should be greater than zero.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///		Thrown if any of Type's are null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///		Thrown if any bounds do not adhere to predefined constaints.
		/// </exception>
		/// <exception cref="ArgumentException">
		///		Thrown if associationName is null, empty or whitespace.
		/// </exception>
		public static void RegisterAssociation(string associationName, Type firstType, Type secondType,
												int lowerBoundForFirstType, int upperBoundForFirstType,
												int lowerBoundForSecondType, int upperBoundForSecondType)
		{
			var association = ConstructAsso(associationName, firstType, secondType, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType);

			Assos.Add(associationName, association);
		}

		/// <summary>
		///		Registers new Association with specified name, classes used on both ends and upper amount boundaries for said classes with
		///		lower boundaries by default set to 0.	
		/// </summary>
		/// <param name="associationName">
		/// 	Name of the Association.
		/// </param>
		/// <param name="firstType">
		/// 	First class which will be used in new Association.
		/// </param>
		/// <param name="secondType">
		/// 	Second class which will be used in new Association.
		///  </param>
		/// <param name="upperBoundForFirstType">
		/// 	Upper bound for First class.
		/// 	Should be greater than zero.
		/// </param>
		/// <param name="upperBoundForSecondType">
		/// 	Upper bound for Second class.
		/// 	Should be greater than zero.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// 	Thrown if any of Type's are null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///		Thrown if any bounds do not adhere to predefined constaints.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown if associationName is null, empty or whitespace.
		/// </exception>
		public static void RegisterAssociation(string associationName, Type firstType, Type secondType,
												int upperBoundForFirstType, int upperBoundForSecondType)
		{
			RegisterAssociation(associationName, firstType, secondType, 0, upperBoundForFirstType, 0, upperBoundForSecondType);
		}

		///  <summary>
		/// 	Registers new Association with specified name, classes used on both ends and boundaries set to many-to-many - upper boundaries
		///		by default are set to int.maxValue and lower boundaries to 0.
		///  </summary>
		///  <param name="associationName">
		/// 	Name of the Association.
		///  </param>
		///  <param name="firstType">
		/// 	First class which will be used in new Association.
		///  </param>
		///  <param name="secondType">
		/// 	Second class which will be used in new Association.
		///  </param>
		///  <exception cref="ArgumentNullException">
		/// 	Thrown if any of Type's are null.
		///  </exception>
		///  <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown if any bounds do not adhere to predefined constaints.
		///  </exception>
		///  <exception cref="ArgumentException">
		/// 	Thrown if associationName is null, empty or whitespace.
		///  </exception>
		public static void RegisterAssociation(string associationName, Type firstType, Type secondType)
		{
			RegisterAssociation(associationName, firstType, secondType, int.MaxValue, int.MaxValue);
		}

		#endregion //	Association

		#region Qualified Associations

		///  <summary>
		///  	Registers a qualified association with specified name, classes used on both ends and all amount boundaries for said classes..
		/// 		No equality comprarer is specified, as we expect this Association to use primitive types for it's qualifier objects. 
		///  </summary>
		///  <param name="associationName">
		///   	Name of the Association.
		///    </param>
		///  <param name="firstType">
		///   	First class which will be used in new Association.
		///  </param>
		/// 	<param name="secondType">
		///   	Second class which will be used in new Association.
		///  </param>am>
		/// <param name="lowerBoundForFirstType">
		///		Lower bound for First class.
		///		Should be greater, equal to zero.
		/// </param>
		/// <param name="upperBoundForFirstType">
		/// 	Upper bound for First class.
		/// 	Should be greater than zero.
		/// </param>
		/// <param name="lowerBoundForSecondType">
		///		Lower bound for Second class.
		///		Should be greater, equal to zero.
		/// </param>
		/// <param name="upperBoundForSecondType">
		/// 	Upper bound for Second class.
		/// 	Should be greater than zero.
		/// </param>
		public static void RegisterQualifiedAssociation<TQualifier>(string associationName, Type firstType, Type secondType,
																	int lowerBoundForFirstType, int upperBoundForFirstType,
																	int lowerBoundForSecondType, int upperBoundForSecondType)
		where TQualifier : IEqualityComparer<TQualifier>
		{
			var association = ConstructAsso(associationName, firstType, secondType, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType);

			association = new QualifiedAsso<TQualifier>(association);

			Assos.Add(associationName, association);
		}

		/// <summary>
		/// 	Registers a qualified association with specified name, classes used on both ends and upper boundaries. Lower boundaries are by default set to 0.
		///		No equality comprarer is specified, as we expect this Association to use primitive types for it's qualifier objects. 
		/// </summary>
		/// <param name="associationName">
		///  	Name of the Association.
		///   </param>
		/// <param name="firstType">
		///  	First class which will be used in new Association.
		/// </param>
		///	<param name="secondType">
		///  	Second class which will be used in new Association.
		/// </param>
		/// <param name="upperBoundForFirstType">
		/// 	Upper bound for First class.
		/// 	Should be greater than zero.
		/// </param>
		/// <param name="upperBoundForSecondType">
		/// 	Upper bound for Second class.
		/// 	Should be greater than zero.
		/// </param>
		public static void RegisterQualifiedAssociation<TQualifier>(string associationName, Type firstType, Type secondType,
																	int upperBoundForFirstType, int upperBoundForSecondType)
			where TQualifier : IEqualityComparer<TQualifier>
		{
			RegisterQualifiedAssociation<TQualifier>(associationName, firstType, secondType, 0, upperBoundForFirstType, 0, upperBoundForSecondType);
		}

		/// <summary>
		/// 	Registers a qualified association with specified name, classes used on both ends and boundaries set to many-to-many - upper boundaries
		///		by default are set to int.maxValue and lower boundaries to 0. No equality comprarer is specified, as we expect this Association to use 
		///		primitive types for it's qualifier objects. 
		/// </summary>
		/// <param name="associationName">
		///  	Name of the Association.
		///   </param>
		/// <param name="firstType">
		///  	First class which will be used in new Association.
		/// </param>
		///	<param name="secondType">
		///  	Second class which will be used in new Association.
		/// </param>
		public static void RegisterQualifiedAssociation<TQualifier>(string associationName, Type firstType, Type secondType)
			where TQualifier : IEqualityComparer<TQualifier>
		{
			RegisterQualifiedAssociation<TQualifier>(associationName, firstType, secondType, int.MaxValue, int.MaxValue);
		}

		/// <summary>
		/// 	Registers a qualified association with specified name, classes used on both ends, upper boundaries and the EqualityComparer for the
		///		qualifier objects which will be used to compare qualifier objects. Lower boundaries are by default set to 0.
		/// </summary>
		/// <typeparam name="TQualifier">
		/// 	Type of the Qualifier.
		/// </typeparam>
		/// <param name="associationName">
		///  	Name of the Association.
		///   </param>
		/// <param name="firstType">
		///  	First class which will be used in new Association.
		/// </param>
		/// <param name="secondType">
		///  	Second class which will be used in new Association.
		///   </param>
		/// <param name="lowerBoundForFirstType">
		///		Lower bound for First class.
		///		Should be greater, equal to zero.
		/// </param>
		/// <param name="upperBoundForFirstType">
		///  	Upper bound for First class.
		///  	Should be greater than zero.
		///   </param>
		/// <param name="lowerBoundForSecondType">
		///		Lower bound for Second class.
		///		Should be greater, equal to zero.
		/// </param>
		/// <param name="upperBoundForSecondType">
		///  	Upper bound for Second class.
		///  	Should be greater than zero.
		/// </param>
		/// <param name="qualifierComparer">
		/// 	Comparer of the Qualifier objects.
		/// </param>
		public static void RegisterQualifiedAssociation<TQualifier>(string associationName, Type firstType, Type secondType,
																	int lowerBoundForFirstType, int upperBoundForFirstType,
																	int lowerBoundForSecondType, int upperBoundForSecondType,
																	IEqualityComparer<TQualifier> qualifierComparer)
			where TQualifier : IEqualityComparer<TQualifier>
		{
			var association = ConstructAsso(associationName, firstType, secondType, lowerBoundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType);

			association = new QualifiedAsso<TQualifier>(association, qualifierComparer);

			Assos.Add(associationName, association);
		}

		/// <summary>
		///		Registers a qualified association with specified name, classes used on both ends, upper boundaries and the EqualityComparer for the
		///		qualifier objects which will be used to compare qualifier objects. Lower boundaries are by default set to 0.
		/// </summary>
		/// <typeparam name="TQualifier">
		///		Type of the Qualifier.
		/// </typeparam>
		///  <param name="associationName">
		/// 	Name of the Association.
		///  </param>
		///  <param name="firstType">
		/// 	First class which will be used in new Association.
		///  </param>
		///  <param name="secondType">
		/// 	Second class which will be used in new Association.
		///  </param>
		/// <param name="upperBoundForFirstType">
		/// 	Upper bound for First class.
		/// 	Should be greater than zero.
		///  </param>
		/// <param name="upperBoundForSecondType">
		/// 	Upper bound for Second class.
		/// 	Should be greater than zero.
		///  </param>
		/// <param name="qualifierComparer">
		///		Comparer of the Qualifier objects.
		/// </param>
		public static void RegisterQualifiedAssociation<TQualifier>(string associationName, Type firstType, Type secondType,
																	int upperBoundForFirstType, int upperBoundForSecondType,
																	IEqualityComparer<TQualifier> qualifierComparer)
			where TQualifier : IEqualityComparer<TQualifier>
		{
			RegisterQualifiedAssociation(associationName, firstType, secondType, 0, upperBoundForFirstType, 0, upperBoundForSecondType, qualifierComparer);
		}

		/// <summary>
		///  	Registers a qualified association with specified name, classes used on both ends, the EqualityComparer for the
		///		qualifier objects which will be used to compare qualifier objects and boundaries set to many-to-many - upper boundaries
		/// 	by default are set to int.maxValue and lower boundaries to 0.
		/// </summary>
		/// <typeparam name="TQualifier">
		///		Type of the Qualifier.
		/// </typeparam>
		/// <param name="associationName">
		///     Name of the Association.
		/// </param>
		/// <param name="firstType">
		///     First class which will be used in new Association.
		/// </param>
		/// <param name="secondType">
		///     Second class which will be used in new Association.
		/// </param>
		/// <param name="qualifierComparer">
		///		Comparer of the Qualifier objects.
		/// </param>
		public static void RegisterQualifiedAssociation<TQualifier>(string associationName, Type firstType, Type secondType,
																	IEqualityComparer<TQualifier> qualifierComparer)
			where TQualifier : IEqualityComparer<TQualifier>
		{
			RegisterQualifiedAssociation(associationName, firstType, secondType, 0, 0, int.MaxValue, int.MaxValue, qualifierComparer);
		}

		#endregion //	Qualified Associations

		#endregion //	Static Methods


	}
}