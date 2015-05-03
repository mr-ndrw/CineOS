using System;

namespace en.AndrewTorski.CineOS.Logic.Model.Exceptions
{
	/// <summary>
	///		An exception that is thrown when an attempt is made to link two objects which are already linked within the scope
	///		of the association.
	/// </summary>
	public class ObjectsAlreadyLinkedException : Exception
	{
		/// <summary>
		///		Object which has the role of the Owner and owns the Part.
		/// </summary>
		public Object Owner { get; set; }

		/// <summary>
		///		Object which has the role of the Part and is being owned by the Owner.
		/// </summary>
		public Object Part { get; set; }

		/// <summary>
		///		Initializes a new instance of the ObjectsAlreadyLinkedException class with Owner and Part objects which are already linked.
		/// </summary>
		public ObjectsAlreadyLinkedException(object owner, object part)
			: base("Objects are already linked.")
		{
			Owner = owner;
			Part = part;
		}
	}
}