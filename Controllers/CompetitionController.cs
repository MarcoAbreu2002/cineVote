using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cineVote.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> SingleCompetition(int competitionId)
        {
            var result = _competitionManager.FindById(competitionId);

            var competitionCategories = _context.CompetitionCategories
                .Where(cc => cc.Competition_Id == competitionId)
                .Select(cc => cc.Category)
                .ToList();

            var nomineesCompetition = _context.NomineeCompetitions.ToList();
            var filterNomineesCompetition = nomineesCompetition
                .Where(s => s.Competition_Id == competitionId)
                .ToList();
            var nomineeIds = filterNomineesCompetition.Select(n => n.NomineeId).ToList();

            var nominees = _context.Nominees
                .Where(n => nomineeIds.Contains(n.NomineeId))
                .ToList();

            var nomineeIdTMDB = nominees.Select(n => n.TMDBId).ToList();
            result.Nominees = nominees;
            result.Categories = competitionCategories;

            result.Movies = await _ITMDBApiService.GetMovieById(nomineeIdTMDB);

            return View(result);
        }





        public IActionResult EditCompetition(int competitionId)
        {
            var record = _competitionManager.FindById(competitionId);
            return View(record);
        }

        [HttpPost]
        public IActionResult EditCompetition(Competition model)
        {
            // if (!ModelState.IsValid)
            //{
            //   return View(model);
            // }
            var result = _competitionManager.Edit(model);
            if (result)
            {
                return RedirectToAction("DisplayCompetition");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
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
