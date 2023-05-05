using cineVote.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Data;

namespace cineVote.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly UserManager<Person>? _userManager;
        private readonly SignInManager<Person>? _signInManager;
        private readonly AppDbContext? _context;


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


        public IActionResult Registration()
        {
            var response = new RegistrationModel();
            return View(response);
        }





    }
}
