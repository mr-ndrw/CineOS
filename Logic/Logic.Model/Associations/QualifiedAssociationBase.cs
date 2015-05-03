using System;
using System.Collections.Generic;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	/// <summary>
	///		TODO COMMENT
	/// </summary>
	/// <typeparam name="TQualifier">
	/// 
	/// </typeparam>
	public abstract class QualifiedAssociationBase<TQualifier> : AssociationBase
	{

		#region Private Fields

		/// <summary>
		///		TODO COMMENT
		/// </summary>
		private readonly Type _qualifierType;

		/// <summary>
		/// 
		/// </summary>
		private readonly int _identifierLowerAmountBoundary;

		/// <summary>
		/// 
		/// </summary>
		private readonly int _identifierUpperAmountBoundary;

		/// <summary>
		/// 
		/// </summary>
		private readonly int _identifiableLowerAmountBoundary;

		/// <summary>
		/// 
		/// </summary>
		private readonly int _identifiableUpperAmountBoundary;

		#endregion

		#region Constructors

		///  <summary>
		/// 		Intitializes a new instance of QualifiedAssociationBase class with specified name and specified amount bounds for Identifiers and for Identifiables.
		///  </summary>
		/// <param name="name">
		///		Name of the association.
		/// </param>
		/// <param name="identifierType">
		///		Type of the Identifier.
		/// </param>
		/// <param name="identifiableType">
		///		Type of the Identifable.
		/// </param>
		/// <param name="identifierLowerAmountBoundary">
		///		Lower bound on the side of the the Identifier.
		///		Sets the least amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifierUpperAmountBoundary">
		///		Upper bound on the side of the the Identifier.
		///		Sets the maximum amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifiableLowerAmountBoundary">
		///		Lower bound on the side of the the Identifiable.
		///		Sets the least amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		/// <param name="identifiableUpperAmountBoundary">
		///		Upper bound on the side of the the Identifiable.
		///		Sets the maximum amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		protected QualifiedAssociationBase(Type identifierType, Type identifiableType, string name, int identifierLowerAmountBoundary, int identifierUpperAmountBoundary, int identifiableLowerAmountBoundary, int identifiableUpperAmountBoundary)
			: base(identifierType, identifiableType, name, identifierLowerAmountBoundary, identifierUpperAmountBoundary, 0 , int.MaxValue)
			//	Boundaries contain within AssociationBase are set to default values - lower to 0 and upper to int.MaxValue
		{
			_identifierLowerAmountBoundary = identifierLowerAmountBoundary;
			_identifierUpperAmountBoundary = identifierUpperAmountBoundary;
			_identifiableLowerAmountBoundary = identifiableLowerAmountBoundary;
			_identifiableUpperAmountBoundary = identifiableUpperAmountBoundary;
			_qualifierType = typeof (TQualifier);
		}

		///  <summary>
		/// 	Intitializes a new instance of QualifiedAssociationBase class with specified name and specified upper amount bounds for Identifiers and for Identifiables.
		///		Lower amount bounds are by default set to 0.	
		///  </summary>
		/// <param name="name">
		///		Name of the association.
		/// </param>
		/// <param name="identifierType">
		///		Type of the Identifier.
		/// </param>
		/// <param name="identifiableType">
		///		Type of the Identifable.
		/// </param>
		/// <param name="identifierUpperAmountBound">
		///		Upper bound on the side of the the Identifier.
		///		Sets the maximum amount of Identifier objects with which one Identifiable object may be linked.
		/// </param>
		/// <param name="identifiableUpperAmountBound">
		///		Upper bound on the side of the the Identifiable.
		///		Sets the maximum amount of Identifiable objects with which one Identifier object may be linked.
		/// </param>
		protected QualifiedAssociationBase(Type identifierType, Type identifiableType, string name, int identifierUpperAmountBound, int identifiableUpperAmountBound)
			:this(identifierType, identifiableType, name, 0, identifierUpperAmountBound, 0, identifiableUpperAmountBound)
		{
			
		}

		#endregion

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public int IdentifierLowerAmountBoundary
		{
			get { return _identifierLowerAmountBoundary; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int IdentifierUpperAmountBoundary
		{
			get { return _identifierUpperAmountBoundary; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int IdentifiableLowerAmountBoundary
		{
			get { return _identifiableLowerAmountBoundary; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int IdentifiableUpperAmountBoundary
		{
			get { return _identifiableUpperAmountBoundary; }
		}

		#endregion

		#region Abstract Methods

		/// <summary>
		///		
		/// </summary>
		/// <param name="firstObject"></param>
		/// <param name="secondObject"></param>
		/// <param name="qualifierObject"></param>
		public abstract void Link(object firstObject, object secondObject, TQualifier qualifierObject);

		/// <summary>
		///		
		/// </summary>
		/// <param name="identifierObject"></param>
		/// <param name="qualifier"></param>
		/// <returns></returns>
		public abstract List<object> GetQualifiedLinkedObjects(object identifierObject, TQualifier qualifier);

		/// <summary>
		///		
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public abstract List<object> GetLinkedObjects(object obj); 

		#endregion
	}
}