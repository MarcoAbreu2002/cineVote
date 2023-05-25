using Microsoft.EntityFrameworkCore;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Models.DTO;
using Microsoft.Extensions.Options;
using cineVote.Repositories.Abstract;
using System.Security.Claims;

namespace cineVote.Repositories.Implementation
{
    public class UserAuthService : IUserAuthService
    {
        private readonly SignInManager<Person> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Person> _userManager;

        public UserAuthService(UserManager<Person> userManager, SignInManager<Person> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
                return status;
            }
            User user = new User()
            {
                NormalizedEmail = model.Email,
                EmailConfirmed = true,
                NormalizedUserName = model.Username,
                EmailAddress = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Password = model.Password,
                ImageUrl = "Testing",
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            if (!await _roleManager.RoleExistsAsync(model.Role))
                await _roleManager.CreateAsync(new IdentityRole(model.Role));


            if (await _roleManager.RoleExistsAsync(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;
        }

        public Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid Email";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }

            return status;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }

}
