using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Views.ViewModels.Engine;

namespace CarsBg_System.Services.Engine
{
    public interface IEngineService
    {

        public bool Add(EngineFormModel engine);

        public bool Delete(int engineId);

        bool IsHaveEngineById(int id);

        IEnumerable<EngineViewModel> GetAllEngines();

        public IEnumerable<ShowEngineViewModel> ShowEngines();

    }
}
