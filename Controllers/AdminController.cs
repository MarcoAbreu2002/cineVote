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

    }
}
