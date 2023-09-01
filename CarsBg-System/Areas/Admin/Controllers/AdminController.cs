using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
