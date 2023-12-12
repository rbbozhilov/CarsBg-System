using CarsBg_System.Models.Api.Car;
using CarsBg_System.Services.Car;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Controllers.Api
{
    [ApiController]
    public class CarApiController : ControllerBase
    {

        private ICarService carService;

        public CarApiController(ICarService carService)
        {
            this.carService = carService;
        }

        [Route("api/topcars")]
        [HttpGet]
        public ActionResult<List<CarResponseModel>> GetTopCar()
        {

            var currentTopCar = this.carService.GetTopCar();

            if (currentTopCar == null)
            {
                return BadRequest();
            }

            return currentTopCar.ToList();
        }


    }
}
