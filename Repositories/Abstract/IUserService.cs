using cineVote.Models.Domain;
using cineVote.Models.DTO;
namespace cineVote.Repositories.Abstract
{
    public interface IUserService
    {
        Task<Status> getProfile(int userId);
        Task<User> FindByUsernameAsync(string username);

        bool EditProfile(User user);
    }
}