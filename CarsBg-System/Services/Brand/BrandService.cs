using CarsBg_System.Areas.Admin.Models.Brand;
using CarsBg_System.Areas.Admin.Views.ViewModels;
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

        public async Task<bool> AddAsync(BrandFormModel brand)
        {
            bool isHave = this.data.Brands.Any(x => x.Name == brand.Name);

            if (isHave)
            {
                return false;
            }

            var currentBrand = new CarsBg_System.Data.Models.Brand() { Name = brand.Name };

            await this.data.Brands.AddAsync(currentBrand);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int brandId)
        {
            var brand = this.data.Brands.Where(x => x.Id == brandId).FirstOrDefault();

            if (brand == null || brand.IsDeleted == true)
            {
                return false;
            }

            brand.IsDeleted = true;
            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<ShowBrandViewModel> ShowBrands()
        => this.data.Brands
                     .Where(x => x.IsDeleted == false)
                     .Select(x => new ShowBrandViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name
                     })
                    .ToList();

        public bool IsHaveBrandById(int id)
        => this.data.Brands.Any(x => x.Id == id);
    }
}
