using CarsBg_System.Data;
using CarsBg_System.Views.ViewModels.Category;

namespace CarsBg_System.Services.Category
{
    public class CategoryService : ICategoryService
    {

        private CarsDbContext data;

        public CategoryService(CarsDbContext data)
        {
            this.data = data;
        }


        public IEnumerable<CategoryViewModel> GetAllCategories()
        => this.data.Categories.Select(x => new CategoryViewModel() { Id = x.Id, CategoryName = x.Name });
    }
}
