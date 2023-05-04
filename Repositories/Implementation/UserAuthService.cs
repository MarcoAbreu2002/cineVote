using cineVote.Repositories.Abstract;
using cineVote.Models;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using cineVote.Models.DTO;

namespace cineVote.Repositories.Implementation
{
    public class UserAuthService : IUserAuthService
    {
        public Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        {
            throw new NotImplementedException();
        }

        public Task<Status> LoginAsync(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> RegisterAsync(RegistrationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
