using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Services.Engine;
using CarsBg_System.Tests.Mock;

using Xunit;

namespace CarsBg_System.Tests.Services
{
    public class EngineServiceTest
    {

        [Fact]
        public async Task AddEngine_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);
            var information = new EngineFormModel() { Name = "Test" };


            //Act

            await engineService.AddAsync(information);
            bool result = data.Engines.Any(x => x.Name == "Test");

            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task AddEngine_ShouldReturnFalse_HaveSameEngine()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);
            var information = new EngineFormModel() { Name = "Test" };


            //Act

            await engineService.AddAsync(information);
            bool result = await engineService.AddAsync(information);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteEngine_ShouldReturnTrueAndDelete()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);
            var information = new EngineFormModel() { Name = "Test" };



            //Act

            await engineService.AddAsync(information);
            var engine = data.Engines.Where(x => x.Name == "Test").FirstOrDefault();
            bool isDeleted = await engineService.DeleteAsync(engine.Id);


            //Assert

            Assert.True(isDeleted);
            Assert.True(engine.IsDeleted);
        }

        [Fact]
        public async Task DeleteEngine_ShouldFalse_NotExistEngine()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);

            //Act & Assert

            Assert.False(await engineService.DeleteAsync(2));
        }

        [Fact]
        public async Task DeleteEngine_ShouldReturnFalse_EngineIsAlreadyDeleted()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);
            var information = new EngineFormModel() { Name = "Test" };


            //Act

            await engineService.AddAsync(information);
            var engine = data.Engines.Where(x => x.Name == "Test").FirstOrDefault();
            await engineService.DeleteAsync(engine.Id);

            bool result = await engineService.DeleteAsync(engine.Id);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task ShowAllEngines_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);
            var engine1 = new EngineFormModel() { Name = "Test" };
            var engine2 = new EngineFormModel() { Name = "Test2" };
            var engine3 = new EngineFormModel() { Name = "Test3" };


            //Act

            await engineService.AddAsync(engine1);
            await engineService.AddAsync(engine2);
            await engineService.AddAsync(engine3);

            var result = engineService.ShowEngines();


            //Assert

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task HaveEngineById_ReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);
            var engine1 = new EngineFormModel() { Name = "Test" };

            //Act

            await engineService.AddAsync(engine1);
            var currentEngineId = data.Engines.Where(x => x.Name == "Test").Select(x => x.Id).FirstOrDefault();
            var result = engineService.IsHaveEngineById(currentEngineId);


            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task HaveEngineById_ReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);

            //Act & Assert

            Assert.False(engineService.IsHaveEngineById(5));
        }

        [Fact]
        public async Task GetAllEngines_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var engineService = new EngineService(data);
            var engine1 = new EngineFormModel() { Name = "Test" };
            var engine2 = new EngineFormModel() { Name = "Test2" };
            var engine3 = new EngineFormModel() { Name = "Test3" };


            //Act

            await engineService.AddAsync(engine1);
            await engineService.AddAsync(engine2);
            await engineService.AddAsync(engine3);

            var result = engineService.GetAllEngines();


            //Assert

            Assert.Equal(3, result.Count());
        }


    }
}
