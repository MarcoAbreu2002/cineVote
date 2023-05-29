using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace cineVote.Repositories.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Person> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(AppDbContext db, UserManager<Person> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Status> getProfile(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            var userId = getUserId();
            var user = await _db.Users.FindAsync(userId);
            return user ?? throw new InvalidOperationException("User not found.");
        }


        private string getUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

        public bool EditProfile(User user)
        {
            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred during user update: " + ex.Message);
                return false;
            }
        }


    }
}
