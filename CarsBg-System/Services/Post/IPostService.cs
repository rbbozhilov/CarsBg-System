namespace CarsBg_System.Services.Post
{
    public interface IPostService
    {

        bool AddPost(int carId,string description,string userName);

        bool DeletePost(int postId);

    }
}
