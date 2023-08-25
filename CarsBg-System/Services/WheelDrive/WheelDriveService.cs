using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data;
using CarsBg_System.Views.ViewModels.WheelDrive;

namespace CarsBg_System.Services.WheelDrive
{
    public class WheelDriveService : IWheelDriveService
    {

        private CarsDbContext data;

        public WheelDriveService(CarsDbContext data)
        {
            this.data = data;
        }


        public bool Add(WheelDriveFormModel wheelDrive)
        {
            bool isHave = this.data.WheelDrives.Any(x => x.Name == wheelDrive.Name);

            if (isHave)
            {
                return false;
            }

            var currentWheelDrive = new CarsBg_System.Data.Models.WheelDrive() { Name = wheelDrive.Name };

            this.data.WheelDrives.Add(currentWheelDrive);
            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int wheelDriveId)
        {
            var wheelDrive = this.data.WheelDrives.Where(x => x.Id == wheelDriveId).FirstOrDefault();

            if (wheelDrive == null)
            {
                return false;
            }

            this.data.WheelDrives.Remove(wheelDrive);
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<ShowWheelDriveViewModel> ShowWheelDrives()
        => this.data.WheelDrives
                     .Select(x => new ShowWheelDriveViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name
                     })
                    .ToList();


        public bool IsHaveWheelDriveById(int id)
        => this.data.WheelDrives.Any(x => x.Id == id);

        public IEnumerable<WheelDriveViewModel> GetAllWheelDrives()
        => this.data.WheelDrives.Select(x => new WheelDriveViewModel() { Id = x.Id, WheelDriveName = x.Name });

    }
}
