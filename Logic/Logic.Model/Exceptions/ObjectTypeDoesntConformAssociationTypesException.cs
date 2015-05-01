using System;
using en.AndrewTorski.CineOS.Logic.Model.Associations;

namespace en.AndrewTorski.CineOS.Logic.Model.Exceptions
{
	/// <summary>
	///		Exception that is thrown when object's type is not conforming with those recognized by the Association.
	/// </summary>
	public class ObjectTypeDoesntConformAssociationTypesException : Exception
	{
		///  <summary>
		/// 		Initializes a new instance of the ObjectTypeDoesntConformAssociationTypesException class with the Association in which
		/// 		exception occured and the Type of the object which didn't conform to Association's type.
		///  </summary>
		///  <param name="association">
		/// 		Association in which excpetion occured.
		///  </param>
		///  <param name="nonConformingObjectsType">
		/// 		Type of the object which didn't conform with the Association.
		///  </param>
		/// <param name="obj">
		///		Object which type didn't conform with association.
		/// </param>
		public ObjectTypeDoesntConformAssociationTypesException(AssociationBase association, Type nonConformingObjectsType, object obj)
		{
			Association = association;
			NonConformingObjectsType = nonConformingObjectsType;
			Object = obj;
		}

		public AssociationBase Association { get; private set; }
		public Type NonConformingObjectsType { get; private set; }
		public object Object { get; set; }
	}
}