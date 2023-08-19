using CarsBg_System.Data;

namespace CarsBg_System.Services.Brand
{
    public class BrandService : IBrandService
    {

        private CarsDbContext data;

        public BrandService(CarsDbContext data)
        {
            this.data = data;
        }

        public bool IsHaveBrandById(int id)
        => this.data.Brands.Any(x => x.Id == id);
    }
}
