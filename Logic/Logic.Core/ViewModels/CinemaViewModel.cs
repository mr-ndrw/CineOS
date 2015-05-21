using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Core.ViewModels
{
    public class CinemaViewModel
    {
        public CinemaViewModel()
        {
            
        }

        public CinemaViewModel(Cinema cinema)
        {
            Id = cinema.Id;
            Name = cinema.Name;
            Address = cinema.Address;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}