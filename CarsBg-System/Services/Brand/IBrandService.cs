using CarsBg_System.Areas.Admin.Models.Brand;
using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Brand
{
    public interface IBrandService
    {

        Task<bool> DeleteAsync(int brandId);

        Task<bool> AddAsync(BrandFormModel brand);

        bool IsHaveBrandById(int id);

        IEnumerable<ShowBrandViewModel> ShowBrands();

    }
}
