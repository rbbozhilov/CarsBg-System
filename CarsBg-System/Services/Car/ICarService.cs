﻿using CarsBg_System.Data.Models;
using CarsBg_System.Models.Car;
using CarsBg_System.Views.ViewModels.Cars;
using CarsBg_System.Views.ViewModels.Extras;

namespace CarsBg_System.Services.Car
{
    public interface ICarService
    {


        void AddCar(AddCarFormModel carModel,string userId, IList<Extra> extras);

        IList<Extra> GetChoicedExtras(IList<ExtrasViewModel> extras);

        IEnumerable<ExtrasViewModel> GetExtras();

        IEnumerable<MyCarsViewModel> GetMyCars(string userId);

        IEnumerable<BrandViewModel> GetAllBrands();

        IEnumerable<ModelViewModel> GetAllModelsByBrand(int brandId);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByBrand(int brandId);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByModel(int modelId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByCategory(int categoryId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByEngineType(int engineId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByColor(string color, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByPrice(decimal from,decimal to, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByYear(int from,int to, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByTransmission(int transmissionId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByWheelDrive(int wheelDriveId, IQueryable<CarsBg_System.Data.Models.Car> query);

        IQueryable<CarsBg_System.Data.Models.Car> GetCarsByRegion(int regionId, IQueryable<CarsBg_System.Data.Models.Car> query);

    }
}
