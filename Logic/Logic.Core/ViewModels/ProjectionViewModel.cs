using System;
using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Core.ViewModels
{
    public class ProjectionViewModel
    {
        public ProjectionViewModel(Projection projection)
        {
            Id = projection.Id;
            DateTime = projection.DateTime;
        }

        public int Id { get; set; }

        public DateTime DateTime { get; set; }
    }
}
