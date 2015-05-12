using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

namespace en.AndrewTorski.CineOS.Logic.Model.Association
{
	/// <summary>
	///		Base class for all the classes which fullfill the role an association with an attribute.
	///		Association with an attribute allows linking two objects with an intermediary class which
	///		provides additional information about the relationship of two objects.
	/// </summary>
	/// <typeparam name="TAttribute">
	///		Type which will serve as the attribute in this association. Provides more information about the
	///		relationship between two objects.
	/// </typeparam>
	[DataContract]
	public abstract class AttributeAssociationBase<TAttribute> : AssociationBase
	{
		#region Private Fields

		/// <summary>
		///		Type of the attribute.
		/// </summary>
		[DataMember]
		private readonly Type _attributeType;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes an object of association with specified name and all boundaries for parametrized types.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="firstTypeLowerAmountBoundary">Lower cardinality boundary on the side of First Type.</param>
		/// <param name="firstTypeUpperAmountBoundary">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="secondTypeLowerAmountBoundary">Lower cardinality boundary on the side of Second Type.</param>
		/// <param name="secondTypeUpperAmountBoundary">Upper cardinality boundary on the side of Second Type.</param>
		/// <param name="type1"></param>
		/// <param name="type2"></param>
		protected AttributeAssociationBase(Type type1, Type type2, string name, int firstTypeLowerAmountBoundary, int firstTypeUpperAmountBoundary, int secondTypeLowerAmountBoundary, int secondTypeUpperAmountBoundary)
			: base(type1, type2, name, firstTypeLowerAmountBoundary, firstTypeUpperAmountBoundary, secondTypeLowerAmountBoundary, secondTypeUpperAmountBoundary)
		{
			_attributeType = typeof (TAttribute);
		}

		/// <summary>
		///     Initializes an object of association with specified name and upper amount boundaries for parametrized types. Lower
		///     ones are by default set to 0.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="upperBoundForFirstType">Upper cardinality boundary on the side of First Type.</param>
		/// <param name="upperBoundForSecondType">Upper cardinality boundary on the side of Second Type.</param>
		/// <param name="type1"></param>
		/// <param name="type2"></param>
		protected AttributeAssociationBase(Type type1, Type type2, string name, int upperBoundForFirstType, int upperBoundForSecondType)
			: base(type1, type2, name, upperBoundForFirstType, upperBoundForSecondType)
		{
			_attributeType = typeof(TAttribute);
		}

		/// <summary>
		///     Initializes an object of association with specified name and default values for boundaries. Upper boundaries are
		///     set to int.MaxValue and lower boundaries to 0.
		/// </summary>
		/// <param name="name">Name of the association.</param>
		/// <param name="type1"></param>
		/// <param name="type2"></param>
		protected AttributeAssociationBase(Type type1, Type type2, string name)
			: base(type1, type2, name)
		{
			_attributeType = typeof(TAttribute);
		}

		#endregion

		#region Abstract Methods

		/// <summary>
		///		Creates a Link between two objects along with a attribute.
		/// </summary>
		/// <param name="firstObject">
		///		First object to link.
		/// </param>
		/// <param name="secondObject">
		///		Second object to link.
		/// </param>
		/// <param name="attribute">
		///		Attribute providing more information about the link.
		/// </param>
		public abstract void Link(object firstObject, object secondObject , TAttribute attribute);

		/// <summary>
		///		Returns the collection linked objects to the provided object along with the attribute for each
		///		indivdual link.
		/// </summary>
		/// <param name="obj">
		///		Objects for which a search for linked objects is performed.
		/// </param>
		/// <returns>
		///		Returns the collection of Tuples of attributes and linked objects.
		/// </returns>
		public abstract List<Tuple<TAttribute, object>> GetLinkedAttributesAndObjects(object obj);

		/// <summary>
		///		Returns a collection of all attributes for all the links created for the provided object.
		/// </summary>
		/// <param name="obj">
		///		Object for which a search for attributes is perfomed.
		/// </param>
		/// <returns>
		///		Collection of attributes.
		/// </returns>
		public abstract List<TAttribute> GetLinkedAttributes(object obj);

		#endregion

	}
}