using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Extra
{
    public interface IExtraService
    {

        bool Add(ExtraFormModel extra);

        bool Delete(int extraId);

        IEnumerable<ShowExtraViewModel> ShowExtras();

    }
}
