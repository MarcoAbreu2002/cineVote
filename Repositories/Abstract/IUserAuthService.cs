using cineVote.Models.DTO;
namespace cineVote.Repositories.Abstract
{
    public interface IUserAuthService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
        Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
