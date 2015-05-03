using System;
using System.Collections.Generic;
using en.AndrewTorski.CineOS.Logic.Model.Associations;

namespace en.AndrewTorski.CineOS.Logic.Model
{
	/// <summary>
	///		More specialized version of <see cref="association{T1,T2}"/> which provides the ability
	///		of contaning a EqualityComparer which should be used whenever a search for
	///		object would be performed by means of finding them through qualifiers. 
	/// </summary>
	/// <remarks>
	///		EqualityComparer may be custom provided for more complex qualifier classes or
	///		just be left out as null, if we're using primite types(i.e. int, string).
	/// </remarks>
	/// <typeparam name="TQualifier"></typeparam>
	public class QualifiedAsso<T1, T2, TQualifier> : StandardAssociation<T1, T2>
		where TQualifier : IEqualityComparer<TQualifier> where T1 : class where T2 : class
	{
		#region Private Fields

		/// <summary>
		///		Comparer which should be used for comparing qualifier objects for this AssociationRole.
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
		///  	Name of the AssociationRole.
		///   </param>
		/// <param name="firstType">
		///  	First class which will be used in new AssociationRole.
		/// </param>
		/// <param name="secondType">
		///  	Second class which will be used in new AssociationRole.
		///   </param>
		/// <param name="firstTypeLowerAmountBoundary">
		///		Lower bound for First class.
		///		Should be greater, equal to zero.
		/// </param>
		/// <param name="firstTypeUpperAmountBoundary">
		///  	Upper bound for First class.
		///  	Should be greater than zero.
		///   </param>
		/// <param name="partLowerAmountBoundary">
		///		Lower bound for Second class.
		///		Should be greater, equal to zero.
		/// </param>
		/// <param name="secondTypeUpperAmountBoundary">
		///  	Upper bound for Second class.
		///  	Should be greater than zero.
		/// </param>
		/// <param name="qualifierComparer">
		/// 	Comparer of the Qualifier objects.
		/// </param>
		public QualifiedAsso(	string name, Type firstType, Type secondType,
								int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary,
								int partLowerAmountBoundary, int secondTypeUpperAmountBoundary,
								IEqualityComparer<TQualifier> qualifierComparer)
			: base(name, firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary, partLowerAmountBoundary, secondTypeUpperAmountBoundary)
		{
			_qualifierComparer = qualifierComparer;
		}

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends, upper amount 
		///		boundaries(lower are by default 0) and the EqualityComparer for the qualifier objects which will be used to find qualifier objects.
		/// </summary>
		///  <param name="name">
		/// 	Name of the AssociationRole.
		///  </param>
		///  <param name="firstType">
		/// 	First class which will be used in new AssociationRole.
		///  </param>
		///  <param name="secondType">
		/// 	Second class which will be used in new AssociationRole.
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
		///     Name of the AssociationRole.
		/// </param>
		/// <param name="firstType">
		///     First class which will be used in new AssociationRole.
		/// </param>
		/// <param name="secondType">
		///     Second class which will be used in new AssociationRole.
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
		///		Initializes new instance of QulifiedAsso class using copying the values in provided AssociationRole 
		///		and the EqualityComparer for the qualifier objects which will be used to find qualifier objects.
		/// </summary>
		/// <param name="standardAssociation">
		///		AssociationRole from which we initailize.
		/// </param>
		/// <param name="qualifierComparer">
		///		Comparer of the Qualifier objects.
		/// </param>
		public QualifiedAsso(StandardAssociation<T1, T2> standardAssociation, IEqualityComparer<TQualifier> qualifierComparer)
			: this(standardAssociation.Name, standardAssociation.Type1, standardAssociation.Type2, standardAssociation.FirstTypeLowerAmountBoundary, standardAssociation.PartLowerAmountBoundary, standardAssociation.FirstTypeUpperAmountBoundary, standardAssociation.SecondTypeUpperAmountBoundary, qualifierComparer)
		{
		}

		/*
			'EqualityComparer == null' constructors.
		 */

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using copying the values in provided AssociationRole.
		///		No equality comprarer is specified, as we expect this AssociationRole to use primitive types for it's qualifier objects.
		/// </summary>
		/// <param name="standardAssociation">
		///		AssociationRole from which we initailize.
		/// </param>
		public QualifiedAsso(StandardAssociation<T1, T2> standardAssociation)
			: this(standardAssociation, null)
		{

		}

		///  <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends and all amount 
		///		boundaries. No equality comprarer is specified, as we expect this AssociationRole to use primitive types for it's qualifier objects.
		///  </summary>
		///  <param name="name">
		///   	Name of the AssociationRole.
		///    </param>
		///  <param name="firstType">
		///   	First class which will be used in new AssociationRole.
		///  </param>
		///  <param name="secondType">
		///   	Second class which will be used in new AssociationRole.
		///    </param>
		///  <param name="firstTypeLowerAmountBoundary">
		/// 		Lower bound for First class.
		/// 		Should be greater, equal to zero.
		///  </param>
		///  <param name="firstTypeUpperAmountBoundary">
		///   	Upper bound for First class.
		///   	Should be greater than zero.
		///    </param>
		///  <param name="partLowerAmountBoundary">
		/// 		Lower bound for Second class.
		/// 		Should be greater, equal to zero.
		///  </param>
		///  <param name="secondTypeUpperAmountBoundary">
		///   	Upper bound for Second class.
		///   	Should be greater than zero.
		///  </param>
		public QualifiedAsso(	string name, Type firstType, Type secondType,
								int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary,
								int partLowerAmountBoundary, int secondTypeUpperAmountBoundary)
			: this(name, firstType, secondType, firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary, partLowerAmountBoundary, secondTypeUpperAmountBoundary, null)
		{
		}

		/// <summary>
		///		Initializes new instance of QulifiedAsso class using the provided name, class for both ends, upper amount 
		///		boundaries(lower are by default 0). No equality comprarer is specified, as we expect this AssociationRole to 
		///		use primitive types for it's qualifier objects.
		/// </summary>
		/// <param name="name">
		///     Name of the AssociationRole.
		/// </param>
		/// <param name="firstType">
		///     First class which will be used in new AssociationRole.
		/// </param>
		/// <param name="secondType">
		///     Second class which will be used in new AssociationRole.
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
		///		(upper set to int.maxValue and lower to 0). No equality comprarer is specified, as we expect this AssociationRole to use 
		///		primitive types for it's qualifier objects.
		/// </summary>
		/// <param name="name">
		///     Name of the AssociationRole.
		/// </param>
		/// <param name="firstType">
		///     First class which will be used in new AssociationRole.
		/// </param>
		/// <param name="secondType">
		///     Second class which will be used in new AssociationRole.
		/// </param>
		public QualifiedAsso(string name, Type firstType, Type secondType)
			: this(name, firstType, secondType, int.MaxValue, int.MaxValue)
		{
		}
 
		#endregion // Constructors

		#region Properties

		/// <summary>
		///		Gets the comparer which should be used for comparing qualifier objects for this AssociationRole.
		/// </summary>
		/// <remarks>
		///		May be null, if used qualifier is of primitive type.
		/// </remarks>
		public IEqualityComparer<TQualifier> QualifierComparer { get{ return _qualifierComparer; } }

		#endregion //	Properties
		
	}
}