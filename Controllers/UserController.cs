using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cineVote.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private readonly AppDbContext? _context;
        private readonly IUserService _userService;

        public UserController(AppDbContext? context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IActionResult> ProfileAsync(string username)
        {
            var record = await _userService.FindByUsernameAsync(username);
            var subscriptions = _context.Subscriptions.ToList();

            // Filter the subscriptions list based on a condition
            var filteredSubscriptions = subscriptions.Where(s => s.userName == username).ToList();

            record.subscritions = filteredSubscriptions;

            return View(record);
        }

        public async Task<IActionResult> Subscription(int subscription)
        {
            var subscriptionToShow = _context.Subscriptions.Find(subscription);
            var Competition = _context.Competitions.Find(subscriptionToShow.Competition_Id);

            return View(Competition);
        }


        public async Task<IActionResult> EditProfile(string username)
        {
            var record = await _userService.FindByUsernameAsync(username);
            return View(record);
        }


        [HttpPost]
        public ActionResult Subscribe(string username, int competitionId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Profile", username);
            }

            var result = _userService.Subscribe(username, competitionId);


            return RedirectToAction("DisplayCompetition", "Competition");
        }


        [HttpPost]
        public IActionResult EditProfile(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var result = _userService.EditProfile(user);
            if (result)
            {
                return RedirectToAction("Profile", new { username = user.UserName });
            }
            TempData["msg"] = "Error has occured on server side";
            return View(user);
        }




    }
}
