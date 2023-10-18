using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data;
using CarsBg_System.Models.Car;
using CarsBg_System.Views.ViewModels.Cars;
using CarsBg_System.Views.ViewModels.Extras;
using CarsBg_System.Views.ViewModels.Home;
using CarsBg_System.Views.ViewModels.Post;
using Microsoft.EntityFrameworkCore;

namespace CarsBg_System.Services.Car
{
    public class CarService : ICarService
    {

        private CarsDbContext data;

        public CarService(CarsDbContext data)
        {
            this.data = data;
        }

        public async Task<int> AddCarAsync(AddCarFormModel carModel, string userId, IList<CarsBg_System.Data.Models.Extra> extras)
        {
            var car = new CarsBg_System.Data.Models.Car()
            {
                Name = carModel.Name,
                Color = carModel.Color,
                CategoryId = carModel.CategoryId,
                Date = carModel.Year,
                Description = carModel.Description,
                TransmissionId = carModel.TransmissionId,
                WheelDriveId = carModel.WheelDriveId,
                EngineId = carModel.EngineId,
                EnginePower = carModel.EnginePower,
                HorsePower = carModel.HorsePower,
                ModelId = carModel.ModelId,
                PhoneNumber = carModel.PhoneNumber,
                RegionId = carModel.RegionId,
                UserId = userId,
                Mileage = carModel.Mileage
            };

            var price = new CarsBg_System.Data.Models.Price()
            {
                Money = carModel.Price,
                Date = DateTime.UtcNow,
                CarId = car.Id
            };

            car.Prices.Add(price);

            foreach (var item in extras)
            {
                car.Extras.Add(item);
            }

            await this.data.Cars.AddAsync(car);
            await this.data.SaveChangesAsync();

            return car.Id;
        }

        public bool CheckCarOfUser(string userId, int carId)
        => this.data.Cars.Where(x => x.Id == carId).Any(u => u.UserId == userId);


        public bool IsHaveCar(int carId)
        => this.data.Cars.Any(x => x.Id == carId);


        public async Task<bool> ChangeStatusAsync(int statusId, int carId)
        {
            var status = this.data.Statuses.Any(x => x.Id == statusId);


            if (!status)
            {
                return false;
            }

            var car = await this.GetCarByIdAsync(carId);

            car.StatusId = statusId;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditCarAsync(
                            int id,
                            string name,
                            string description,
                            string color,
                            int mileage,
                            int enginePower,
                            int horsePower,
                            int phoneNumber,
                            decimal price,
                            DateTime year,
                            int regionId,
                            int transmissionId,
                            int wheelDriveId,
                            int engineId,
                            int categoryId)
        {

            var currentCar = await this.data.Cars
                                         .Include(x => x.Prices)
                                         .Where(c => c.Id == id && c.IsDeleted == false)
                                         .FirstOrDefaultAsync();

            if (currentCar == null)
            {
                return false;
            }

            currentCar.Name = name;
            currentCar.Description = description;
            currentCar.Color = color;
            currentCar.Mileage = mileage;
            currentCar.EnginePower = enginePower;
            currentCar.HorsePower = horsePower;
            currentCar.PhoneNumber = phoneNumber;
            currentCar.TransmissionId = transmissionId;
            currentCar.EngineId = engineId;
            currentCar.CategoryId = categoryId;
            currentCar.WheelDriveId = wheelDriveId;
            currentCar.Date = year;
            currentCar.RegionId = regionId;

            var currentPrice = currentCar.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money;

            if (price != currentPrice)
            {
                var newPrice = new CarsBg_System.Data.Models.Price()
                {
                    Date = DateTime.UtcNow,
                    Money = price,
                    CarId = id
                };
                currentCar.Prices.Add(newPrice);
            }


            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int carId)
        {
            var car = await this.data.Cars.Where(x => x.Id == carId).FirstOrDefaultAsync();

            if (car == null || car.IsDeleted == true)
            {
                return false;
            }

            car.IsDeleted = true;
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<CarsBg_System.Data.Models.Car> GetCarByIdAsync(int id)
        => await this.data.Cars.Include(x => x.Prices).FirstOrDefaultAsync(x => x.Id == id);


        public async Task<HomeViewModel> GetVipAndTopCarsAsync()
        => new HomeViewModel()
        {
            TopCarsImages = await this.GetTopCarsAsync(),
            VipCarsImages = await this.GetVipCarsAsync()
        };


        public async Task<IEnumerable<ShowCarViewModel>> ShowCarsForAdminAsync()
        => await this.data.Cars
                     .Where(x => x.IsDeleted == false)
                     .Select(x => new ShowCarViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Price = x.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money
                     })
                    .ToListAsync();


        public IEnumerable<BrandViewModel> GetAllBrands()
        => data.Brands.Select(x => new BrandViewModel() { Id = x.Id, Name = x.Name });

        public async Task<IEnumerable<CarViewModel>> GetAllCarsBySearchAsync(IQueryable<Data.Models.Car> query)
        => await query.Where(x => x.IsDeleted == false)
                .Select(x => new CarViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    EngineType = x.Engine.Name,
                    Price = x.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money,
                    Status = x.Status.StatusName,
                    HorsePower = x.HorsePower,
                    ImageId = x.Images.FirstOrDefault().Id.ToString()
                })
                .OrderByDescending(x => x.Status)
                .ThenBy(x => x.Price)
                .ToListAsync();

        public async Task<CarDetailViewModel> ShowCarFullInformationAsync(int carId)
        => await this.data.Cars
                      .Where(x => x.Id == carId && x.IsDeleted == false)
                      .Select(x => new CarDetailViewModel()
                      {
                          Price = x.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money,
                          Date = x.Date,
                          Description = x.Description,
                          EngineType = x.Engine.Name,
                          Name = x.Name,
                          Status = x.Status.StatusName,
                          EnginePower = x.EnginePower,
                          HorsePower = x.HorsePower,
                          Id = x.Id,
                          ImagesId = x.Images.Select(i => i.Id.ToString()).ToList(),
                          Category = x.Category.Name,
                          Color = x.Color,
                          Mileage = x.Mileage,
                          Extras = x.Extras.Select(x => x.Name).ToList(),
                          Model = x.Model.Name,
                          PhoneNumber = x.PhoneNumber,
                          Region = x.Region.Name,
                          Transmission = x.Transmission.Name,
                          WheelDrive = x.WheelDrive.Name,
                          Comments = x.Posts
                                        .Select(p => new PostViewModel()
                                        {
                                            Id = p.Id,
                                            Comment = p.Comment,
                                            User = p.User
                                        })
                                        .ToList()
                      })
                      .FirstOrDefaultAsync();


        public async Task<IEnumerable<ModelViewModel>> GetAllModelsByBrandAsync(int brandId)
        => await this.data.Models
                            .Where(x => x.BrandId == brandId)
                            .Select(x => new ModelViewModel() { Id = x.Id, ModelName = x.Name })
                            .ToListAsync();


        public IQueryable<Data.Models.Car> GetCarsByBrand(int brandId)
        => this.data.Cars.Where(x => x.Model.BrandId == brandId);

        public IQueryable<Data.Models.Car> GetCarsByCategory(int categoryId, IQueryable<CarsBg_System.Data.Models.Car> query)
        => query.Where(x => x.CategoryId == categoryId);

        public IQueryable<Data.Models.Car> GetCarsByColor(string color, IQueryable<Data.Models.Car> query)
        => query.Where(x => x.Color.ToLower() == color.ToLower());

        public IQueryable<Data.Models.Car> GetCarsByEngineType(int engineId, IQueryable<Data.Models.Car> query)
        => query.Where(x => x.EngineId == engineId);

        public IQueryable<Data.Models.Car> GetCarsByModel(int modelId, IQueryable<CarsBg_System.Data.Models.Car> query)
        => query.Where(x => x.ModelId == modelId);

        public IQueryable<Data.Models.Car> GetCarsByPrice(decimal from, decimal to, IQueryable<Data.Models.Car> query)
        => query.Where(x => x.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money >= from && x.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money <= to);

        public IQueryable<Data.Models.Car> GetCarsByRegion(int regionId, IQueryable<Data.Models.Car> query)
        => query.Where(x => x.RegionId == regionId);

        public IQueryable<Data.Models.Car> GetCarsByTransmission(int transmissionId, IQueryable<Data.Models.Car> query)
        => query.Where(x => x.TransmissionId == transmissionId);

        public IQueryable<Data.Models.Car> GetCarsByWheelDrive(int wheelDriveId, IQueryable<Data.Models.Car> query)
        => query.Where(x => x.WheelDriveId == wheelDriveId);

        public IQueryable<Data.Models.Car> GetCarsByYear(int from, int to, IQueryable<Data.Models.Car> query)
        => query.Where(x => x.Date.Year >= from && x.Date.Year <= to);

        public IList<CarsBg_System.Data.Models.Extra> GetChoicedExtras(IList<ExtrasViewModel> extras)
        {

            List<CarsBg_System.Data.Models.Extra> currentExtras = new List<CarsBg_System.Data.Models.Extra>();

            foreach (var extra in extras)
            {

                var getExtra = this.data.Extras.Where(x => x.Id == extra.Id).FirstOrDefault();

                if (getExtra != null)
                {
                    currentExtras.Add(getExtra);
                }
            }

            return currentExtras;
        }

        public IEnumerable<ExtrasViewModel> GetExtras()
        => this.data.Extras.Select(x => new ExtrasViewModel()
        {
            ExtraName = x.Name,
            Id = x.Id,
            IsChecked = false
        }).ToList();

        public async Task<IEnumerable<MyCarsViewModel>> GetMyCarsAsync(string userId)
        => await this.data.Cars
            .Where(x => x.UserId == userId && x.IsDeleted == false)
            .Select(x => new MyCarsViewModel()
            {
                Id = x.Id,
                HorsePower = x.HorsePower,
                Name = x.Name
            })
            .ToListAsync();


        private async Task<List<TopCarsViewModel>> GetTopCarsAsync()
        => await this.data.Cars
                            .Where(x => x.IsDeleted == false && x.Status.StatusName == "Top")
                            .OrderByDescending(x => x.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money)
                            .Select(x => new TopCarsViewModel()
                            {
                                CarId = x.Id,
                                ImageId = x.Images.FirstOrDefault().Id.ToString()
                            })
                            .Take(5)
                            .ToListAsync();




        private async Task<List<VipCarsViewModel>> GetVipCarsAsync()
        => await this.data.Cars
                            .Where(x => x.IsDeleted == false && x.Status.StatusName == "Vip")
                            .OrderByDescending(x => x.Prices.OrderByDescending(x => x.Date).FirstOrDefault().Money)
                            .Select(x => new VipCarsViewModel()
                            {
                                CarId = x.Id,
                                ImageId = x.Images.FirstOrDefault().Id.ToString()
                            })
                            .Take(5)
                            .ToListAsync();

    }
}
