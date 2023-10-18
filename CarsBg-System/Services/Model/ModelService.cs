using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data;

namespace CarsBg_System.Services.Model
{
    public class ModelService : IModelService
    {

        private CarsDbContext data;


        public ModelService(CarsDbContext data)
        {
            this.data = data;
        }


        public async Task<bool> AddAsync(ModelFormModel modelForm)
        {
            bool isHave = this.data.Models.Any(x => x.Name == modelForm.Name);

            if (isHave)
            {
                return false;
            }

            var currentModel = new CarsBg_System.Data.Models.Model() { Name = modelForm.Name, BrandId = modelForm.BrandId };

            await this.data.Models.AddAsync(currentModel);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int modelId)
        {
            var model = this.data.Models.Where(x => x.Id == modelId).FirstOrDefault();

            if (model == null || model.IsDeleted == true)
            {
                return false;
            }

            model.IsDeleted = true;
            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<ShowModelViewModel> ShowModels()
        => this.data.Models
                     .Where(x => x.IsDeleted == false)
                     .Select(x => new ShowModelViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name,
                     })
                    .ToList();


        public bool IsHaveModelById(int id)
        => this.data.Models.Any(x => x.Id == id);
    }
}
