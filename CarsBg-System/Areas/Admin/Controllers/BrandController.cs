using CarsBg_System.Areas.Admin.Models.Brand;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class BrandController : Controller
    {

        private IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandFormModel brandModel)
        {
            if (!ModelState.IsValid)
            {
                return View(brandModel);
            }

            bool isDone = await this.brandService.AddAsync(brandModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ShowBrand));
        }

        public IActionResult ShowBrand(IEnumerable<ShowBrandViewModel> brandModel)
        {
            var brands = this.brandService.ShowBrands();

            return View(brands);
        }


        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.brandService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowBrand));
        }


    }
}
