using CarsBg_System.Views.ViewModels.WheelDrive;

namespace CarsBg_System.Services.WheelDrive
{
    public interface IWheelDriveService
    {

        bool IsHaveWheelDriveById(int id);

        IEnumerable<WheelDriveViewModel> GetAllWheelDrives();

    }
}
