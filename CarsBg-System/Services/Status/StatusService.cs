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

        public bool Add(StatusFormModel status)
        {
            bool isHave = this.data.Statuses.Any(x => x.StatusName == status.Name);

            if (isHave)
            {
                return false;
            }

            var currentStatus = new CarsBg_System.Data.Models.Status() { StatusName = status.Name };

            this.data.Statuses.Add(currentStatus);
            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int statusId)
        {
            var status = this.data.Statuses.Where(x => x.Id == statusId).FirstOrDefault();

            if (status == null)
            {
                return false;
            }

            this.data.Statuses.Remove(status);
            this.data.SaveChanges();

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
