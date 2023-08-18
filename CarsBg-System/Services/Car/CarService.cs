using CarsBg_System.Data;
using CarsBg_System.Views.ViewModels.Cars;

namespace CarsBg_System.Services.Car
{
    public class CarService : ICarService
    {

        private CarsDbContext data;

        public CarService(CarsDbContext data)
        {
            this.data = data;
        }


        public IEnumerable<BrandViewModel> GetAllBrands()
        => data.Brands.Select(x => new BrandViewModel() { Id = x.Id, Name = x.Name });

        public IEnumerable<ModelViewModel> GetAllModelsByBrand(int brandId)
        {

            var getAllModelsByBrand = this.data.Models
                                                .Where(x => x.BrandId == brandId)
                                                .Select(x => new ModelViewModel() { Id = x.Id, ModelName = x.Name })
                                                .ToList();

            return getAllModelsByBrand;
        }
    }
}
