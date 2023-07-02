using cineVote.Models;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cineVote.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db,ILogger<HomeController> logger)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            var notifications = _db.Notifications
                .Where(n => n.userName == userName)
                .ToList();

            return View(notifications);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}