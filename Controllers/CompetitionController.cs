using Microsoft.AspNetCore.Mvc;

namespace cineVote.Controllers
{
    public class CompetitionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
