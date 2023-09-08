using CarsBg_System.Data;
using CarsBg_System.Models.Api.Price;

namespace CarsBg_System.Services.Price
{
    public class PriceService : IPriceService
    {

        private CarsDbContext data;


        public PriceService(CarsDbContext data)
        {
            this.data = data;
        }


        public MainResponseModel GetPrices(int id)
        => this.data.Cars
                       .Where(x => x.Id == id && x.IsDeleted == false)
                       .Select(x => new MainResponseModel()
                       {
                           Prices = x.Prices.Select(p => new PriceResponseModel()
                           {
                               Date = p.Date,
                               Price = p.Money
                           }).ToList()
                       })
                      .FirstOrDefault();
    }
}

