using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Services.Extra;
using CarsBg_System.Tests.Mock;

using Xunit;

namespace CarsBg_System.Tests.Services
{
    public class ExtraServiceTest
    {

        [Fact]
        public async Task AddExtra_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var extraService = new ExtraService(data);
            var information = new ExtraFormModel() { Name = "Test" };


            //Act

            await extraService.AddAsync(information);
            bool result = data.Extras.Any(x => x.Name == "Test");

            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task AddExtra_ShouldReturnFalse_HaveSameExtra()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var extraService = new ExtraService(data);
            var information = new ExtraFormModel() { Name = "Test" };


            //Act

            await extraService.AddAsync(information);
            bool result = await extraService.AddAsync(information);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteExtra_ShouldReturnTrueAndDelete()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var extraService = new ExtraService(data);
            var information = new ExtraFormModel() { Name = "Test" };



            //Act

            await extraService.AddAsync(information);
            var extra = data.Extras.Where(x => x.Name == "Test").FirstOrDefault();
            bool isDeleted = await extraService.DeleteAsync(extra.Id);
            var result = data.Extras.Where(x => x.Name == "Test").FirstOrDefault();


            //Assert

            Assert.True(isDeleted);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteExtra_ShouldFalse_NotExistExtra()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var extraService = new ExtraService(data);

            //Act & Assert

            Assert.False(await extraService.DeleteAsync(2));
        }

        [Fact]
        public async Task ShowAllExtras_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var extraService = new ExtraService(data);
            var extra1 = new ExtraFormModel() { Name = "Test" };
            var extra2 = new ExtraFormModel() { Name = "Test2" };
            var extra3 = new ExtraFormModel() { Name = "Test3" };


            //Act

            await extraService.AddAsync(extra1);
            await extraService.AddAsync(extra2);
            await extraService.AddAsync(extra3);

            var result = extraService.ShowExtras();


            //Assert

            Assert.Equal(3, result.Count());
        }
    }
}
