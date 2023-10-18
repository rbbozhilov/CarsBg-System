namespace CarsBg_System.Services.Post
{
    public interface IPostService
    {

        Task<bool> AddPostAsync(int carId,string description,string userName);

        Task<bool> DeletePostAsync(int postId);

    }
}
