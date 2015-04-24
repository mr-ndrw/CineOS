using System;
using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model
{
	/// <summary>
	///		More specialized version of <see cref="Asso"/> which provides the ability
	///		of contaning a EqualityComparer which should be used whenever a search for
	///		object would be performed by means of finding them through qualifiers. 
	/// </summary>
	/// <remarks>
	///		EqualityComparer may be custom provided for more complex qualifier classes or
	///		just be left out as null, if we're using primite types(i.e. int, string).
	/// </remarks>
	/// <typeparam name="TQualifier"></typeparam>
	public class QualifiedAsso<T1, T2, TQualifier> : Asso<T1, T2>
		where TQualifier : IEqualityComparer<TQualifier> where T1 : class where T2 : class
	{
		#region Private Fields

		/// <summary>
		///		Comparer which should be used for comparing qualifier objects for this Association.
		/// </summary>
		/// <remarks>
		///		This may be intentionally null, if we predicted that the qualifier object is of simple type like int, or
		/// </remarks>
		private readonly IEqualityComparer<TQualifier> _qualifierComparer; 

		#endregion //	Private Fields

		#region Constructors

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends, all amount 
		///		boundaries and the EqualityComparer for the qualifier objects which will be used to find qualifier objects.
		/// </summary>
		/// <param name="name">
		///  	Name of the Association.
		///   </param>
		/// <param name="firstType">
		///  	First class which will be used in new Association.
		/// </param>
		/// <param name="secondType">
		///  	Second class which will be used in new Association.
		///   </param>
		/// <param name="boundForFirstType">
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
		public QualifiedAsso(	string name, Type firstType, Type secondType,
								int boundForFirstType, int upperBoundForFirstType,
								int lowerBoundForSecondType, int upperBoundForSecondType,
								IEqualityComparer<TQualifier> qualifierComparer)
			: base(name, boundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType)
		{
			_qualifierComparer = qualifierComparer;
		}

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends, upper amount 
		///		boundaries(lower are by default 0) and the EqualityComparer for the qualifier objects which will be used to find qualifier objects.
		/// </summary>
		///  <param name="name">
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
		public QualifiedAsso(	string name, Type firstType, Type secondType,
								int upperBoundForFirstType, int upperBoundForSecondType,
								IEqualityComparer<TQualifier> qualifierComparer)
			: this(name, firstType, secondType, 0, upperBoundForFirstType, 0, upperBoundForSecondType, qualifierComparer)
		{
		}

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends, many-to-many boundaries
		///		(upper set to int.maxValue and lower to 0) and the EqualityComparer for the qualifier objects which will be used to find qualifier objects.
		/// </summary>
		/// <param name="name">
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
		public QualifiedAsso(	string name, Type firstType, Type secondType, 
								IEqualityComparer<TQualifier> qualifierComparer)
			: this(name, firstType, secondType, int.MaxValue, int.MaxValue, qualifierComparer)
		{
		}

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using copying the values in provided Association 
		///		and the EqualityComparer for the qualifier objects which will be used to find qualifier objects.
		/// </summary>
		/// <param name="association">
		///		Association from which we initailize.
		/// </param>
		/// <param name="qualifierComparer">
		///		Comparer of the Qualifier objects.
		/// </param>
		public QualifiedAsso(Asso<T1, T2> association, IEqualityComparer<TQualifier> qualifierComparer)
			: this(association.Name, association.Type1, association.Type2, association.GetBoundaryForFirstType, association.LowerBoundaryForSecondType, association.UpperBoundaryForFirstType, association.UpperBoundaryForSecondType, qualifierComparer)
		{
		}

		/*
			'EqualityComparer == null' constructors.
		 */

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using copying the values in provided Association.
		///		No equality comprarer is specified, as we expect this Association to use primitive types for it's qualifier objects.
		/// </summary>
		/// <param name="association">
		///		Association from which we initailize.
		/// </param>
		public QualifiedAsso(Asso<T1, T2> association)
			: this(association, null)
		{

		}

		///  <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends and all amount 
		///		boundaries. No equality comprarer is specified, as we expect this Association to use primitive types for it's qualifier objects.
		///  </summary>
		///  <param name="name">
		///   	Name of the Association.
		///    </param>
		///  <param name="firstType">
		///   	First class which will be used in new Association.
		///  </param>
		///  <param name="secondType">
		///   	Second class which will be used in new Association.
		///    </param>
		///  <param name="boundForFirstType">
		/// 		Lower bound for First class.
		/// 		Should be greater, equal to zero.
		///  </param>
		///  <param name="upperBoundForFirstType">
		///   	Upper bound for First class.
		///   	Should be greater than zero.
		///    </param>
		///  <param name="lowerBoundForSecondType">
		/// 		Lower bound for Second class.
		/// 		Should be greater, equal to zero.
		///  </param>
		///  <param name="upperBoundForSecondType">
		///   	Upper bound for Second class.
		///   	Should be greater than zero.
		///  </param>
		public QualifiedAsso(	string name, Type firstType, Type secondType,
								int boundForFirstType, int upperBoundForFirstType,
								int lowerBoundForSecondType, int upperBoundForSecondType)
			: this(name, firstType, secondType, boundForFirstType, upperBoundForFirstType, lowerBoundForSecondType, upperBoundForSecondType, null)
		{
		}

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends, upper amount 
		///		boundaries(lower are by default 0). No equality comprarer is specified, as we expect this Association to 
		///		use primitive types for it's qualifier objects.
		/// </summary>
		/// <param name="name">
		///     Name of the Association.
		/// </param>
		/// <param name="firstType">
		///     First class which will be used in new Association.
		/// </param>
		/// <param name="secondType">
		///     Second class which will be used in new Association.
		/// </param>
		///  <param name="upperBoundForFirstType">
		///   	Upper bound for First class.
		///   	Should be greater than zero.
		///    </param>
		///  <param name="upperBoundForSecondType">
		///   	Upper bound for Second class.
		///   	Should be greater than zero.
		///  </param>
		public QualifiedAsso(	string name, Type firstType, Type secondType,
								int upperBoundForFirstType, int upperBoundForSecondType)
			: this(name, firstType, secondType, 0, upperBoundForFirstType, 0, upperBoundForSecondType)
		{
		}

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends, many-to-many boundaries
		///		(upper set to int.maxValue and lower to 0). No equality comprarer is specified, as we expect this Association to use 
		///		primitive types for it's qualifier objects.
		/// </summary>
		/// <param name="name">
		///     Name of the Association.
		/// </param>
		/// <param name="firstType">
		///     First class which will be used in new Association.
		/// </param>
		/// <param name="secondType">
		///     Second class which will be used in new Association.
		/// </param>
		public QualifiedAsso(string name, Type firstType, Type secondType)
			: this(name, firstType, secondType, int.MaxValue, int.MaxValue)
		{
		}
 
		#endregion // Constructors

		#region Properties

		/// <summary>
		///		Gets the comparer which should be used for comparing qualifier objects for this Association.
		/// </summary>
		/// <remarks>
		///		May be null, if used qualifier is of primitive type.
		/// </remarks>
		public IEqualityComparer<TQualifier> QualifierComparer { get{ return _qualifierComparer; } }

		#endregion //	Properties
		
	}
}