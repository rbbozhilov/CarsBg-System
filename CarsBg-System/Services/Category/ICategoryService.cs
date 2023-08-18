using CarsBg_System.Views.ViewModels.Category;

namespace CarsBg_System.Services.Category
{
    public interface ICategoryService
    {

        IEnumerable<CategoryViewModel> GetAllCategories();


    }
}
