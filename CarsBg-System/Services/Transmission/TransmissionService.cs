using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
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

        public async Task<bool> AddAsync(TransmissionFormModel transmission)
        {
            bool isHave = this.data.Transmissions.Any(x => x.Name == transmission.Name);

            if (isHave)
            {
                return false;
            }

            var currentTransmission = new CarsBg_System.Data.Models.Transmission() { Name = transmission.Name };

            await this.data.Transmissions.AddAsync(currentTransmission);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int transmissionId)
        {
            var transmission = this.data.Transmissions.Where(x => x.Id == transmissionId).FirstOrDefault();

            if (transmission == null || transmission.IsDeleted == true)
            {
                return false;
            }

            transmission.IsDeleted = true;
            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<ShowTransmissionViewModel> ShowTransmissions()
        => this.data.Transmissions
                     .Where(x => x.IsDeleted == false)
                     .Select(x => new ShowTransmissionViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name
                     })
                    .ToList();


        public bool IsHaveTransmissionById(int id)
        => this.data.Transmissions.Any(x => x.Id == id);

        public IEnumerable<TransmissionViewModel> GetAllTransmissions()
        => this.data.Transmissions.Select(x => new TransmissionViewModel() { Id = x.Id, TransmissionName = x.Name });

    }
}
