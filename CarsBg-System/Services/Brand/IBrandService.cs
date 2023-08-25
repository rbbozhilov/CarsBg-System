using CarsBg_System.Areas.Admin.Models.Brand;
using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Brand
{
    public interface IBrandService
    {

        bool Delete(int brandId);

        bool Add(BrandFormModel brand);

        bool IsHaveBrandById(int id);

        IEnumerable<ShowBrandViewModel> ShowBrands();

    }
}
