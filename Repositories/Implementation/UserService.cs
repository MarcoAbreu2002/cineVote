using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System;

namespace cineVote.Repositories.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Person> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly List<IObserver> _observers = new List<IObserver>();


        public UserService(AppDbContext db, UserManager<Person> userManager, IHttpContextAccessor httpContextAccessor, List<IObserver> observers)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _observers = observers;
        }

        public Task<Status> getProfile(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            var userId = getUserId();
            var user = await _db.Users.FindAsync(userId);
            return user ?? throw new InvalidOperationException("User not found.");
        }


        private string getUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

        public bool EditProfile(User user)
        {
            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred during user update: " + ex.Message);
                return false;
            }
        }
        public Competition FindById(int id)
        {
            return _db.Competitions.Find(id);
        }
        public Task<Status> Subscribe(string username, int competitionId, IObserver observer)
        {
            var status = new Status();
            var userId = getUserId();
            User user =  _db.Users.Find(userId);
            var competition = FindById(competitionId);
            Subscription subscription = new Subscription()
            {
                Name = competition.Name,
                Competition_Id = competitionId,
                userName = user.UserName,
                User = user
            };
            user.SubscriptionId = subscription.SubscriptionId;
            _db.Users.Update(user);
            _db.Subscriptions.Add(subscription);
            _observers.Add(observer);
            NotifyObservers(subscription);
            _db.SaveChanges();


            status.StatusCode = 1;
            status.Message = "Account created successfully";
            return Task.FromResult(status);
        }

        public bool Unsubscribe(int subscriptionId, IObserver observer)
        {
            try
            {
                var data = _db.Subscriptions.Find(subscriptionId);
                if (data == null)
                    return false;
                _db.Subscriptions.Remove(data);
                _observers.Remove(observer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void NotifyObservers(Subscription subscription)
        {
            foreach (var observer in _observers)
            {
                observer.Update(subscription);
            }
        }

    }
}
