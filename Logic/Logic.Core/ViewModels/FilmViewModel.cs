using System.Reflection.Emit;
using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Core.ViewModels
{
    public class FilmViewModel
    {
        public FilmViewModel(Film film)
        {
            Id = film.Id;
            Length = film.Length;
            PosterUrl = film.PosterUrl;
            Title = film.Title;
            Director = film.Director;
            EsrbRating = film.EsrbRating;
            Genre = film.Genre;
            Actors = film.Actors;
        }

        public int Id { get; set; }
        public int Length { get; set; }
        public string PosterUrl { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string EsrbRating { get; set; }
        public string Genre { get; set; }
        public string[] Actors { get; set; }
    }
}