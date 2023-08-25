using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Views.ViewModels.Transmission;

namespace CarsBg_System.Services.Transmission
{
    public interface ITransmissionService
    {

        bool Add(TransmissionFormModel transmission);

        bool Delete(int transmissionId);

        bool IsHaveTransmissionById(int id);

        IEnumerable<TransmissionViewModel> GetAllTransmissions();

        IEnumerable<ShowTransmissionViewModel> ShowTransmissions();

    }
}
