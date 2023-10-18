using CarsBg_System.Data;

namespace CarsBg_System.Services.Post
{
    public class PostService : IPostService
    {

        private CarsDbContext data;

        public PostService(CarsDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> AddPostAsync(int carId, string description, string userName)
        {
            var currentCar = this.data.Cars.Where(x => x.Id == carId && x.IsDeleted == false).FirstOrDefault();

            if (currentCar == null)
            {
                return false;
            }

            var newPost = new CarsBg_System.Data.Models.Post() { Comment = description, User = userName };

            currentCar.Posts.Add(newPost);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var post = this.data.Posts.Where(x => x.Id == postId).FirstOrDefault();

            if (post == null)
            {
                return false;
            }

            this.data.Posts.Remove(post);

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
