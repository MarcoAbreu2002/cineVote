using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace cineVote.Repositories.Implementation
{
    public class SocialService : ISocialService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public SocialService(AppDbContext db, UserManager<Person> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Posts>> GetPostsAsync(string userName)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                return new List<Posts>(); // Return an empty list if the user is not found
            }

            string userId = user.Id;
            var userRelationships = _context.UserRelationships
                .Where(relationship => relationship.FollowerId == userId)
                .ToList();

            var followeeIds = userRelationships
                .Select(relationship => relationship.FolloweeId)
                .ToList();

            var usernames = new List<string>();
            foreach (var followeeId in followeeIds)
            {
                var userFollowing = await _userManager.FindByIdAsync(followeeId);
                if (userFollowing != null)
                {
                    var username = userFollowing.UserName;
                    usernames.Add(username);
                }
            }

            usernames.Add(userName);


            var posts = _context.Posts
                .Where(p => usernames.Contains(p.userName))
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

            return posts;
        }

        public Posts CreatePost(string userName, string title, string content)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            string userId = user.Id;

            var post = new Posts()
            {
                Title = title,
                Content = content,
                userName = userName,
                User = user
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public Comments CreateComment(string userName, int postId, string content)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var post = _context.Posts
                .FirstOrDefault(p => p.PostsId == postId);

            if (post == null)
            {
                throw new InvalidOperationException("Post not found.");
            }

            var comment = new Comments()
            {
                Content = content,
                PostsId = postId,
                userName = userName,
                User = user
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

    }
}
