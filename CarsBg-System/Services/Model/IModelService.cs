using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Services.Model
{
    public interface IModelService
    {

        bool Add(ModelFormModel modelForm);

        bool Delete(int modelId);

        bool IsHaveModelById(int id);

        IEnumerable<ShowModelViewModel> ShowModels();

    }
}
