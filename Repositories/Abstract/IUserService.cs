using cineVote.Models.Domain;
using cineVote.Models.DTO;
namespace cineVote.Repositories.Abstract
{
    public interface IUserService
    {
        Task<Status> getProfile(int userId);
        Task<User> FindByUsernameAsync(string username);
        Task<Status> Subscribe(string username, int competitionId);
        Task<Status> Vote(string username, int competitionId, int categoryId, int nomineeId, int subscriptionId);

        Task<Status> Follow(string userIdToFollow);

        Task<Status> UnFollow(string userIdToUnfollow);

        Task<List<int>> getFavorites();

        public string getUserId();
        bool EditProfile(User user);
        bool readNotification(Notification notification);
    }
}