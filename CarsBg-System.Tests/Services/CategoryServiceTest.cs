using CarsBg_System.Areas.Admin.Models.Category;
using CarsBg_System.Services.Category;
using CarsBg_System.Tests.Mock;

using Xunit;

namespace CarsBg_System.Tests.Services
{
    public class CategoryServiceTest
    {

        [Fact]
        public async Task AddCategory_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var information = new CategoryFormModel() { Name = "Test" };


            //Act

            await categoryService.AddAsync(information);
            bool result = data.Categories.Any(x => x.Name == "Test");

            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task AddCategory_ShouldReturnFalse_HaveSameCategory()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var information = new CategoryFormModel() { Name = "Test" };


            //Act

            await categoryService.AddAsync(information);
            bool result = await categoryService.AddAsync(information);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnTrueAndDelete()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var information = new CategoryFormModel() { Name = "Test" };



            //Act

            await categoryService.AddAsync(information);
            var category = data.Categories.Where(x => x.Name == "Test").FirstOrDefault();
            bool isDeleted = await categoryService.DeleteAsync(category.Id);


            //Assert

            Assert.True(isDeleted);
            Assert.True(category.IsDeleted);
        }

        [Fact]
        public async Task DeleteCategory_ShouldFalse_NotExistCategory()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);

            //Act & Assert

            Assert.False(await categoryService.DeleteAsync(2));
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnFalse_CategoryIsAlreadyDeleted()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var information = new CategoryFormModel() { Name = "Test" };


            //Act

            await categoryService.AddAsync(information);
            var category = data.Categories.Where(x => x.Name == "Test").FirstOrDefault();
            await categoryService.DeleteAsync(category.Id);

            bool result = await categoryService.DeleteAsync(category.Id);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task ShowAllCategories_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var category1 = new CategoryFormModel() { Name = "Test" };
            var category2 = new CategoryFormModel() { Name = "Test2" };
            var category3 = new CategoryFormModel() { Name = "Test3" };


            //Act

            await categoryService.AddAsync(category1);
            await categoryService.AddAsync(category2);
            await categoryService.AddAsync(category3);

            var result = categoryService.ShowCategories();


            //Assert

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task HaveCategoryById_ReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var category1 = new CategoryFormModel() { Name = "Test" };

            //Act

            await categoryService.AddAsync(category1);
            var currentCategoryId = data.Categories.Where(x => x.Name == "Test").Select(x => x.Id).FirstOrDefault();
            var result = categoryService.IsHaveCategoryById(currentCategoryId);


            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task HaveCategoryById_ReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);

            //Act & Assert

            Assert.False(categoryService.IsHaveCategoryById(5));
        }

        [Fact]
        public async Task GetAllCategories_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var category1 = new CategoryFormModel() { Name = "Test" };
            var category2 = new CategoryFormModel() { Name = "Test2" };
            var category3 = new CategoryFormModel() { Name = "Test3" };


            //Act

            await categoryService.AddAsync(category1);
            await categoryService.AddAsync(category2);
            await categoryService.AddAsync(category3);

            var result = categoryService.GetAllCategories();


            //Assert

            Assert.Equal(3, result.Count());
        }

    }
}
