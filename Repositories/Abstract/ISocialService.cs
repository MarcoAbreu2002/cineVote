using cineVote.Models.Domain;
using cineVote.Models.DTO;
namespace cineVote.Repositories.Abstract
{
    public interface ISocialService
    {
        Task<List<Posts>> GetPostsAsync(string userName);
        Posts CreatePost(string userName, string title, string content);
        Comments CreateComment(string userName, int postId, string content);

        Task<Status> EditPost (string title, string content, int postId);

        Task<Status> RemovePost(int postId);
        Task<Status> EditComment (string content, int postId);
    }
}