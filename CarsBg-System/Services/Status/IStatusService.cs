using CarsBg_System.Areas.Admin.Models.Status;
using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Status
{
    public interface IStatusService
    {

        Task<bool> AddAsync(StatusFormModel status);

        Task<bool> DeleteAsync(int statusId);

        IEnumerable<ShowStatusViewModel> ShowStatuses();

    }
}
