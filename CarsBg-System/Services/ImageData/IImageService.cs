using CarsBg_System.Models.Image;

namespace CarsBg_System.Services.ImageData
{
    public interface IImageService
    {

        Task Process(IEnumerable<ImageInputModel> images,int carId);

    }
}
