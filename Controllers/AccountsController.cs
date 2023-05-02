using Microsoft.AspNetCore.Mvc;

namespace cineVote.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }


    }
}
