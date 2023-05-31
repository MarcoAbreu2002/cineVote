using cineVote.Models.Domain;
using cineVote.Models.DTO;
namespace cineVote.Repositories.Abstract
{
    public interface IUserService
    {
        Task<Status> getProfile(int userId);
        Task<User> FindByUsernameAsync(string username);
        Task<Status>  Subscribe (string username, int competitionId, IObserver observer);
        public bool removeCompetition(int subscriptionId, IObserver observer);
        bool EditProfile(User user);
    }
}