using Microsoft.AspNetCore.Mvc.Filters;

namespace cineVote.Models.Domain
{
    public class NotificationFilter : IActionFilter
    {
        private readonly AppDbContext _context;

        public NotificationFilter(AppDbContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string userName = context.HttpContext.User.Identity.Name;
            var notifications = _context.Notifications
                .Where(n => n.userName == userName)
                .ToList();

            // Store the notifications in the ViewBag for access in the view
            context.HttpContext.Items["notifications"] = notifications;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method is optional and can be left empty.
        }
    }
}