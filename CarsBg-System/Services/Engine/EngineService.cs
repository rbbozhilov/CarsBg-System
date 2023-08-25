using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data;
using CarsBg_System.Views.ViewModels.Engine;

namespace CarsBg_System.Services.Engine
{
    public class EngineService : IEngineService
    {

        private CarsDbContext data;

        public EngineService(CarsDbContext data)
        {
            this.data = data;
        }

        public bool Add(EngineFormModel engine)
        {
            bool isHave = this.data.Engines.Any(x => x.Name == engine.Name);

            if (isHave)
            {
                return false;
            }

            var currentEngine = new CarsBg_System.Data.Models.Engine() { Name = engine.Name };

            this.data.Engines.Add(currentEngine);
            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int engineId)
        {
            var engine = this.data.Categories.Where(x => x.Id == engineId).FirstOrDefault();

            if (engine == null || engine.IsDeleted == true)
            {
                return false;
            }

            engine.IsDeleted = true;
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<ShowEngineViewModel> ShowEngines()
        => this.data.Engines
                     .Where(x => x.IsDeleted == false)
                     .Select(x => new ShowEngineViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name
                     })
                    .ToList();


        public bool IsHaveEngineById(int id)
        => this.data.Engines.Any(x => x.Id == id);

        public IEnumerable<EngineViewModel> GetAllEngines()
        => this.data.Engines.Select(x => new EngineViewModel() { Id = x.Id, EngineName = x.Name });


    }
}
