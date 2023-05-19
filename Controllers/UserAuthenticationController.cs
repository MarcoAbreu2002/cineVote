using cineVote.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Data;
using Microsoft.Data.SqlClient;

namespace cineVote.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly UserManager<Person>? _userManager;
        private readonly SignInManager<Person>? _signInManager;
        private readonly AppDbContext? _context;
        private readonly IUserAuthService _authService;

        private readonly string _connectionString =
            "Data Source=engenhariasoftware.database.windows.net;Initial Catalog=cinevote;Persist Security Info=True;User ID=engenharisoftwareadmin;Password=pDu8jRkmh3kQAfx";


        public UserAuthenticationController(UserManager<Person>? userManager, SignInManager<Person>? signInManager,
            AppDbContext? context,IUserAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            this._authService = authService;

        }

        public IActionResult Index()
        {
            return Login();
        }

        public IActionResult Login()
        {
            var response = new LoginModel();
            return View(response);
        }

        public IActionResult Registration()
        {
            var response = new RegistrationModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var result = await _authService.LoginAsync(loginModel);
            if(result.StatusCode==1)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }

            /*
            if (!ModelState.IsValid) return View(loginModel);
            var user = await _userManager.FindByEmailAsync(loginModel.EmailAddress);

            if (user == null)
            {
                var passCheck = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                if (passCheck != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "Wrong credentials";
                return View(loginModel);
            }

            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginModel);
            */
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel registrationModel)
        {
            if(!ModelState.IsValid) { return View(registrationModel); }
            registrationModel.Role = "user";
            var result = await this._authService.RegisterAsync(registrationModel);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
            /*
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql_username = "SELECT COUNT(*) FROM tblUsers WHERE Username = @Username";
                var command_username = new SqlCommand(sql_username, connection);
                command_username.Parameters.AddWithValue("@Username", registrationModel.Username);
                var count_username = (int)command_username.ExecuteScalar();
                
                var sql_email = "SELECT COUNT(*) FROM tblUsers WHERE Email = @Email";
                var command_email = new SqlCommand(sql_email, connection);
                command_email.Parameters.AddWithValue("@Email", registrationModel.Email);
                var count_email = (int)command_email.ExecuteScalar();

                if (count_username > 0)
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(registrationModel);
                }

                if (count_email > 0)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(registrationModel);
                }

                var sql =
                    "INSERT INTO tblUsers (First_Name, Last_Name, Email, Username, Password, IsAdmin, User_Id) VALUES (@First_Name, @Last_Name, @Email, @Username, @Password, @IsAdmin, @User_Id)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@First_Name", registrationModel.FirstName);
                command.Parameters.AddWithValue("@Last_Name", registrationModel.LastName);
                command.Parameters.AddWithValue("@Email", registrationModel.Email);
                command.Parameters.AddWithValue("@Username", registrationModel.Username);
                command.Parameters.AddWithValue("@Password", registrationModel.Password);
                command.Parameters.AddWithValue("@User_Id", Guid.NewGuid());
                command.Parameters.AddWithValue("@IsAdmin", 0);
                command.ExecuteNonQuery();


                return RedirectToAction("Index", "Home");
            }
            */
            // var response = new RegistrationModeSl();
            // return View(response);
        }


        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin()
        {
            RegistrationModel model = new RegistrationModel
            {
                Username="admin",
                Email="admin@test.com",
                FirstName="First",
                LastName="Admin",
                Password="passwordtest#",
            };
            model.Role = "admin";
            var result = await this._authService.RegisterAsync(model);
            return Ok(result);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();  
            return RedirectToAction(nameof(Login));
        }
    }
}