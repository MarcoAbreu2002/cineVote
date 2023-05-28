using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cineVote.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly AppDbContext? _context;
        private readonly ICompetitionManager _competitionManager;
        private readonly ITMDBApiService _ITMDBApiService;

        public CompetitionController(AppDbContext? context, ICompetitionManager competitionManager, ITMDBApiService ITMDBApiService)
        {
            _context = context;
            _competitionManager = competitionManager;
            _ITMDBApiService = ITMDBApiService;
        }
        public IActionResult DisplayCompetition()
        {
            List<Competition> competitions = _context.Competitions.ToList();

            return View(competitions);
        }

        public IActionResult Delete(int competitionId)
        {
            var result = _competitionManager.removeCompetition(competitionId);
            return RedirectToAction("DisplayCompetition");
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCompetition()
        {
            var model = new createCompetitionModel();
            model.categoryList = _context.Categories.ToList();
            model.nominees = await _ITMDBApiService.GetPopularMovies();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateCompetition(createCompetitionModel createCompetitionModel)
        {
            //if (ModelState.IsValid)
            // {
            var result = _competitionManager.createCompetition(createCompetitionModel);
            //TempData["msg"] = result.Message;
            return RedirectToAction(nameof(CreateCompetition));
            // }

            //return View(createCompetitionModel);
        }
    }
}
