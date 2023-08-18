using CarsBg_System.Data;
using CarsBg_System.Views.ViewModels.Region;

namespace CarsBg_System.Services.Region
{
    public class RegionService : IRegionService
    {

        private CarsDbContext data;

        public RegionService(CarsDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<RegionViewModel> GetAllRegions()
        => this.data.Regions.Select(x => new RegionViewModel() { Id = x.Id, RegionName = x.Name });
    }
}
