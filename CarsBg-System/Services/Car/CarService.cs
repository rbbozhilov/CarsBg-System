using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data;
using CarsBg_System.Data.Models;
using CarsBg_System.Models.Car;
using CarsBg_System.Views.ViewModels.Cars;
using CarsBg_System.Views.ViewModels.Extras;

namespace CarsBg_System.Services.Car
{
    public class CarService : ICarService
    {

        private CarsDbContext data;

        public CarService(CarsDbContext data)
        {
            this.data = data;
        }

        public void AddCar(AddCarFormModel carModel, string userId, IList<CarsBg_System.Data.Models.Extra> extras)
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
                Price = carModel.Price,
                UserId = userId
            };

            foreach (var item in extras)
            {
                car.Extras.Add(item);
            }

            this.data.Cars.Add(car);
            this.data.SaveChanges();

        }

        public bool IsHaveCar(int carId)
        => this.data.Cars.Any(x => x.Id == carId);


        public bool ChangeStatus(int statusId, int carId)
        {
            var status = this.data.Statuses.Any(x => x.Id == statusId);
            

            if(!status)
            {
                return false;
            }

            var car = this.GetCarById(carId);

            car.StatusId = statusId;

            this.data.SaveChanges();

            return true;
        }


        public bool Delete(int carId)
        {
            var car = this.data.Cars.Where(x => x.Id == carId).FirstOrDefault();

            if (car == null || car.IsDeleted == true)
            {
                return false;
            }

            car.IsDeleted = true;
            this.data.SaveChanges();

            return true;
        }

        public CarsBg_System.Data.Models.Car GetCarById(int id)
        => this.data.Cars.FirstOrDefault(x => x.Id == id);


        public IEnumerable<ShowCarViewModel> ShowCarsForAdmin()
        => this.data.Cars
                     .Where(x => x.IsDeleted == false)
                     .Select(x => new ShowCarViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Price = x.Price
                     })
                    .ToList();


        public IEnumerable<BrandViewModel> GetAllBrands()
        => data.Brands.Select(x => new BrandViewModel() { Id = x.Id, Name = x.Name });

        public IEnumerable<CarViewModel> GetAllCarsBySearch(IQueryable<Data.Models.Car> query)
        => query.Where(x => x.IsDeleted == false)
                .Select(x => new CarViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    EngineType = x.Engine.Name,
                    Price = x.Price,
                    Status = x.Status.StatusName
                })
                .ToList();


        public IEnumerable<ModelViewModel> GetAllModelsByBrand(int brandId)
        {

            var getAllModelsByBrand = this.data.Models
                                                .Where(x => x.BrandId == brandId)
                                                .Select(x => new ModelViewModel() { Id = x.Id, ModelName = x.Name })
                                                .ToList();

            return getAllModelsByBrand;
        }

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
        => query.Where(x => x.Price >= from && x.Price <= to);

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

        public IEnumerable<MyCarsViewModel> GetMyCars(string userId)
        => this.data.Cars
            .Where(x => x.UserId == userId)
            .Select(x => new MyCarsViewModel()
            {
                Id = x.Id,
                HorsePower = x.HorsePower,
                Name = x.Name
            })
            .ToList();

    }
}
