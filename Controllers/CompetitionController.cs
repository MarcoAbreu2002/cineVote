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


        public async Task<IActionResult> Results(int competitionId)
        {
            var competition = _competitionManager.FindById(competitionId);

            // Find all subscriptions with the same competition_id as Competition
            var subscriptions = _context.Subscriptions
                .Where(s => s.Competition_Id == competitionId)
                .Select(s => s.SubscriptionId)
                .ToList();

            // Find all VoteSubscriptions that have the same subscription_id as Subscription
            var voteSubscriptions = _context.voteSubscriptions
                .Where(vs => subscriptions.Contains(vs.SubscriptionId))
                .Select(vs => vs.VoteId)
                .ToList();

            // Find all Votes that have their vote_id in VoteSubscription
            var votes = _context.Votes
                .Where(v => voteSubscriptions.Contains(v.VoteId))
                .ToList();

            var voteCounts = votes
                .GroupBy(v => new { v.CategoryId, v.NomineeId })
                .Select(group => new
                {
                    CategoryId = group.Key.CategoryId,
                    NomineeId = group.Key.NomineeId,
                    Count = group.Count()
                })
                .ToList();

            // Find the nominee with the most votes in each category
            var topNominees = voteCounts
                .GroupBy(vc => vc.CategoryId)
                .Select(group => new
                {
                    CategoryId = group.Key,
                    TopNomineeId = group.OrderByDescending(vc => vc.Count).FirstOrDefault()?.NomineeId
                })
                .ToList();

            var totalVoteCount = votes.Count;
            var totalCategories = topNominees.Count;
            int numberOfParticipants = totalVoteCount / totalCategories;

            var result = _competitionManager.generateResults(topNominees, numberOfParticipants, competitionId);

            return View();
        }


        public async Task<IActionResult> ShowResultsCompetition(int competitionId)
        {
            var competition = _competitionManager.FindById(competitionId);

            int firstPlace = 0;
            int secondPlace = 0;
            int thirdPlace = 0;

            var results = _context.Results
                .Where(cc => cc.Competition_Id == competitionId)
                .ToList();

            foreach (var result in results)
            {
                firstPlace = result.FirstPlaceId;
                secondPlace = result.SecondPlaceId;
                thirdPlace = result.ThirdPlaceId;
            }

            var categoryNominees = _context.CategoryNominees
                .Where(cn => cn.CategoryNomineeKey == firstPlace || cn.CategoryNomineeKey == secondPlace || cn.CategoryNomineeKey == thirdPlace)
                .ToList();

            foreach (var categoryNominee in categoryNominees)
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryNominee.CategoryId);
                var nominee = _context.Nominees.FirstOrDefault(n => n.NomineeId == categoryNominee.NomineeId);

                
            }

            return View();
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
