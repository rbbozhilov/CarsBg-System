using CarsBg_System.Models.Car;
using CarsBg_System.Services.Car;
using CarsBg_System.Services.Category;
using CarsBg_System.Services.Engine;
using CarsBg_System.Services.Region;
using CarsBg_System.Services.Transmission;
using CarsBg_System.Services.WheelDrive;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Controllers
{
    public class CarController : Controller
    {

        private ICarService carService;
        private IEngineService engineService;
        private ITransmissionService transmissionService;
        private IWheelDriveService wheelDriveService;
        private IRegionService regionService;
        private ICategoryService categoryService;

        public CarController(
                              ICarService carService,
                              IEngineService engineService,
                              ICategoryService categoryService,
                              IRegionService regionService,
                              IWheelDriveService wheelDriveService,
                              ITransmissionService transmissionService)
        {
            this.carService = carService;
            this.engineService = engineService;
            this.categoryService = categoryService;
            this.regionService = regionService;
            this.wheelDriveService = wheelDriveService;
            this.transmissionService = transmissionService;
        }

        public IActionResult Index([FromQuery] CarFormModel query)
        {

            var models = this.carService.GetAllModelsByBrand(query.BrandId > 0 ? query.BrandId : 1);


            return View(new CarFormModel()
            {
                Brands = this.carService.GetAllBrands(),
                Models = models,
                Engines = this.engineService.GetAllEngines(),
                WheelDrives = this.wheelDriveService.GetAllWheelDrives(),
                Transmissions = this.transmissionService.GetAllTransmissions(),
                Categories = this.categoryService.GetAllCategories(),
                Regions = this.regionService.GetAllRegions(),
            });
        }

        public IActionResult Search([FromQuery] CarFormModel query)
        {


            return View();
        }


    }
}
