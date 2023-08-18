using CarsBg_System.Views.ViewModels.Cars;

namespace CarsBg_System.Services.Car
{
    public interface ICarService
    {

        IEnumerable<BrandViewModel> GetAllBrands();

        IEnumerable<ModelViewModel> GetAllModelsByBrand(int brandId);


    }
}
