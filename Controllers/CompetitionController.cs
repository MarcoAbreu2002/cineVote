using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cineVote.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(NotificationFilter))]
    public class CompetitionController : Controller
    {
        private readonly AppDbContext? _context;
        private readonly ICompetitionManager _competitionManager;
        private readonly ITMDBApiService _ITMDBApiService;

        public CompetitionController(AppDbContext context, ICompetitionManager competitionManager, ITMDBApiService ITMDBApiService)
        {
            _context = context;
            _competitionManager = competitionManager;
            _ITMDBApiService = ITMDBApiService;
        }



        public IActionResult DisplayCompetition()
        {
            List<Competition> competitions = _context.Competitions.ToList();
            string userName = User.Identity.Name;
            return View(competitions);
        }

        public async Task<IActionResult> ShowMoreDetails(int movieId)
        {
            var Movies = await _ITMDBApiService.GetSingleMovieById(movieId);
            return View("ShowMoreDetails",Movies);
        }

        public IActionResult Delete(int competitionId)
        {
            var result = _competitionManager.removeCompetition(competitionId);
            return RedirectToAction("DisplayCompetition");
        }

        public async Task<IActionResult> ShowResultsCompetition(int competitionId)
        {
            var competition = _competitionManager.FindById(competitionId);

            var results = _context.Results
                .Where(cc => cc.Competition_Id == competitionId)
                .ToList();

            List<Category> categories = GetCategories(results);
            List<Nominee> nominees = GetNominees(results);

            competition.Categories = categories;
            competition.Nominees = nominees;
            var finalWinners = await GetFinalWinnersAsync(competition, nominees);

            competition.finalWinners = finalWinners;

            return View(competition);
        }

        private List<Category> GetCategories(List<Result> results)
        {
            List<Category> categories = new List<Category>();

            foreach (var result in results)
            {
                var category = _context.Categories
                    .FirstOrDefault(cc => cc.CategoryId == result.CategoryId);

                if (category != null)
                {
                    categories.Add(category);
                }
            }

            return categories;
        }



        private List<Nominee> GetNominees(List<Result> results)
        {
            List<Nominee> nominees = new List<Nominee>();

            foreach (var result in results)
            {
                var firstPlaceNominee = _context.Nominees
                    .FirstOrDefault(n => n.NomineeId == result.FirstPlaceId);

                var secondPlaceNominee = _context.Nominees
                    .FirstOrDefault(n => n.NomineeId == result.SecondPlaceId);

                var thirdPlaceNominee = _context.Nominees
                    .FirstOrDefault(n => n.NomineeId == result.ThirdPlaceId);

                nominees.Add(firstPlaceNominee);
                nominees.Add(secondPlaceNominee);
                nominees.Add(thirdPlaceNominee);
            }

            return nominees;
        }

        private async Task<Dictionary<Category, List<Dictionary<string, object>>>> GetFinalWinnersAsync(Competition competition, List<Nominee> nominees)
        {
            var finalWinners = new Dictionary<Category, List<Dictionary<string, object>>>();
            int nomineeIndex = 0;
            var nomineeIds = nominees
                .Where(n => n != null) // Check if TMDBId is not null
                .Select(n => n.TMDBId)
                .ToList();
            int k = 0;

            foreach (var category in competition.Categories)
            {
                var tmdbDict = new List<Dictionary<string, object>>();
                for (int i = 0; i < 3; i++)
                {
                    if (nomineeIndex < competition.Nominees.Count)
                    {
                        if (competition.Nominees[nomineeIndex] != null)
                        {
                            if (nomineeIds != null && nomineeIds.Count > 0)
                            {
                                tmdbDict.AddRange(await _ITMDBApiService.GetSingleMovieById(nomineeIds[k]));
                                k++;
                            }
                        }
                        else
                        {
                            tmdbDict.Add(null);
                        }
                        nomineeIndex++;
                    }
                }
                finalWinners.Add(category, tmdbDict);
            }

            return finalWinners;
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
            var result = _competitionManager.createCompetition(createCompetitionModel);
            return RedirectToAction(nameof(CreateCompetition));
        }
    }
}
