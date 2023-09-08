using CarsBg_System.Models.Api.Price;

namespace CarsBg_System.Services.Price
{
    public interface IPriceService
    {


        MainResponseModel GetPrices(int id);

    }
}
