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
        List<Posts> posts = _context.Posts.ToList();
        return View(posts);
    }

    // POST: /Post/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Posts post)
    {
        if (ModelState.IsValid)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return Index();
    }

    // GET: /Post/Details/5
    public IActionResult Details(int id)
    {
        Posts post = _context.Posts.Include(p => p.Comments).FirstOrDefault(p => p.PostsId == id);
        return View(post);
    }

    // POST: /Post/Comment/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Comment(int id, Comments comment)
    {
        if (ModelState.IsValid)
        {
            Posts post = _context.Posts.FirstOrDefault(p => p.PostsId == id);
            if (post != null)
            {
                comment.PostsId = post.PostsId;
                post.Comments.Add(comment);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = post.PostsId });
            }
        }
        return RedirectToAction("Index");
    }
        
    }
}