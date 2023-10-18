using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Model
{
    public interface IModelService
    {

        Task<bool> AddAsync(ModelFormModel modelForm);

        Task<bool> DeleteAsync(int modelId);

        bool IsHaveModelById(int id);

        IEnumerable<ShowModelViewModel> ShowModels();

    }
}
