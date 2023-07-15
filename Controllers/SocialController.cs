using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cineVote.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(NotificationFilter))]
    public class SocialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISocialService _socialService;

        public SocialController(AppDbContext context, ISocialService socialService)
        {
            _context = context;
            _socialService = socialService;
        }

        public async Task<IActionResult> Index()
        {
            string userName = User.Identity.Name;
            var posts = await _socialService.GetPostsAsync(userName);

            return View(posts);
        }

        // POST: /Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Title, string Content)
        {
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                var post = _socialService.CreatePost(userName, Title, Content);

                return Json(post);
            }
            else
            {
                return BadRequest();
            }
        }

        /*
                // POST: /Post/Comment/5
                [HttpPost]
                [ValidateAntiForgeryToken]
                public IActionResult Comment(string Content, int PostsId)
                {
                      if (ModelState.IsValid)
                      {
                    string userName = User.Identity.Name;
                    var comment = _socialService.CreateComment(userName, PostsId, Content);

                    return Json(comment);
                     }
                     else
                     {
                         return BadRequest();
                     }
                }

        */
        // POST: /Post/Comment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Comment(string Content, int PostsId)
        {
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                var user = _context.Users
                    .FirstOrDefault(u => u.UserName == userName);

                if (user != null)
                {
                    string userId = user.Id;
                    // Use the userId as needed
                }

                Comments comment1 = new Comments()
                {
                    Content = Content,
                    PostsId = PostsId,
                    userName = userName,
                    User = user
                };
                _context.Comments.Add(comment1);
                _context.SaveChanges();
                return Json(comment1);
            }
            else
            {
                return BadRequest();
            }
        }


    }
}