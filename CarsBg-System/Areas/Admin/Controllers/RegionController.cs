using CarsBg_System.Areas.Admin.Models.Region;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Region;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class RegionController : Controller
    {

        private IRegionService regionService;

        public RegionController(IRegionService regionService)
        {
            this.regionService = regionService;
        }

        public IActionResult AddRegion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRegion(RegionFormModel regionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(regionModel);
            }

            bool isDone = this.regionService.Add(regionModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ShowRegion));
        }

        public IActionResult ShowRegion(IEnumerable<ShowRegionViewModel> regionModel)
        {
            var regions = this.regionService.ShowRegions();

            return View(regions);
        }


        public IActionResult Delete(int id)
        {
            bool isDeleted = this.regionService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowRegion));
        }


    }
}
