using System;

namespace en.AndrewTorski.CineOS.Logic.Model.Associations
{
	public class Association
	{
		#region Private Fields
		/// <summary>
		///		First Type registered with this AssociationRole.
		/// </summary>
		private readonly Type _type1;

		/// <summary>
		///		First Type registered with this AssociationRole.
		/// </summary>
		private readonly Type _type2;

		/// <summary>
		///		Name of the association.
		/// </summary>
		private readonly string _name;

		/// <summary>
		///		Lower cardinality boundary on the side of First Type.
		/// </summary>
		private readonly int _lowerBoundForFirstType;

		/// <summary>
		///		Upper cardinality boundary on the side of First Type.
		/// </summary>
		private readonly int _upperBoundForFirstType;

		/// <summary>
		///		Lower cardinality boundary on the side of Second Type.
		/// </summary>
		private readonly int _lowerBoundForSecondType;

		/// <summary>
		///		Upper cardinality boundary on the side of Second Type.
		/// </summary>
		private readonly int _upperBoundForSecondType;  

		#endregion

		#region Constructors

		///  <summary>
		/// 		Initializes an object of Association with specified name and all boundaries for parametrized types.
		///  </summary>
		/// <param name="name">Name of the association.</param>
		///  <param name="lowerBoundForFirstType">Lower cardinality boundary on the side of First Type.</param>
		///  <param name="upperBoundForFirstType">Upper cardinality boundary on the side of First Type.</param>
		///  <param name="lowerBoundForSecondType">Lower cardinality boundary on the side of Second Type.</param>
		///  <param name="upperBoundForSecondType">Upper cardinality boundary on the side of Second Type.</param>
		/// <param name="type1"></param>
		/// <param name="type2"></param>
		public Association(Type type1, Type type2, string name, int lowerBoundForFirstType, int upperBoundForFirstType, int lowerBoundForSecondType, int upperBoundForSecondType)
		{
			_type1 = type1;
			_type2 = type2;
			_name = name;
			_lowerBoundForFirstType = lowerBoundForFirstType;
			_upperBoundForFirstType = upperBoundForFirstType;
			_lowerBoundForSecondType = lowerBoundForSecondType;
			_upperBoundForSecondType = upperBoundForSecondType;
		}

		#endregion

		#region Properties
		/// <summary>
		///		Gets the name of the association.
		/// </summary>
		public string Name { get { return _name; } }

		/// <summary>
		///		Gets the first firstType registered with this AssociationRole.
		/// </summary>
		public Type Type1 { get { return _type1; } }

		/// <summary>
		///		Gets the second firstType registered with this AssociationRole.
		/// </summary>
		public Type Type2 { get { return _type2; } }

		/// <summary>
		///		Gets the lower boundary on the side of First Type.
		/// </summary>
		public int GetBoundaryForFirstType { get { return _lowerBoundForFirstType; } }

		/// <summary>
		///		Gets the Upper boundary on the side of First Type
		/// </summary>
		public int UpperBoundaryForFirstType { get { return _upperBoundForFirstType; } }

		/// <summary>
		///		Gets the lower boundary on the side of Second Type
		/// </summary>
		public int LowerBoundaryForSecondType { get { return _lowerBoundForSecondType; } }

		/// <summary>
		///		Gets the Upper boundary on the side of Second Type
		/// </summary>
		public int UpperBoundaryForSecondType { get { return _upperBoundForSecondType; } }

		#endregion //	Properties
	}
}