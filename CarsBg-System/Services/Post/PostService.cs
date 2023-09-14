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

        public bool AddPost(int carId,string description)
        {
            var currentCar = this.data.Cars.Where(x => x.Id == carId).FirstOrDefault();

            if (currentCar == null)
            {
                return false;
            }

            var newPost = new CarsBg_System.Data.Models.Post() { Comment = description };

            currentCar.Posts.Add(newPost);

            this.data.SaveChanges();

            return true;
        }
    }
}
