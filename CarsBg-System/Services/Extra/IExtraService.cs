using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Extra
{
    public interface IExtraService
    {

        Task<bool> AddAsync(ExtraFormModel extra);

        Task<bool> DeleteAsync(int extraId);

        IEnumerable<ShowExtraViewModel> ShowExtras();

    }
}
