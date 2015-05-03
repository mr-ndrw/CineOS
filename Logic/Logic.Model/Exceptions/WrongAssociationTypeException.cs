using System;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	/// <summary>
	///		An exception which is thrown when an attempt has been made to use certain kind of association as different kind of association.
	/// </summary>
	/// <example>
	///		When an attempt is made to link two objects using association of name 'Example' as StandardAssociation, when in reality it is a qualified associaton.
	/// </example>
	public class WrongAssociationTypeException : Exception
	{
		/// <summary>
		///		Initializes a new instance of the WrongAssociationTypeException class using the name of the association which was being cast/projected, the actual type of the
		///		association and the type to which it was to be casted.
		/// </summary>
		/// <param name="associationName">
		///		Name of the association casted.
		/// </param>
		/// <param name="actualAssociationType">
		///		Actual type of the association.
		/// </param>
		/// <param name="projectedAssociationType">
		///		Projected type of the association.
		/// </param>
		public WrongAssociationTypeException(string associationName, Type actualAssociationType, Type projectedAssociationType)
			:base(ConstructMessageString(associationName, actualAssociationType, projectedAssociationType))
		{
			AssociationName = associationName;
			ActualAssociationType = actualAssociationType;
			ProjectedAssociationType = projectedAssociationType;
		}

		private static string  ConstructMessageString(string associationName, Type existingAssociationType, Type projectedAssociationType)
		{
			const string messagePart = @"Association of name: '{0}' was projected to be of Type: '{1}', but was '{2}'.";
			return string.Format(messagePart, associationName, existingAssociationType.Name, projectedAssociationType.Name);
		}

		/// <summary>
		///		Name of the association.
		/// </summary>
		public string AssociationName { get; set; }

		/// <summary>
		///		Type of the Association which exists under the above name.
		/// </summary>
		public Type ActualAssociationType { get; set; }

		/// <summary>
		///		Type of the Association which was projected to be under the above name.
		/// </summary>
		public Type ProjectedAssociationType { get; set; }
	}
}