namespace en.AndrewTorski.CineOS.Logic.Model.Enums
{
	public enum Association
	{
		FromCinemaToRegion, FromRegionToCinema, 
		FromCinemaToProjectionRoom, FromProjectionRoomToCinema,
		FromProjectionRoomToSeat, FromSeatToProjectionRoom,
		FromFilmToMedium, FromMediumToFilm,
		FromProjectionToProjectionRoom, FromProjectionRoomToProjection,
		FromCinemaToProjection, FromProjectionToCinema,
		FromProjectionToFilm, FromFilmToProjection,
		FromReservationToProjection, FromProjectionToReservation,
		FromSeatToReservation, FromReservationToSeat,
		FromReservationToClient, FromClientToReservation,
		FromCinemaToEmployment, FromEmploymentToCinema,
		FromEmployeeToEmployment, FromEmploymentToEmployee,
		FromManagerToSubordinate, FromSubordinateToManager,
		FromMediumToProjection, FromProjectionToMedium
	}
}