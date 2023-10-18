using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Views.ViewModels.Engine;

namespace CarsBg_System.Services.Engine
{
    public interface IEngineService
    {

        Task<bool> AddAsync(EngineFormModel engine);

        Task<bool> DeleteAsync(int engineId);

        bool IsHaveEngineById(int id);

        IEnumerable<EngineViewModel> GetAllEngines();

        IEnumerable<ShowEngineViewModel> ShowEngines();

    }
}
