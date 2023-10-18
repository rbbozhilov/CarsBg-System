using CarsBg_System.Areas.Admin.Models.Brand;
using CarsBg_System.Services.Brand;
using CarsBg_System.Tests.Mock;

using Xunit;


namespace CarsBg_System.Tests.Services
{
    public class BrandServiceTest
    {
        [Fact]
        public async Task AddBrand_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var brandService = new BrandService(data);
            var information = new BrandFormModel() { Name = "Test" };


            //Act

            bool isAdded = await brandService.AddAsync(information);
            var result = data.Brands.Any(x => x.Name == "Test");

            //Assert

            Assert.True(isAdded);
            Assert.True(result);

        }

        [Fact]
        public async Task AddBrand_ShouldReturnFalse_SameBrandAddedAgain()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var brandService = new BrandService(data);
            var information = new BrandFormModel() { Name = "Test" };


            //Act

            await brandService.AddAsync(information);
            bool result = await brandService.AddAsync(information);

            //Assert

            Assert.False(result);

        }

        [Fact]
        public async Task DeleteBrand_ShouldReturnTrueAndDelete()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var brandService = new BrandService(data);
            var information = new BrandFormModel() { Name = "Test" };



            //Act

            await brandService.AddAsync(information);
            var brand = data.Brands.Where(x => x.Name == "Test").FirstOrDefault();
            bool isDeleted = await brandService.DeleteAsync(brand.Id);


            //Assert

            Assert.True(isDeleted);
            Assert.True(brand.IsDeleted);
        }

        [Fact]
        public async Task DeleteBrand_ShouldFalse_NotExistBrand()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var brandService = new BrandService(data);

            //Act & Assert

            Assert.False(await brandService.DeleteAsync(2));
        }

        [Fact]
        public async Task DeleteBrand_ShouldReturnFalse_BrandIsAlreadyDeleted()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var brandService = new BrandService(data);
            var information = new BrandFormModel() { Name = "Test" };


            //Act

            await brandService.AddAsync(information);
            var brand = data.Brands.Where(x => x.Name == "Test").FirstOrDefault();
            await brandService.DeleteAsync(brand.Id);

            bool result = await brandService.DeleteAsync(brand.Id);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task ReturnAllBrands()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var brandService = new BrandService(data);
            var brand1 = new BrandFormModel() { Name = "Test" };
            var brand2 = new BrandFormModel() { Name = "Test1" };
            var brand3 = new BrandFormModel() { Name = "Test2" };


            //Act

            await brandService.AddAsync(brand1);
            await brandService.AddAsync(brand2);
            await brandService.AddAsync(brand3);
            var result = brandService.ShowBrands();
            var isHaveBrand1 = result.Any(x => x.Name == "Test");
            var isHaveBrand2 = result.Any(x => x.Name == "Test1");
            var isHaveBrand3 = result.Any(x => x.Name == "Test2");

            //Assert

            Assert.Equal(3, result.Count());
            Assert.True(isHaveBrand3);
            Assert.True(isHaveBrand2);
            Assert.True(isHaveBrand1);
        }

        [Fact]
        public async Task HaveBrandId_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var brandService = new BrandService(data);
            var information = new BrandFormModel() { Name = "Test" };


            //Act

            await brandService.AddAsync(information);
            var brandId = data.Brands.Where(x => x.Name == "Test").Select(x => x.Id).FirstOrDefault();

            bool result = brandService.IsHaveBrandById(brandId);

            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task HaveBrandId_ShouldReturnFalse_DontHaveThisBrandId()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var brandService = new BrandService(data);

            //Act & Assert

            Assert.False(brandService.IsHaveBrandById(2));
        }

    }
}
