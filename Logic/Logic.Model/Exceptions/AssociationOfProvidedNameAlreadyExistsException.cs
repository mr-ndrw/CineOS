using System;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	/// <summary>
	///		An exception that is thrown when an attempt is made to register an association which already exists with provided name in associations scoppe .
	/// </summary>
	public class AssociationOfProvidedNameAlreadyExistsException : Exception
	{
		/// <summary>
		///		Name of the association which was provided and which already exists in the associations scope.
		/// </summary>
		public string ProvidedAssociationName { get; set; }

		/// <summary>
		///		Initalizes a new instance of the AssociationOfProvidedNameAlreadyExistsException class with the name of provided association name.
		/// </summary>
		/// <param name="providedAssocationName">
		///		Name of the association which was provided and which already exists in the associations scope.
		/// </param>
		public AssociationOfProvidedNameAlreadyExistsException(string providedAssocationName)
			: base(string.Format("An association of following name: '{0}' already exists!", providedAssocationName))
		{
			ProvidedAssociationName = providedAssocationName;
		}
	}
}