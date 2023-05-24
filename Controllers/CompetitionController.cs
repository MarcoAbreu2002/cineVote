using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace cineVote.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly AppDbContext? _context;

        private readonly ICompetitionManager _CompetitionManager;

        public CompetitionController(AppDbContext? context, ICompetitionManager competitionManager)
        {
            _context = context;
            _CompetitionManager = competitionManager;
        }
        public IActionResult Index()
        {
            //IEnumerable<Competition> objCompetitionList = _context.tblCompetition.Include(c => c.CategoryEntity).ToList();
            return View(/*objCompetitionList*/);
        }


        public IActionResult createCompetition()
        {
            var model = new createCompetitionModel();
            model.categoryList = _context.Categories.ToList();
            return View(model);
        }



        [HttpPost]
        public IActionResult createCompetition(createCompetitionModel createCompetitionModel)
        {
            if (ModelState.IsValid) { return View(createCompetitionModel); }
            var result = this._CompetitionManager.createCompetition(createCompetitionModel);
            //TempData["msg"] = result.Message;
            return RedirectToAction(nameof(createCompetition));
        }
    }
}
