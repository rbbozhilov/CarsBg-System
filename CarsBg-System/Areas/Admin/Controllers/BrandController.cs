using CarsBg_System.Areas.Admin.Models.Brand;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {

        public BrandController()
        {

        }


        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBrand(BrandFormModel brand)
        {
            return View();
        }


    }
}
