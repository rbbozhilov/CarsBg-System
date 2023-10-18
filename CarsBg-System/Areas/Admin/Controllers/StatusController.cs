using CarsBg_System.Areas.Admin.Models.Status;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Status;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
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
        public async Task<IActionResult> AddStatus(StatusFormModel statusModel)
        {
            if (!ModelState.IsValid)
            {
                return View(statusModel);
            }

            bool isDone = await this.statusService.AddAsync(statusModel);

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


        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.statusService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowStatus));
        }


    }
}
