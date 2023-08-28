using CarsBg_System.Areas.Admin.Models.Status;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Status;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatusController : Controller
    {

        private IStatusService statusService;

        public StatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        public IActionResult AddStatus()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStatus(StatusFormModel statusModel)
        {
            if (!ModelState.IsValid)
            {
                return View(statusModel);
            }

            bool isDone = this.statusService.Add(statusModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ShowStatus));
        }

        public IActionResult ShowStatus(IEnumerable<ShowStatusViewModel> statusModel)
        {
            var statuses = this.statusService.ShowStatuses();

            return View(statuses);
        }


        public IActionResult Delete(int id)
        {
            bool isDeleted = this.statusService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowStatus));
        }


    }
}
