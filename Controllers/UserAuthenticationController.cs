using cineVote.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;

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
            registrationModel.Role = "admin";
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
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();
            return RedirectToAction("Login", "UserAuthentication");
        }

    }
}