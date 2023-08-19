using CarsBg_System.Views.ViewModels.Region;

namespace CarsBg_System.Services.Region
{
    public interface IRegionService
    {

        bool IsHaveRegionById(int id);

        IEnumerable<RegionViewModel> GetAllRegions();

    }
}
