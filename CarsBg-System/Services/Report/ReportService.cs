using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data;
using Microsoft.EntityFrameworkCore;

namespace CarsBg_System.Services.Report
{
    public class ReportService : IReportService
    {

        private CarsDbContext data;

        public ReportService(CarsDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> AddReportAsync(int postId, string userId)
        {
            var post = await this.data.Posts.Include(x => x.Reports).Where(x => x.Id == postId).FirstOrDefaultAsync();

            if (post == null || post.Reports.Any(x => x.User == userId))
            {
                return false;
            }

            post.Reports.Add(new CarsBg_System.Data.Models.Report() { User = userId });

            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<ShowReportViewModel> ShowAllReports()
        => this.data.Posts
                        .Where(x => x.Reports.Any())
                        .OrderByDescending(x => x.Reports.Count)
                        .Select(x => new ShowReportViewModel()
                        {
                            CarId = x.CarId,
                            PostId = x.Id,
                            Comment = x.Comment,
                            Count = x.Reports.Count()
                        })
                        .ToList();

        public async Task<bool> ClearReportsAsync(int postId)
        {
            var post = this.data.Posts.Include(x => x.Reports).Where(x => x.Id == postId).FirstOrDefault();

            if (post == null)
            {
                return false;
            }

            post.Reports.Clear();

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
