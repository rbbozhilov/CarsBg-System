using CarsBg_System.Areas.Admin.Models.Region;
using CarsBg_System.Areas.Admin.Views.ViewModels;
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

        public async Task<bool> AddAsync(RegionFormModel region)
        {
            bool isHave = this.data.Regions.Any(x => x.Name == region.Name);

            if (isHave)
            {
                return false;
            }

            var currentRegion = new CarsBg_System.Data.Models.Region() { Name = region.Name };

            await this.data.Regions.AddAsync(currentRegion);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int regionId)
        {
            var region = this.data.Regions.Where(x => x.Id == regionId).FirstOrDefault();

            if (region == null || region.IsDeleted == true)
            {
                return false;
            }

            region.IsDeleted = true;
            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<ShowRegionViewModel> ShowRegions()
        => this.data.Regions
                     .Where(x => x.IsDeleted == false)
                     .Select(x => new ShowRegionViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name
                     })
                    .ToList();


        public bool IsHaveRegionById(int id)
        => this.data.Regions.Any(x => x.Id == id);

        public IEnumerable<RegionViewModel> GetAllRegions()
        => this.data.Regions.Select(x => new RegionViewModel() { Id = x.Id, RegionName = x.Name });

    }
}
