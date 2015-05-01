namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
	/// <summary>
	///		Defines a generalized method for testing equality of a Qualifier object and Identifier object.
	///		Qualifier should contain some data that can be derived directly or indirectly from the Identifier Object, and
	///		on that basis their Equality object will be checked.
	/// </summary>
	/// <typeparam name="TQualifier"></typeparam>
	/// <typeparam name="TIdentifiable"></typeparam>
	/// <example>
	///		Seat(eg. in Cinema) class would contain such data as row number, column number etc. etc. Having multiple 
	///		Seat instances linked with an instance of Projection, we'd like now to get a certain seat from this Collection of
	///		Seats based on it's row and column Number.
	/// 
	///		We introduce class SeatCoordinates which contains two attributes - row and column number.
	///		SeatCoordinate will serve us as a Qualifier and Seat as the Identifiable object.
	///		We implement the IQualifierIdentifierEqualityComparer interface by means of 
	///		SeatCoordinatesSeatEqualityComparer<SeatCoordinate, Seat> class and implement declared method therein.
	/// 
	///		public bools Equals(SeatCoordinate qualifier, Seat identifiable)
	///		{
	///			return qualifier.RowNumber == identifiable.RowNumber && qualifier.ColumnNumber == identifiable.ColumnNumber;
	///		}
	/// </example>>
	public interface IQualifierIdentifierEqualityComparer<in TQualifier, in TIdentifiable>
	{
		bool Equals(TQualifier qualifier, TIdentifiable identifable);
	}
}