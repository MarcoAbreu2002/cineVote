using cineVote.Controllers;
using cineVote.Models.Domain;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace cineVote.Repositories.Implementation
{
    public class PopupNotificationObserver : IObserver
    {
        private UserController controller;

        public void Update(Subscription subscription)
        {
            string message = "";
            if (subscription.Competition.StartDate >= DateTime.Now && subscription.Competition.EndDate <= DateTime.Now)
            {
                message = $"A competição '{subscription.Competition.Name}' está aberta!";
            }
            else if (subscription.Competition.EndDate > DateTime.Now)
            {
                message = $"A competição '{subscription.Competition.Name}' está fechada!";
            }

            ShowPopupNotification(message, subscription.Competition.Competition_Id);
        }

        private void ShowPopupNotification(string message, int competitionId)
        {
            controller.ProcessNotification(message, competitionId);
        }
    }
}