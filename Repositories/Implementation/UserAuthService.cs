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
        private readonly SignInManager<Person>? signInManager;
        private readonly UserManager<Person>? userManager;
        private readonly RoleManager<IdentityRole>? roleManager;

        public UserAuthService(RoleManager<IdentityRole>? roleManager,UserManager<Person>? userManager,SignInManager<Person>? signInManager)
        {
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {
             var status = new Status();
            var user = await userManager.FindByNameAsync(model.EmailAddress);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid email address";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
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
             await signInManager.SignOutAsync();
        }

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if(userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
                return status;
            }

            Person user = new Person
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user,model.Password);
            if(!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            //roles
            if(!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));
            
            if(await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "Message has been created";
            return status;
        }
    }
}
