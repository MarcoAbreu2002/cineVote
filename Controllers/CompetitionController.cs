using cineVote.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace cineVote.Controllers
{
    public class CompetitionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult createCompetition()
        {
            var response = new createCompetitionModel();
            return View(response);
        }
    }
}
