using CarsBg_System.Areas.Admin.Models.Category;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Views.ViewModels.Category;

namespace CarsBg_System.Services.Category
{
    public interface ICategoryService
    {

        Task<bool> AddAsync(CategoryFormModel category);

        Task<bool> DeleteAsync(int categoryId);

        bool IsHaveCategoryById(int id);

        IEnumerable<CategoryViewModel> GetAllCategories();

        IEnumerable<ShowCategoryViewModel> ShowCategories();

    }
}
