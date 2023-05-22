using cineVote.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using cineVote.Repositories.Abstract;

namespace cineVote.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly AppDbContext? _context;
        private readonly IUserAuthService _authService;

        public UserAuthenticationController(AppDbContext? context, IUserAuthService authService)
        {
            this._authService = authService;
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
        public async Task<IActionResult> Registration(RegistrationModel registrationModel)
        {
            if (ModelState.IsValid) { return View(registrationModel); }
            registrationModel.Role = "user";
            var result = await this._authService.RegisterAsync(registrationModel);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            // Check if the login credentials are valid
            var result = await _authService.LoginAsync(loginModel);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

    }
}