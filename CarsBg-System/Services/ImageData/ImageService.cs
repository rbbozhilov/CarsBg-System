using CarsBg_System.Data;
using CarsBg_System.Models.Image;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace CarsBg_System.Services.ImageData
{

    public class ImageService : IImageService
    {

        private const int carouselWidth = 720;
        private const int smallWidth = 200;
        private IServiceScopeFactory serviceScopeFactory;
        private CarsDbContext data;

        public ImageService(IServiceScopeFactory serviceScopeFactory, CarsDbContext data)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.data = data;
        }

        public Task<List<string>> GetAllImages()
        => this.data.ImageData
            .Select(i => i.Id.ToString())
            .ToListAsync();

        public Task<Stream> GetCourseImages(string id)
        => this.GetImageData(id, "Carousel");

        public Task<Stream> GetSmallImage(string id)
        => this.GetImageData(id, "Small");

        public async Task Process(IEnumerable<ImageInputModel> images, int carId)
        {

            var tasks = new List<Task>();

            foreach (var image in images)
            {
                tasks.Add(Task.Run(async () =>
                {
                    using var imageResult = await Image.LoadAsync(image.Content);

                    var original = await this.SaveImage(imageResult, imageResult.Width);
                    var carouse = await this.SaveImage(imageResult, carouselWidth);
                    var small = await this.SaveImage(imageResult, smallWidth);

                    var data = this.serviceScopeFactory
                                      .CreateScope()
                                      .ServiceProvider
                                      .GetRequiredService<CarsDbContext>();

                    data.ImageData.Add(new Data.Models.ImageData()
                    {
                        OriginalFileName = image.Name,
                        OriginalType = image.Type,
                        OriginalContent = original,
                        CarouselContent = carouse,
                        SmallContent = small,
                        CarId = carId
                    });

                    await data.SaveChangesAsync();
                }));
            }

            await Task.WhenAll(tasks);
        }

        private async Task<byte[]> SaveImage(Image image, int resizeWidth)
        {
            var width = image.Width;
            var height = image.Height;

            if (width > resizeWidth)
            {
                height = (int)((double)resizeWidth / width * height);
                width = resizeWidth;
            }

            image.Mutate(i => i.Resize(new Size(height, width)));

            image.Metadata.ExifProfile = null; // Removing all meta data from user image.

            var memoryStream = new MemoryStream();

            await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
            {
                Quality = 75
            });

            return memoryStream.ToArray();
        }

        private async Task<Stream> GetImageData(string id, string size)
        {
            var database = this.data.Database;

            var dbConnection = (SqlConnection)database.GetDbConnection();

            var command = new SqlCommand(

                $"SELECT {size}Content FROM ImageData WHERE Id = @id;"
                , dbConnection);

            command.Parameters.Add(new SqlParameter("@id", id));

            dbConnection.Open();

            var reader = await command.ExecuteReaderAsync();

            Stream result = null;

            if (reader.HasRows)
            {
                while (reader.Read()) result = reader.GetStream(0);
            }

            reader.Close();

            return result;
        }

    }
}
