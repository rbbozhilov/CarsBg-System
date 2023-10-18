using CarsBg_System.Areas.Admin.Models.Category;
using CarsBg_System.Areas.Admin.Views.ViewModels;
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

        public async Task<bool> AddAsync(CategoryFormModel category)
        {
            bool isHave = this.data.Categories.Any(x => x.Name == category.Name);

            if (isHave)
            {
                return false;
            }

            var currentCategory = new CarsBg_System.Data.Models.Category() { Name = category.Name };

            await this.data.Categories.AddAsync(currentCategory);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int categoryId)
        {
            var category = this.data.Categories.Where(x => x.Id == categoryId).FirstOrDefault();

            if (category == null || category.IsDeleted == true)
            {
                return false;
            }

            category.IsDeleted = true;
            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<ShowCategoryViewModel> ShowCategories()
        => this.data.Categories
                     .Where(x => x.IsDeleted == false)
                     .Select(x => new ShowCategoryViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name
                     })
                    .ToList();

        public bool IsHaveCategoryById(int id)
        => this.data.Categories.Any(x => x.Id == id);

        public IEnumerable<CategoryViewModel> GetAllCategories()
        => this.data.Categories.Select(x => new CategoryViewModel() { Id = x.Id, CategoryName = x.Name });

    }
}
