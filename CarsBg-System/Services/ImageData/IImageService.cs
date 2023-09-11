using CarsBg_System.Models.Image;

namespace CarsBg_System.Services.ImageData
{
    public interface IImageService
    {

        Task Process(IEnumerable<ImageInputModel> images, int carId);

        Task<List<string>> GetAllImages();

        Task<Stream> GetCourseImages(string id);

        Task<Stream> GetSmallImage(string id);

        Task<Stream> GetOriginalImage(string id);

    }
}
