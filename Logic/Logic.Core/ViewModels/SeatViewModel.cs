using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Core.ViewModels
{
    public class SeatViewModel
    {
        public SeatViewModel(string row, string column)
        {
            RowNumber = row;
            ColumnNumber = column;
        }

        public SeatViewModel(Seat seat, bool reserved)
        {
            Id = seat.Id;
            ColumnNumber = seat.ColumnNumber;
            RowNumber = seat.RowNumber;
            Reserved = reserved;
        }

        public int Id { get; set; }
        public string RowNumber { get; set; }
        public string ColumnNumber { get; set; }

        public string RowColumn
        {
            get { return string.Format("{0} {1}", RowNumber, ColumnNumber); }
        }

        public bool Reserved { get; set; }
    }
}
