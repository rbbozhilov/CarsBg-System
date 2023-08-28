using CarsBg_System.Areas.Admin.Models.Category;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {

        private ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryFormModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            bool isDone = categoryService.Add(categoryModel);

            if (!isDone)
            {
                return BadRequest();
            }


            return RedirectToAction("Index", nameof(CarController));
        }

        public IActionResult ShowCategory(IEnumerable<ShowCategoryViewModel> categoryModel)
        {
            var categories = this.categoryService.ShowCategories();

            return View(categories);
        }


        public IActionResult Delete(int id)
        {
            bool isDeleted = categoryService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ShowCategory));
        }


    }
}
