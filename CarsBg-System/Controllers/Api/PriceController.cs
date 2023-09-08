using CarsBg_System.Models.Api.Price;
using CarsBg_System.Services.Price;
using Microsoft.AspNetCore.Mvc;

namespace CarsBg_System.Controllers.Api
{
    [ApiController]
    public class PriceController : ControllerBase
    {

        private IPriceService priceService;

        public PriceController(IPriceService priceService)
        {
            this.priceService = priceService;
        }


        [Route("api/prices/{id}")]
        [HttpGet]
        public ActionResult<MainResponseModel> PriceCarInformation(int id)
        {

            var currentPrices = this.priceService.GetPrices(id);

            if(currentPrices == null)
            {
                return BadRequest();
            }

            return currentPrices;
        }
    }
}
