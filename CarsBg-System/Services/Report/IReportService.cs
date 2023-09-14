using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Report
{
    public interface IReportService
    {

        bool AddReport(int postId, string userId);

        bool ClearReports(int postId);

        IEnumerable<ShowReportViewModel> ShowAllReports();

    }
}
