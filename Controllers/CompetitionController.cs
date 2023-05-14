using cineVote.Data;
using cineVote.Models.DTO;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace cineVote.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly AppDbContext? _context;

        private readonly string _connectionString =
            "Data Source=engenhariasoftware.database.windows.net;Initial Catalog=cinevote;Persist Security Info=True;User ID=engenharisoftwareadmin;Password=pDu8jRkmh3kQAfx";

        public CompetitionController(AppDbContext? context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.tblCategory;
            return View(objCategoryList);
        }
        
        public IActionResult createCompetition()
        {
            var response = new createCompetitionModel();
            return View(response);
        }

        [HttpPost]
        public IActionResult createCompetition(createCompetitionModel createCompetitionModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql =
                    "INSERT INTO tblCompetition (Name, IsPublic, Category, StartDate, EndDate, Competition_Id) VALUES (@Name, @IsPublic, @Category, @StartDate, @EndDate,@Competition_Id)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name", createCompetitionModel.Name);
                command.Parameters.AddWithValue("@IsPublic", createCompetitionModel.isPublic);
                command.Parameters.AddWithValue("@Category", Guid.NewGuid());
                command.Parameters.AddWithValue("@StartDate", createCompetitionModel.startDate);
                command.Parameters.AddWithValue("@EndDate", createCompetitionModel.endDate);
                command.Parameters.AddWithValue("@Competition_Id", Guid.NewGuid());
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Home");


        return View(createCompetitionModel);
            // var response = new RegistrationModeSl();
            // return View(response);
        }
    }
}
