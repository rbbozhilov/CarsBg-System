using CarsBg_System.Data;
using CarsBg_System.Views.ViewModels.Transmission;

namespace CarsBg_System.Services.Transmission
{
    public class TransmissionService : ITransmissionService
    {
        private CarsDbContext data;

        public TransmissionService(CarsDbContext data)
        {
            this.data = data;
        }


        public IEnumerable<TransmissionViewModel> GetAllTransmissions()
        => this.data.Transmissions.Select(x => new TransmissionViewModel() { Id = x.Id, TransmissionName = x.Name });
    }
}
