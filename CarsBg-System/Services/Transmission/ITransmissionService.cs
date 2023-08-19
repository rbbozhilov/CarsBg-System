using CarsBg_System.Views.ViewModels.Transmission;

namespace CarsBg_System.Services.Transmission
{
    public interface ITransmissionService
    {

        bool IsHaveTransmissionById(int id);


        IEnumerable<TransmissionViewModel> GetAllTransmissions();

    }
}
