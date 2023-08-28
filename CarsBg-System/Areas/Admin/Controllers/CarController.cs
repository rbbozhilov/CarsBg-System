using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Brand;
using CarsBg_System.Services.Car;
using CarsBg_System.Services.Engine;
using CarsBg_System.Services.Extra;
using CarsBg_System.Services.Model;
using CarsBg_System.Services.Transmission;
using CarsBg_System.Services.WheelDrive;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {

        private ICarService carService;
        private IEngineService engineService;
        private ITransmissionService transmissionService;
        private IModelService modelService;
        private IWheelDriveService wheelDriveService;
        private IExtraService extraService;
        private IBrandService brandService;

        public CarController(ICarService carService,
                             IEngineService engineService,
                             ITransmissionService transmissionService,
                             IModelService modelService,
                             IWheelDriveService wheelDriveService,
                             IExtraService extraService,
                             IBrandService brandService)
        {
            this.carService = carService;
            this.engineService = engineService;
            this.transmissionService = transmissionService;
            this.modelService = modelService;
            this.wheelDriveService = wheelDriveService;
            this.extraService = extraService;
            this.brandService = brandService;
        }

        public IActionResult ShowCar(IEnumerable<ShowCarViewModel> carModel)
        {
            var cars = this.carService.ShowCarsForAdmin();

            return View(cars);
        }

        public IActionResult DeleteCar(int id)
        {
            bool isDeleted = this.carService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowCar));
        }


        public IActionResult AddEngine()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEngine(EngineFormModel engineModel)
        {
            if (!ModelState.IsValid)
            {
                return View(engineModel);
            }

            bool isDone = this.engineService.Add(engineModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ShowEngine));
        }

        public IActionResult ShowEngine(IEnumerable<ShowEngineViewModel> engineModel)
        {
            var engines = this.engineService.ShowEngines();

            return View(engines);
        }


        public IActionResult DeleteEngine(int id)
        {
            bool isDeleted = this.engineService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowEngine));
        }


        public IActionResult AddModel()
        {
            return View(new ModelFormModel()
            {
                Brands = this.brandService.ShowBrands()
            });
        }

        [HttpPost]
        public IActionResult AddModel(ModelFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            bool isDone = this.modelService.Add(formModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ShowModel));
        }

        public IActionResult ShowModel(IEnumerable<ShowModelViewModel> showModel)
        {
            var models = this.modelService.ShowModels();

            return View(models);
        }


        public IActionResult DeleteModel(int id)
        {
            bool isDeleted = this.modelService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowModel));
        }

        public IActionResult AddWheelDrive()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWheelDrive(WheelDriveFormModel wheelDriveModel)
        {
            if (!ModelState.IsValid)
            {
                return View(wheelDriveModel);
            }

            bool isDone = this.wheelDriveService.Add(wheelDriveModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ShowWheelDrive));
        }

        public IActionResult ShowWheelDrive(IEnumerable<ShowWheelDriveViewModel> wheelDriveModel)
        {
            var wheelDrives = this.wheelDriveService.ShowWheelDrives();

            return View(wheelDrives);
        }


        public IActionResult DeleteWheelDrive(int id)
        {
            bool isDeleted = this.wheelDriveService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowWheelDrive));
        }

        public IActionResult AddTransmission()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTransmission(TransmissionFormModel transmissionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(transmissionModel);
            }

            bool isDone = this.transmissionService.Add(transmissionModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ShowTransmission));
        }

        public IActionResult ShowTransmission(IEnumerable<ShowTransmissionViewModel> transmissionModel)
        {
            var transmissions = this.transmissionService.ShowTransmissions();

            return View(transmissions);
        }


        public IActionResult DeleteTransmission(int id)
        {
            bool isDeleted = this.transmissionService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowTransmission));
        }

        public IActionResult AddExtra()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddExtra(ExtraFormModel extraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(extraModel);
            }

            bool isDone = this.extraService.Add(extraModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ShowExtra));
        }

        public IActionResult ShowExtra(IEnumerable<ShowExtraViewModel> extraModel)
        {
            var extras = this.extraService.ShowExtras();

            return View(extras);
        }


        public IActionResult DeleteExtra(int id)
        {
            bool isDeleted = this.extraService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowExtra));
        }

    }
}
