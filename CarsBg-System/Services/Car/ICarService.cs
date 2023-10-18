using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data.Models;
using CarsBg_System.Models.Car;
using CarsBg_System.Views.ViewModels.Cars;
using CarsBg_System.Views.ViewModels.Extras;
using CarsBg_System.Views.ViewModels.Home;

namespace CarsBg_System.Services.Car
{
    public interface ICarService
    {

        Task<bool> DeleteAsync(int carId);

        Task<bool> ChangeStatusAsync(int statusId, int carId);

        bool IsHaveCar(int carId);

        bool CheckCarOfUser(string userId, int carId);

        Task<bool> EditCarAsync(
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
                    int categoryId
                    );


        Task<CarsBg_System.Data.Models.Car> GetCarByIdAsync(int carId);

        Task<int> AddCarAsync(AddCarFormModel carModel, string userId, IList<CarsBg_System.Data.Models.Extra> extras);

        Task<CarDetailViewModel> ShowCarFullInformationAsync(int carId);

        Task<HomeViewModel> GetVipAndTopCarsAsync();

        IList<CarsBg_System.Data.Models.Extra> GetChoicedExtras(IList<ExtrasViewModel> extras);

        Task<IEnumerable<ShowCarViewModel>> ShowCarsForAdminAsync();

        Task<IEnumerable<CarViewModel>> GetAllCarsBySearchAsync(IQueryable<CarsBg_System.Data.Models.Car> query);

        IEnumerable<ExtrasViewModel> GetExtras();

        Task<IEnumerable<MyCarsViewModel>> GetMyCarsAsync(string userId);

        IEnumerable<BrandViewModel> GetAllBrands();

        Task<IEnumerable<ModelViewModel>> GetAllModelsByBrandAsync(int brandId);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByBrand(int brandId);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByModel(int modelId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByCategory(int categoryId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByEngineType(int engineId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByColor(string color, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByPrice(decimal from, decimal to, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByYear(int from, int to, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByTransmission(int transmissionId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByWheelDrive(int wheelDriveId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByRegion(int regionId, IQueryable<CarsBg_System.Data.Models.Car> query);

    }
}
