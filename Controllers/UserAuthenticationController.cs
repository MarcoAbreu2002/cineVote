using cineVote.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using cineVote.Models.Domain;

namespace cineVote.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthService _authService;
        public UserAuthenticationController(IUserAuthService authService)
        {
            this._authService = authService;
        }
        public IActionResult Index()
        {
            return Login();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
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

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Registration()
        {
            var user = new User();
            return View(user);
        }

        public IActionResult RegistrationSubmit(User user)
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "user";
            var result = await this._authService.RegisterAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }



    }
}
