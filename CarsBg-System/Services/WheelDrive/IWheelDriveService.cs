using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Views.ViewModels.WheelDrive;

namespace CarsBg_System.Services.WheelDrive
{
    public interface IWheelDriveService
    {

        bool Add(WheelDriveFormModel wheelDrive);

        bool Delete(int wheelDriveId);

        bool IsHaveWheelDriveById(int id);

        IEnumerable<WheelDriveViewModel> GetAllWheelDrives();

        IEnumerable<ShowWheelDriveViewModel> ShowWheelDrives();

    }
}
