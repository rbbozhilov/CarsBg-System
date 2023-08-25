using CarsBg_System.Areas.Admin.Models.Status;
using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Status
{
    public interface IStatusService
    {

        bool Add(StatusFormModel status);

        bool Delete(int statusId);

        IEnumerable<ShowStatusViewModel> ShowStatuses();

    }
}
