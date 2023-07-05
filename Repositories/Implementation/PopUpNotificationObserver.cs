using cineVote.Controllers;
using cineVote.Models.Domain;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace cineVote.Repositories.Implementation
{
    public class PopupNotificationObserver : IObserver
    {
        private UserController controller;
        private readonly AppDbContext _db;

        public PopupNotificationObserver(AppDbContext db)
        {
            _db = db;
        }

        public void Update(Competition competition, string userName, Subscription subscription)
        {
            string message = "";
            if (competition.StartDate <= DateTime.Now && competition.EndDate > DateTime.Now)
            {
                message = $"The Competition '{competition.Name}' just started!";
            }
            else if (competition.EndDate < DateTime.Now && competition.IsPublic == false)
            {
                message = $"The Results of the Competition '{competition.Name}' have been generated!";
            }

            ShowPopupNotification(message, subscription.Competition_Id, userName, subscription);
        }

        private void ShowPopupNotification(string message, int competitionId, string userName, Subscription subscription)
        {
            Notification notification = new Notification()
            {
                Name = message,
                SubscriptionId = subscription.SubscriptionId,
                userName = userName,
                isRead = false

            };
            _db.Notifications.Add(notification);
            _db.SaveChanges();
        }
    }
}