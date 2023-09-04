using CarsBg_System.Models;
using CarsBg_System.Services.ImageData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;

namespace CarsBg_System.Controllers
{
    public class HomeController : Controller
    {

        private IImageService imageService;

        public HomeController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {

            var something = await this.imageService.GetAllImages();

            return View(something);
        }

        public async Task<IActionResult> Small(string id)
        {
            return this.ReturnImage(await this.imageService.GetCourseImages(id));
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IActionResult ReturnImage(Stream image)
        {
            var headers = this.Response.GetTypedHeaders();

            headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(30)
            };

            headers.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(30));

            return this.File(image, "image/jpeg");
        }
    }
}