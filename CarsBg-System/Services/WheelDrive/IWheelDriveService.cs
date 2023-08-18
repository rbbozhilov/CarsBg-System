using CarsBg_System.Views.ViewModels.WheelDrive;

namespace CarsBg_System.Services.WheelDrive
{
    public interface IWheelDriveService
    {

        IEnumerable<WheelDriveViewModel> GetAllWheelDrives();

    }
}
