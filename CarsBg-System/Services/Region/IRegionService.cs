using CarsBg_System.Areas.Admin.Models.Region;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Views.ViewModels.Region;

namespace CarsBg_System.Services.Region
{
    public interface IRegionService
    {

        Task<bool> AddAsync(RegionFormModel region);

        Task<bool> DeleteAsync(int regionId);

        bool IsHaveRegionById(int id);

        IEnumerable<RegionViewModel> GetAllRegions();

        IEnumerable<ShowRegionViewModel> ShowRegions();

    }
}
