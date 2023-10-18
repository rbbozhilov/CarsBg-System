using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Report
{
    public interface IReportService
    {

        Task<bool> AddReportAsync(int postId, string userId);

        Task<bool> ClearReportsAsync(int postId);

        IEnumerable<ShowReportViewModel> ShowAllReports();

    }
}
