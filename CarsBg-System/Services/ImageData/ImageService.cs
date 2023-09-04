using CarsBg_System.Data;
using CarsBg_System.Models.Image;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace CarsBg_System.Services.ImageData
{

    public class ImageService : IImageService
    {

        private const int carouselWidth = 720;
        private const int smallWidth = 200;
        private IServiceScopeFactory serviceScopeFactory;

        public ImageService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }


        public async Task Process(IEnumerable<ImageInputModel> images,int carId)
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
    }
}
