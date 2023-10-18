using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Brand;
using CarsBg_System.Services.Car;
using CarsBg_System.Services.Engine;
using CarsBg_System.Services.Extra;
using CarsBg_System.Services.Model;
using CarsBg_System.Services.Status;
using CarsBg_System.Services.Transmission;
using CarsBg_System.Services.WheelDrive;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class CarController : Controller
    {

        private ICarService carService;
        private IEngineService engineService;
        private ITransmissionService transmissionService;
        private IModelService modelService;
        private IWheelDriveService wheelDriveService;
        private IExtraService extraService;
        private IBrandService brandService;
        private IStatusService statusService;

        public CarController(ICarService carService,
                             IEngineService engineService,
                             ITransmissionService transmissionService,
                             IModelService modelService,
                             IWheelDriveService wheelDriveService,
                             IExtraService extraService,
                             IBrandService brandService,
                             IStatusService statusService)
        {
            this.carService = carService;
            this.engineService = engineService;
            this.transmissionService = transmissionService;
            this.modelService = modelService;
            this.wheelDriveService = wheelDriveService;
            this.extraService = extraService;
            this.brandService = brandService;
            this.statusService = statusService;
        }

        public async Task<IActionResult> ShowCar()
        {
            var cars = await this.carService.ShowCarsForAdminAsync();

            return View(cars);

        }

        public IActionResult EditCar()
        {
            return View(new CarFormModel()
            {
                Statuses = this.statusService.ShowStatuses()
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditCar(CarFormModel carModel, int id)
        {

            bool isChanged = await this.carService.ChangeStatusAsync(carModel.StatusId, id);

            if (!isChanged)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowCar));
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
            bool isDeleted = await this.carService.DeleteAsync(id);

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
        public async Task<IActionResult> AddEngine(EngineFormModel engineModel)
        {
            if (!ModelState.IsValid)
            {
                return View(engineModel);
            }

            bool isDone = await this.engineService.AddAsync(engineModel);

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


        public async Task<IActionResult> DeleteEngine(int id)
        {
            bool isDeleted = await this.engineService.DeleteAsync(id);

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
        public async Task<IActionResult> AddModel(ModelFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            bool isDone = await this.modelService.AddAsync(formModel);

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


        public async Task<IActionResult> DeleteModel(int id)
        {
            bool isDeleted = await this.modelService.DeleteAsync(id);

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
        public async Task<IActionResult> AddWheelDrive(WheelDriveFormModel wheelDriveModel)
        {
            if (!ModelState.IsValid)
            {
                return View(wheelDriveModel);
            }

            bool isDone = await this.wheelDriveService.AddAsync(wheelDriveModel);

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


        public async Task<IActionResult> DeleteWheelDrive(int id)
        {
            bool isDeleted = await this.wheelDriveService.DeleteAsync(id);

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
        public async Task<IActionResult> AddTransmission(TransmissionFormModel transmissionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(transmissionModel);
            }

            bool isDone = await this.transmissionService.AddAsync(transmissionModel);

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


        public async Task<IActionResult> DeleteTransmission(int id)
        {
            bool isDeleted = await this.transmissionService.DeleteAsync(id);

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
        public async Task<IActionResult> AddExtra(ExtraFormModel extraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(extraModel);
            }

            bool isDone = await this.extraService.AddAsync(extraModel);

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


        public async Task<IActionResult> DeleteExtra(int id)
        {
            bool isDeleted = await this.extraService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowExtra));
        }

    }
}
