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

        public bool IsHaveModelById(int id)
        => this.data.Models.Any(x => x.Id == id);
    }
}
