using CarsBg_System.Areas.Admin.Models.Status;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data;

namespace CarsBg_System.Services.Status
{
    public class StatusService : IStatusService
    {

        private CarsDbContext data;

        public StatusService(CarsDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> AddAsync(StatusFormModel status)
        {
            bool isHave = this.data.Statuses.Any(x => x.StatusName == status.Name);

            if (isHave)
            {
                return false;
            }

            var currentStatus = new CarsBg_System.Data.Models.Status() { StatusName = status.Name };

            await this.data.Statuses.AddAsync(currentStatus);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int statusId)
        {
            var status = this.data.Statuses.Where(x => x.Id == statusId).FirstOrDefault();

            if (status == null)
            {
                return false;
            }

            this.data.Statuses.Remove(status);
            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<ShowStatusViewModel> ShowStatuses()
        => this.data.Statuses
                     .Select(x => new ShowStatusViewModel()
                     {
                         Id = x.Id,
                         Name = x.StatusName
                     })
                    .ToList();


    }
}
