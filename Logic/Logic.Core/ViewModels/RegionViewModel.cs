using en.AndrewTorski.CineOS.Logic.Model.Entity;

namespace en.AndrewTorski.CineOS.Logic.Core.ViewModels
{
    public class RegionViewModel
    {
        public RegionViewModel()
        {
            
        }

        public RegionViewModel(Region region)
        {
            Id = region.Id;
            Name = region.Name;
        }

        public RegionViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
