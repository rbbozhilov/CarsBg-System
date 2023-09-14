using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Post;
using CarsBg_System.Services.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class PostController : Controller
    {
        private IReportService reportService;
        private IPostService postService;

        public PostController(IReportService reportService, IPostService postService)
        {
            this.reportService = reportService;
            this.postService = postService;
        }

        public IActionResult ShowReport()
        {
            var reports = this.reportService.ShowAllReports();

            return View(reports);
        }


        public IActionResult DeletePost(int id)
        {
            bool isDeleted = this.postService.DeletePost(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowReport));
        }

        public IActionResult ClearReports(int id)
        {
           bool isClear =  this.reportService.ClearReports(id);

            if (!isClear)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowReport));
        }

    }
}
