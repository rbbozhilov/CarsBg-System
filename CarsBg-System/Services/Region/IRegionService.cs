using CarsBg_System.Views.ViewModels.Region;

namespace CarsBg_System.Services.Region
{
    public interface IRegionService
    {

        IEnumerable<RegionViewModel> GetAllRegions();


    }
}
