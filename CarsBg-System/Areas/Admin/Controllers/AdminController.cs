using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}
