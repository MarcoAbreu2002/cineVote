using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using cineVote.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cineVote.Controllers
{
    [ServiceFilter(typeof(NotificationFilter))]
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private readonly AppDbContext? _context;
        private readonly IUserService _userService;

        private readonly ITMDBApiService _ITMDBApiService;

        public UserController(AppDbContext? context, IUserService userService, ITMDBApiService ITMDBApiService)
        {
            _context = context;
            _userService = userService;
            _ITMDBApiService = ITMDBApiService;
        }

        public async Task<IActionResult> ProfileAsync(string username)
        {
            var record = await _userService.FindByUsernameAsync(username);
            var subscriptions = _context.Subscriptions.ToList();
            var userId = _userService.getUserId();
            var followings = _context.UserRelationships.ToList();
            var favoriteIds = await _userService.getFavorites();
            record.Movies = await _ITMDBApiService.GetMovieById(favoriteIds);

            // Filter the subscriptions list based on a condition
            var filteredSubscriptions = subscriptions.Where(s => s.userName == username).ToList();

            // Get the user IDs that the current user is following
            var followingUserIds = followings.Where(f => f.FollowerId == userId).Select(f => f.FolloweeId).ToList();

            // Retrieve the users that the current user is following
            var followingsList = _context.People.Where(u => followingUserIds.Contains(u.Id)).ToList();
            record.subscritions = filteredSubscriptions;
            record.followings = followingsList;


            return View(record);
        }



        public async Task<IActionResult> Vote()
        {
            string username = Request.Form["username"][0];

            int competitionId = int.Parse(Request.Form["competitionId"][0]);

            int subscriptionId = int.Parse(Request.Form["subscriptionId"][0]);

            foreach (var formData in Request.Form)
            {
                if (formData.Key.StartsWith("category-"))
                {
                    int categoryId = int.Parse(formData.Key.Replace("category-", ""));
                    int nomineeId = int.Parse(formData.Value);
                    var result = _userService.Vote(username, competitionId, categoryId, nomineeId, subscriptionId);
                }
            }

            return RedirectToAction("Profile", "User", new { username });
        }

        public async Task<IActionResult> Subscription(int subscription)
        {
            var userId = _userService.getUserId();

            var subscriptionToShow = _context.Subscriptions.Find(subscription);

            var subscriptionVote = _context.voteSubscriptions
                        .Where(cc => cc.SubscriptionId == subscriptionToShow.SubscriptionId)
                        .Select(cc => cc.VoteId)
                        .ToList();

            bool hasVoteWithUserId = _context.Votes.Any(vote => vote.User.Id == userId && subscriptionVote.Contains(vote.VoteId));


            var result = _context.Competitions.Find(subscriptionToShow.Competition_Id);

            if(hasVoteWithUserId)
            {
                result.voted = true;
            }
            else
            {
                result.voted = false;
            }
            var competitionCategories = _context.CompetitionCategories
                           .Where(cc => cc.Competition_Id == result.Competition_Id)
                           .Select(cc => cc.Category)
                           .ToList();

            var nomineesCompetition = _context.NomineeCompetitions.ToList();
            var filterNomineesCompetition = nomineesCompetition
                .Where(s => s.Competition_Id == result.Competition_Id)
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

        public ActionResult TriggerNotification()
        {
            // Your logic to determine the notification message
            var notificationMessage = "New notification message!";

            // Call the JavaScript function to show the popup notification
            return Content($"<script>showNotification('{notificationMessage}');</script>");
        }

        public async Task<IActionResult> clickNotification(int competitionId, int notificationId, string notificationMessage)
        {
            var notification = _context.Notifications.Find(notificationId);
            _userService.readNotification(notification);

            if (notificationMessage.StartsWith("The Competition"))
            {
                return RedirectToAction("Subscription", new { subscription = competitionId });
            }
            else if (notificationMessage.StartsWith("The Results"))
            {
                var competitions = _context.Competitions.ToList();
                var subscriptions = _context.Subscriptions.ToList();

                var filteredSubscription = subscriptions.FirstOrDefault(s => s.SubscriptionId == competitionId);

                if (filteredSubscription != null)
                {
                    var filteredCompetition = competitions.FirstOrDefault(s => s.Competition_Id == filteredSubscription.Competition_Id);

                    if (filteredCompetition != null)
                    {
                        return RedirectToAction("ShowResultsCompetition", "Competition", new { competitionId = filteredCompetition.Competition_Id });
                    }
                }
                return RedirectToAction("Error");
            }

            return RedirectToAction("Error");
        }




        public async Task<IActionResult> EditProfile(string username)
        {
            var record = await _userService.FindByUsernameAsync(username);
            var subscriptions = _context.Subscriptions.ToList();

            // Filter the subscriptions list based on a condition
            var filteredSubscriptions = subscriptions.Where(s => s.userName == username).ToList();

            record.subscritions = filteredSubscriptions;

            return View(record);
        }


        [HttpPost]
        public ActionResult Subscribe(string username, int competitionId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Profile", new { username = username });
            }

            var status = _userService.Subscribe(username, competitionId).Result;

            if (status.StatusCode == 1)
            {
                return RedirectToAction("Profile", new { username = username });

            }
            else if (status.StatusCode == 0)
            {
                TempData["msg"] = status.Message;
            }
            else
            {
                TempData["msg"] = "An error occurred during subscription.";
            }

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

        public async Task<IActionResult> Follow(string userIdToFollow)
        {
            var result = await _userService.Follow(userIdToFollow);

            return RedirectToAction("Profile", "User", new { userId = userIdToFollow });
        }

        public async Task<IActionResult> Unfollow(string userIdToUnfollow)
        {
            var result = _userService.UnFollow(userIdToUnfollow);

            return RedirectToAction("Profile", "User", new { userId = userIdToUnfollow });

        }




    }
}
