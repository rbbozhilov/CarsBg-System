using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Views.ViewModels.Transmission;

namespace CarsBg_System.Services.Transmission
{
    public interface ITransmissionService
    {

        Task<bool> AddAsync(TransmissionFormModel transmission);

        Task<bool> DeleteAsync(int transmissionId);

        bool IsHaveTransmissionById(int id);

        IEnumerable<TransmissionViewModel> GetAllTransmissions();

        IEnumerable<ShowTransmissionViewModel> ShowTransmissions();

    }
}
