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

        public SocialController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Post
        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {
                string userId = user.Id;
                // Use the userId as needed
            }

            var posts = _context.Posts
    .Where(p => p.userName == userName)
    .OrderByDescending(p => p.PostsId)
    .ToList();

            foreach (var post in posts)
            {
                var comments = _context.Comments
                    .Where(c => c.PostsId == post.PostsId)
                    .OrderByDescending(c => c.CommentsId)
                    .ToList();

                // Assign the comments to the post
                post.Comments = comments;
            }

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
                var user = _context.Users
                    .FirstOrDefault(u => u.UserName == userName);

                if (user != null)
                {
                    string userId = user.Id;
                    // Use the userId as needed
                }
                Posts post = new Posts()
                {
                    Title = Title,
                    Content = Content,
                    userName = userName,
                    User = user
                };
                _context.Posts.Add(post);
                _context.SaveChanges();

                return Json(post);
            }
            else
            {
                return BadRequest();
            }
        }



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