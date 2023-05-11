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
        private readonly string _connectionString = "Data Source=engenhariasoftware.database.windows.net;Initial Catalog=cinevote;Persist Security Info=True;User ID=engenharisoftwareadmin;Password=pDu8jRkmh3kQAfx";


        public UserAuthenticationController(UserManager<Person>? userManager, SignInManager<Person>? signInManager, AppDbContext? context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
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
            if (!ModelState.IsValid) return View(loginModel);
            var user = await _userManager.FindByEmailAsync(loginModel.EmailAddress);

            if (user == null) 
            {
                var passCheck = await _userManager.CheckPasswordAsync(user,loginModel.Password);
                if (passCheck != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false,false);
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

        }
        [HttpPost]
        public IActionResult Registration(RegistrationModel registrationModel)
        {

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql =
                        "INSERT INTO tblUsers (First_Name, Last_Name, Email, Username, Password, IsAdmin) VALUES (@First_Name, @Last_Name, @Email, @Username, @Password, @IsAdmin)";
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@First_Name", registrationModel.FirstName);
                    command.Parameters.AddWithValue("@Last_Name", registrationModel.LastName);
                    command.Parameters.AddWithValue("@Email", registrationModel.Email);
                    command.Parameters.AddWithValue("@Username", registrationModel.Username);
                    command.Parameters.AddWithValue("@Password", registrationModel.Password);
                    command.Parameters.AddWithValue("@IsAdmin", 0);
                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index", "Home");
            

            return View(registrationModel);
            // var response = new RegistrationModeSl();
            // return View(response);
        }
        

    }
}
