using cineVote.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

namespace cineVote.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly AppDbContext? _context;

        public UserAuthenticationController(AppDbContext? context)
        {
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
        public async Task<IActionResult> Registration([Bind("PersonId, FirstName, ImageUrl ,LastName, Password, Email")] User user, RegistrationModel registrationModel)
        {
            if (ModelState.IsValid) { return View(registrationModel); }
            user.ImageUrl="testing";
            user.IsAdmin = false;
            Console.WriteLine("TEST: " + registrationModel + "USER: " + user);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            // Check if the login credentials are valid
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginModel.EmailAddress && u.Password == loginModel.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login credentials.");
                return View(loginModel);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}