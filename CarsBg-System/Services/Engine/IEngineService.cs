using CarsBg_System.Views.ViewModels.Engine;

namespace CarsBg_System.Services.Engine
{
    public interface IEngineService
    {

        IEnumerable<EngineViewModel> GetAllEngines();

    }
}
