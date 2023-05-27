using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace cineVote.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext? _context;
        private readonly IAdminTasks _adminTasks;

        public AdminController(AppDbContext? context, IAdminTasks adminTasks)
        {
            _context = context;
            _adminTasks = adminTasks;
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateCategory()
        {
            var model = new CreateCategoryModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryModel createCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var result = _adminTasks.createCategory(createCategoryModel);
                // Redirect to the Admin Dashboard
                return RedirectToAction("AdminDashboard", "Admin");
            }

            return View(createCategoryModel);
        }
/*
        public async Task<IActionResult> CreateNominee()
        {
            // Call TMDB API to retrieve popular movies
            var movies = await GetPopularMovies();

            var model = new createNomineeModel
            {
                Movies = movies
            };

            return View(model);
        }




        [HttpPost]
        public IActionResult CreateNominee(createNomineeModel createNomineeModel)
        {
            if (ModelState.IsValid)
            {
                var result = _adminTasks.createNominee(createNomineeModel);
                // Redirect to the Admin Dashboard
                return RedirectToAction("AdminDashboard", "Admin");
            }

            return View(createNomineeModel);
        }

        private async Task<List<Dictionary<string, object>>> GetPopularMovies()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/movie/popular?language=en-US&page=1"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2Y2IwMjNjMmY4ZjNiODUwNTBkZjVhMjMxYzExZDZlNSIsInN1YiI6IjY0NzIwNDAzOWFlNjEzMDBhODA2Y2RkZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.umLcRjDrFarEpbLBkYgyMKkHcRGXoJZsgjlh1kszVJA" }, // Replace with your actual TMDB API key
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                // Parse the response and extract the movies
                var movies = ParseMovieResponse(body);
                return movies;
            }
        }

        private List<Dictionary<string, object>> ParseMovieResponse(string response)
        {
            // Parse the JSON response and create a list of dictionaries representing movies
            var movieData = JObject.Parse(response);
            var results = movieData["results"];

            var movies = new List<Dictionary<string, object>>();
            foreach (var result in results)
            {
                var movie = new Dictionary<string, object>
        {
            { "Id", result.Value<int>("id") },
            { "Title", result.Value<string>("title") },
            { "Overview", result.Value<string>("overview") },
            { "PosterPath", result.Value<string>("poster_path") },
            { "ReleaseDate", result.Value<string>("release_date") },
            // Add more properties as needed
        };
                movies.Add(movie);
            }

            return movies;
        }
        */

    }
}
