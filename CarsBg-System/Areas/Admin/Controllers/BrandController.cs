using CarsBg_System.Areas.Admin.Models.Brand;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Brand;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public IActionResult AddBrand(BrandFormModel brandModel)
        {
            if (!ModelState.IsValid)
            {
                return View(brandModel);
            }

            bool isDone = this.brandService.Add(brandModel);

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


        public IActionResult Delete(int id)
        {
            bool isDeleted = this.brandService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowBrand));
        }


    }
}
