using CarsBg_System.Infrastructure;
using CarsBg_System.Models.Car;
using CarsBg_System.Models.Image;
using CarsBg_System.Models.Post;
using CarsBg_System.Services.Brand;
using CarsBg_System.Services.Car;
using CarsBg_System.Services.Category;
using CarsBg_System.Services.Engine;
using CarsBg_System.Services.ImageData;
using CarsBg_System.Services.Model;
using CarsBg_System.Services.Post;
using CarsBg_System.Services.Region;
using CarsBg_System.Services.Report;
using CarsBg_System.Services.Transmission;
using CarsBg_System.Services.WheelDrive;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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
        private IPostService postService;
        private IReportService reportService;


        public CarController(
                              ICarService carService,
                              IEngineService engineService,
                              ICategoryService categoryService,
                              IRegionService regionService,
                              IWheelDriveService wheelDriveService,
                              ITransmissionService transmissionService,
                              IBrandService brandService,
                              IModelService modelService,
                              IImageService imageService,
                              IPostService postService,
                              IReportService reportService)
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
            this.postService = postService;
            this.reportService = reportService;
        }


        public async Task<IActionResult> Index([FromQuery] CarFormModel query)
        {

            var models = await this.carService.GetAllModelsByBrandAsync(query.BrandId > 0 ? query.BrandId : 1);

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

        public async Task<IActionResult> Search([FromQuery] CarFormModel query)
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

            var cars = await this.carService.GetAllCarsBySearchAsync(carQuery);

            return View("CarsBySearch", cars);
        }


        [Authorize]
        public async Task<IActionResult> Add([FromQuery] AddCarFormModel query)
        {
            var models = await this.carService.GetAllModelsByBrandAsync(query.BrandId > 0 ? query.BrandId : 1);

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
            var models = await this.carService.GetAllModelsByBrandAsync(query.BrandId > 0 ? query.BrandId : 1);

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

            var carId = await this.carService.AddCarAsync(query, userId, extras);

            await this.imageService.Process(images.Select(i => new ImageInputModel()
            {
                Name = i.Name,
                Type = i.ContentType,
                Content = i.OpenReadStream()
            }), carId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> MyCars()
        {
            var userId = ClaimsPrincipalExtenssions.GetId(this.User);

            var myCars = await this.carService.GetMyCarsAsync(userId);

            return View(myCars);
        }

        [Authorize]
        public async Task<IActionResult> EditCar(int id)
        {

            var currentCar = await this.carService.GetCarByIdAsync(id);

            if (!this.CheckUserCar(id))
            {
                return BadRequest();
            }

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
        public async Task<IActionResult> EditCar(EditCarFormModel carModel, int id)
        {

            if (!this.CheckUserCar(id))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            var isEditted = await this.carService.EditCarAsync(
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

        [Authorize]
        public async Task<IActionResult> DeleteCar(int id)
        {

            if (!this.CheckUserCar(id))
            {
                return BadRequest();
            }

            bool isDeleted = await this.carService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(MyCars));
        }

        public async Task<IActionResult> ViewCar(int id)
        {

            var currentCar = await this.carService.ShowCarFullInformationAsync(id);

            if (currentCar == null)
            {
                return BadRequest();
            }


            return View(currentCar);
        }

        [Authorize]
        public IActionResult AddPostToCar()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPostToCar(PostFormModel post, int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userEmail = this.User.Identity.Name;
            var getIndex = userEmail.IndexOf('@');
            var userName = userEmail.Substring(0, getIndex);

            var addPost = await this.postService.AddPostAsync(id, post.Comment, userName);

            if (!addPost)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(ViewCar), new { Id = id });
        }

        [Authorize]
        public async Task<IActionResult> AddReport(int id, int carId)
        {

            var isReported = await this.reportService.AddReportAsync(id, ClaimsPrincipalExtenssions.GetId(this.User));

            if (!isReported)
            {
                return RedirectToAction(nameof(AlreadyReported));
            }

            return RedirectToAction(nameof(ViewCar), new { Id = carId });
        }

        public IActionResult AlreadyReported()
        {
            return View();
        }

        public async Task<IActionResult> SmallImages(string id)
        {
            return this.ReturnImage(await this.imageService.GetSmallImage(id));
        }

        public async Task<IActionResult> OriginalImages(string id)
        {
            return this.ReturnImage(await this.imageService.GetOriginalImage(id));
        }

        public async Task<IActionResult> CourseImages(string id)
        {
            return this.ReturnImage(await this.imageService.GetCourseImages(id));
        }


        private bool CheckUserCar(int id)
        {
            var userId = ClaimsPrincipalExtenssions.GetId(this.User);

            bool isUserCar = this.carService.CheckCarOfUser(userId, id);

            if (!isUserCar)
            {
                return false;
            }

            return true;
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
