using Microsoft.EntityFrameworkCore;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Models.DTO;
using Microsoft.Extensions.Options;

namespace cineVote.Repositories.Implementation
{
    public class UserAuthService 
    {
        private readonly DbSet<Person>? _userCollection;

        public UserAuthService(IOptions<AppDbContext> databaseSettings)
        {
             //var connectionString = databaseSettings.Value.ConnectionString;
             var connectionString = "Data Source=engenhariasoftware.database.windows.net;Initial Catalog=cinevote;Persist Security Info=True;User ID=engenharisoftwareadmin;Password=pDu8jRkmh3kQAfx";
             Console.WriteLine("Conection string: " + connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new AppDbContext(optionsBuilder.Options))
            {
                _userCollection = dbContext.Set<Person>();
                if (_userCollection != null && _userCollection.Any())
                {
                    Console.WriteLine("User collection has been successfully loaded.");
                }
                else
                {
                    Console.WriteLine("Failed to load user collection or it is empty.");
                }
            }
        }
        public Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        {
            throw new NotImplementedException();
        }

    /*
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
            if (userExists != null)
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
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            //roles
            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "Message has been created";
            return status;
        }
        */
    }
    
}
