using CarsBg_System.Infrastructure;
using CarsBg_System.Models.Car;
using CarsBg_System.Models.Image;
using CarsBg_System.Services.Brand;
using CarsBg_System.Services.Car;
using CarsBg_System.Services.Category;
using CarsBg_System.Services.Engine;
using CarsBg_System.Services.ImageData;
using CarsBg_System.Services.Model;
using CarsBg_System.Services.Region;
using CarsBg_System.Services.Transmission;
using CarsBg_System.Services.WheelDrive;
using Microsoft.AspNetCore.Authorization;
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
        private IBrandService brandService;
        private IModelService modelService;
        private IImageService imageService;


        public CarController(
                              ICarService carService,
                              IEngineService engineService,
                              ICategoryService categoryService,
                              IRegionService regionService,
                              IWheelDriveService wheelDriveService,
                              ITransmissionService transmissionService,
                              IBrandService brandService,
                              IModelService modelService,
                              IImageService imageService)
        {
            this.carService = carService;
            this.engineService = engineService;
            this.categoryService = categoryService;
            this.regionService = regionService;
            this.wheelDriveService = wheelDriveService;
            this.transmissionService = transmissionService;
            this.brandService = brandService;
            this.modelService = modelService;
            this.imageService = imageService;
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
                Regions = this.regionService.GetAllRegions()
            });
        }

        public IActionResult Search([FromQuery] CarFormModel query)
        {

            if (!this.brandService.IsHaveBrandById(query.BrandId))
            {
                this.ModelState.AddModelError(nameof(query.BrandId), "Don't try stupid things!");
            }

            if (!this.modelService.IsHaveModelById(query.ModelId))
            {
                this.ModelState.AddModelError(nameof(query.ModelId), "Don't try stupid things!");
            }

            if (query.CategoryId != 0 && !this.categoryService.IsHaveCategoryById(query.CategoryId))
            {
                this.ModelState.AddModelError(nameof(query.CategoryId), "Don't try stupid things!");
            }

            if (query.EngineId != 0 && !this.engineService.IsHaveEngineById(query.EngineId))
            {
                this.ModelState.AddModelError(nameof(query.EngineId), "Don't try stupid things!");
            }

            if (query.TransmissionId != 0 && !this.transmissionService.IsHaveTransmissionById(query.TransmissionId))
            {
                this.ModelState.AddModelError(nameof(query.TransmissionId), "Don't try stupid things!");
            }

            if (query.WheelDriveId != 0 && !this.wheelDriveService.IsHaveWheelDriveById(query.WheelDriveId))
            {
                this.ModelState.AddModelError(nameof(query.WheelDriveId), "Don't try stupid things!");
            }

            if (query.RegionId != 0 && !this.regionService.IsHaveRegionById(query.RegionId))
            {
                this.ModelState.AddModelError(nameof(query.RegionId), "Don't try stupid things!");
            }


            bool fromPriceIsValide = Decimal.TryParse(query.FromPrice, out decimal fromPrice);
            bool toPriceIsValide = Decimal.TryParse(query.ToPrice, out decimal toPrice);
            bool fromYearIsValide = Int32.TryParse(query.FromYear.ToString(), out int fromYear);
            bool toYearIsValide = Int32.TryParse(query.ToYear.ToString(), out int toYear);

            if (!String.IsNullOrEmpty(query.FromPrice) ||
                !String.IsNullOrWhiteSpace(query.FromPrice) ||
                !String.IsNullOrEmpty(query.ToPrice) ||
                !String.IsNullOrWhiteSpace(query.ToPrice)
                )
            {
                if (fromPriceIsValide && toPriceIsValide)
                {

                    if (fromPrice < 0 || toPrice < 0)
                    {
                        this.ModelState.AddModelError(nameof(query.FromPrice), $"Cannot be less than 0 From Price or To Price");
                    }

                    if (fromPrice > toPrice)
                    {
                        this.ModelState.AddModelError(nameof(query.FromPrice), $"Cannot be more than {query.ToPrice}");
                    }
                }
                else
                {
                    this.ModelState.AddModelError(nameof(query.FromPrice), $"Incorrect input for {nameof(query.FromPrice)} or {nameof(query.ToPrice)}");

                }
            }

            if (fromYearIsValide && toYearIsValide)
            {
                if (fromYear > toYear)
                {
                    this.ModelState.AddModelError(nameof(query.FromYear), $"Cannot be more than {query.ToYear}");
                }
            }
            else
            {
                this.ModelState.AddModelError(nameof(query.FromYear), $"Incorrect input for {nameof(query.FromYear)} or {nameof(query.ToYear)}");

            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var carQuery = this.carService.GetCarsByBrand(query.BrandId);
            carQuery = this.carService.GetCarsByModel(query.ModelId, carQuery);
            carQuery = this.carService.GetCarsByYear(fromYear, toYear, carQuery);

            if (fromPriceIsValide && toPriceIsValide)
            {
                carQuery = this.carService.GetCarsByPrice(fromPrice, toPrice, carQuery);
            }



            if (query.CategoryId != 0)
            {
                carQuery = this.carService.GetCarsByCategory(query.CategoryId, carQuery);
            }
            if (query.EngineId != 0)
            {
                carQuery = this.carService.GetCarsByEngineType(query.EngineId, carQuery);
            }
            if (!String.IsNullOrEmpty(query.Color) || !String.IsNullOrWhiteSpace(query.Color))
            {
                carQuery = this.carService.GetCarsByColor(query.Color, carQuery);
            }
            if (query.TransmissionId != 0)
            {
                carQuery = this.carService.GetCarsByTransmission(query.TransmissionId, carQuery);
            }
            if (query.WheelDriveId != 0)
            {
                carQuery = this.carService.GetCarsByWheelDrive(query.WheelDriveId, carQuery);
            }
            if (query.RegionId != 0)
            {
                carQuery = this.carService.GetCarsByRegion(query.RegionId, carQuery);
            }

            var cars = this.carService.GetAllCarsBySearch(carQuery);

            return View("CarsBySearch", cars);
        }


        [Authorize]
        public IActionResult Add([FromQuery] AddCarFormModel query)
        {
            var models = this.carService.GetAllModelsByBrand(query.BrandId > 0 ? query.BrandId : 1);

            return View(new AddCarFormModel()
            {
                Brands = this.carService.GetAllBrands(),
                Models = models,
                Engines = this.engineService.GetAllEngines(),
                WheelDrives = this.wheelDriveService.GetAllWheelDrives(),
                Transmissions = this.transmissionService.GetAllTransmissions(),
                Categories = this.categoryService.GetAllCategories(),
                Regions = this.regionService.GetAllRegions(),
                Extras = this.carService.GetExtras().ToList()
            });
        }

        [HttpPost]
        [Authorize]
        [RequestSizeLimit(300 * 1024 * 1024)]
        public async Task<IActionResult> Add(AddCarFormModel query, IFormFile[] images)
        {
            var models = this.carService.GetAllModelsByBrand(query.BrandId > 0 ? query.BrandId : 1);

            if (images.Length > 20)
            {
                this.ModelState.AddModelError("images", "You cannot upload more than 20 images");
            }

            if (images.Any(x => x.Length > 15 * 1024 * 1024))
            {
                this.ModelState.AddModelError("images", "Image cannot be larger than 15 MB");
            }


            if (!ModelState.IsValid)
            {
                return View(new AddCarFormModel()
                {
                    Brands = this.carService.GetAllBrands(),
                    Models = models,
                    Engines = this.engineService.GetAllEngines(),
                    WheelDrives = this.wheelDriveService.GetAllWheelDrives(),
                    Transmissions = this.transmissionService.GetAllTransmissions(),
                    Categories = this.categoryService.GetAllCategories(),
                    Regions = this.regionService.GetAllRegions(),
                    Extras = this.carService.GetExtras().ToList()
                });
            }

            var userId = ClaimsPrincipalExtenssions.GetId(this.User);

            var selectedExtras = query.Extras.Where(x => x.IsChecked).ToList();

            var extras = this.carService.GetChoicedExtras(selectedExtras);

            var carId = this.carService.AddCar(query, userId, extras);

            await this.imageService.Process(images.Select(i => new ImageInputModel()
            {
                Name = i.Name,
                Type = i.ContentType,
                Content = i.OpenReadStream()
            }), carId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult MyCars()
        {
            var userId = ClaimsPrincipalExtenssions.GetId(this.User);

            var myCars = this.carService.GetMyCars(userId);

            return View(myCars);
        }

        [Authorize]
        public IActionResult EditCar(int id)
        {

            var currentCar = this.carService.GetCarById(id);

            return View(new EditCarFormModel()
            {
                Color = currentCar.Color,
                Name = currentCar.Name,
                Mileage = currentCar.Mileage,
                Description = currentCar.Description,
                EnginePower = currentCar.EnginePower,
                HorsePower = currentCar.HorsePower,
                PhoneNumber = currentCar.PhoneNumber,
                Price = currentCar.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money,
                Year = currentCar.Date,
                EngineId = currentCar.EngineId,
                Engines = this.engineService.GetAllEngines(),
                WheelDriveId = currentCar.WheelDriveId,
                WheelDrives = this.wheelDriveService.GetAllWheelDrives(),
                TransmissionId = currentCar.TransmissionId,
                Transmissions = this.transmissionService.GetAllTransmissions(),
                CategoryId = currentCar.CategoryId,
                Categories = this.categoryService.GetAllCategories(),
                RegionId = currentCar.RegionId,
                Regions = this.regionService.GetAllRegions(),
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditCar(EditCarFormModel carModel, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            var isEditted = this.carService.EditCar(
                                      id,
                                      carModel.Name,
                                      carModel.Description,
                                      carModel.Color,
                                      carModel.Mileage,
                                      carModel.EnginePower,
                                      carModel.HorsePower,
                                      carModel.PhoneNumber,
                                      carModel.Price,
                                      carModel.Year,
                                      carModel.RegionId,
                                      carModel.TransmissionId,
                                      carModel.WheelDriveId,
                                      carModel.EngineId,
                                      carModel.CategoryId);
            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(MyCars));
        }

        public IActionResult ViewCar(int id)
        {

            var currentCar = this.carService.ShowCarFullInformation(id);

            if (currentCar == null)
            {
                return BadRequest();
            }


            return View(currentCar);
        }


    }
}
