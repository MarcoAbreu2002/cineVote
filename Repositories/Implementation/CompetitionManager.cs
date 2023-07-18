using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Newtonsoft.Json;
using cineVote.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace cineVote.Repositories.Implementation
{
    public class CompetitionManager : ICompetitionManager
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Person> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly CompetitionController _competitionController;

        public CompetitionManager(AppDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<Person> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string startCompetition(Competition competition)
        {
            var data = this.FindById(competition.Competition_Id);
            data.IsPublic = true;
            _db.Competitions.Update(data);
            _db.SaveChanges();

            List<Subscription> subscriptions = _db.Subscriptions
                .Where(s => s.Competition_Id == competition.Competition_Id)
                .ToList();

            PopupNotificationObserver observer = new PopupNotificationObserver(_db);
            competition.Attach(observer);

            // Assign each subscription to a specific observer
            for (int i = 0; i < subscriptions.Count; i++)
            {
                Subscription subscription = subscriptions[i];
                competition.Notify(competition, subscription.userName, subscription);
            }

            return "Começou a competição";
        }

        public string finishCompetition(Competition competition)
        {
            var data = this.FindById(competition.Competition_Id);
            data.IsPublic = false;
            _db.Competitions.Update(data);
            _db.SaveChanges();

            Results(competition.Competition_Id);

            List<Subscription> subscriptions = _db.Subscriptions
                .Where(s => s.Competition_Id == competition.Competition_Id)
                .ToList();

            PopupNotificationObserver observer = new PopupNotificationObserver(_db);
            competition.Attach(observer);

            // Assign each subscription to a specific observer
            for (int i = 0; i < subscriptions.Count; i++)
            {
                Subscription subscription = subscriptions[i];
                competition.Notify(competition, subscription.userName, subscription);
            }

            return "Terminou a competição";
        }


        public Task<Status> generateResults(dynamic topNominees, int numberOfParticipants, int competition_id)
        {
            var status = new Status();

            foreach (var topNominee in topNominees)
            {
                var categoryId = topNominee.CategoryId;
                var nominees = topNominee.Nominees;

                int? FirstPlaceId = null;
                int? SecondPlaceId = null;
                int? ThirdPlaceId = null;

                for (int i = 0; i < Math.Min(nominees.Count, 3); i++)
                {
                    var nomineeId = nominees[i].NomineeId;

                    CategoryNominee categoryNominee = new CategoryNominee()
                    {
                        CategoryId = categoryId,
                        NomineeId = nomineeId
                    };
                    _db.CategoryNominees.Add(categoryNominee);
                    _db.SaveChanges();

                    // Assign the IDs to FirstPlaceId, SecondPlaceId, and ThirdPlaceId
                    if (i == 0)
                    {
                        FirstPlaceId = nomineeId;
                    }
                    else if (i == 1)
                    {
                        SecondPlaceId = nomineeId;
                    }
                    else if (i == 2)
                    {
                        ThirdPlaceId = nomineeId;
                    }
                }

                Result results = new Result()
                {
                    TotalParticipants = numberOfParticipants,
                    FirstPlaceId = (int)FirstPlaceId,
                    SecondPlaceId = SecondPlaceId, // Assign the nullable value directly
                    ThirdPlaceId = ThirdPlaceId, // Assign the nullable value directly
                    Competition_Id = competition_id,
                    CategoryId = categoryId
                };

                _db.Results.Add(results);
                _db.SaveChanges();
            }

            status.StatusCode = 1;
            status.Message = "Results generated successfully";
            return Task.FromResult(status);
        }

        public void Results(int competitionId)
        {
            var competition = FindById(competitionId);

            // Find all subscriptions with the same competition_id as Competition
            var subscriptions = _db.Subscriptions
                .Where(s => s.Competition_Id == competitionId)
                .Select(s => s.SubscriptionId)
                .ToList();

            // Find all VoteSubscriptions that have the same subscription_id as Subscription
            var voteSubscriptions = _db.voteSubscriptions
                .Where(vs => subscriptions.Contains(vs.SubscriptionId))
                .Select(vs => vs.VoteId)
                .ToList();

            // Find all Votes that have their vote_id in VoteSubscription
            var votes = _db.Votes
                .Where(v => voteSubscriptions.Contains(v.VoteId))
                .ToList();

            var voteCounts = votes
                .GroupBy(v => new { v.CategoryId, v.NomineeId })
                .Select(group => new
                {
                    CategoryId = group.Key.CategoryId,
                    NomineeId = group.Key.NomineeId,
                    Count = group.Count()
                })
                .ToList();

            // Group voteCounts by CategoryId
            var groupedVoteCounts = voteCounts
                .GroupBy(vc => vc.CategoryId)
                .ToDictionary(group => group.Key, group => group.ToList());

            // Find the nominee with the most votes in each category
            var topNominees = groupedVoteCounts
    .Select(categoryVotes => new
    {
        CategoryId = categoryVotes.Key,
        Nominees = categoryVotes.Value.OrderByDescending(vc => vc.Count).ToList()
    })
    .ToList();


            var totalVoteCount = votes.Count;
            var totalCategories = topNominees.Count;
            int numberOfParticipants;
            if(totalVoteCount >0){
                numberOfParticipants = totalVoteCount / totalCategories;
            }
            else{
                numberOfParticipants = 0;
            }

            var result = generateResults(topNominees, numberOfParticipants, competitionId);

        }

        public async Task<Status> addToFavorites(int movieId)
        {
            var status = new Status();
            var userId = getAdminId();
            var user = await _userManager.FindByIdAsync(userId);

            Favorite favorite = new Favorite()
            {
                TMDBId = movieId,
                userName = user.UserName,
                User = (User)user
            };
            _db.Favorites.Add(favorite);
            _db.SaveChanges();

            status.StatusCode = 1;
            status.Message = "Favorite added successfully";
            return status;
        }

        public async Task<Status> removeFavorites(int movieId)
        {
            var status = new Status();
            var userId = getAdminId();
            var user = await _userManager.FindByIdAsync(userId);
            var favorite = _db.Favorites.FirstOrDefault(f => f.TMDBId == movieId && f.User.Id == userId);

            if (favorite != null)
            {
                _db.Favorites.Remove(favorite);
                _db.SaveChanges();

                status.StatusCode = 1;
                status.Message = "Favorite removed successfully";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Favorite not found or already removed";
            }
            return status;
        }


        public Task<Status> createCompetition(createCompetitionModel createCompetitionModel)
        {
            var status = new Status();
            int[] nomineeDBIdArray = JsonConvert.DeserializeObject<int[]>(createCompetitionModel.NomineeDBId);
            int[] categoryArray = JsonConvert.DeserializeObject<int[]>(createCompetitionModel.category);

            Competition competition = new Competition()
            {
                Name = createCompetitionModel.Name,
                IsPublic = createCompetitionModel.isPublic,
                StartDate = createCompetitionModel.startDate,
                EndDate = createCompetitionModel.endDate,
                AdminId = getAdminId(),
                Categories = new List<Category>() // Initialize the Categories collection
            };

            _db.Competitions.Add(competition);

            _db.SaveChanges();

            foreach (int categoryId in categoryArray)
            {
                CompetitionCategory competitionCategory = new CompetitionCategory()
                {
                    CategoryId = categoryId,
                    Competition_Id = competition.Competition_Id

                };
                _db.CompetitionCategories.Add(competitionCategory);
            }
            _db.SaveChanges();

            foreach (int nomineeId in nomineeDBIdArray)
            {
                Nominee nominee = new Nominee()
                {
                    TMDBId = nomineeId,
                };
                _db.Nominees.Add(nominee);
                _db.SaveChanges();

                NomineeCompetition nomineeCompetition = new NomineeCompetition()
                {
                    Competition_Id = competition.Competition_Id,
                    NomineeId = nominee.NomineeId
                };

                _db.NomineeCompetitions.Add(nomineeCompetition);
            }

            _db.SaveChanges();

            status.StatusCode = 1;
            status.Message = "Account created successfully";
            return Task.FromResult(status);
        }

        public Task<Competition> getCompetition()
        {
            throw new NotImplementedException();
        }

        public bool removeCompetition(int competitionId)
        {
            try
            {
                var competitions = this.FindById(competitionId);
                var subscriptions = _db.Subscriptions
                .Where(cc => cc.Competition_Id == competitionId)
                .ToList();
                foreach (var subscription in subscriptions)
                {
                    var voteSubscriptions = _db.voteSubscriptions
                    .Where(cc => cc.SubscriptionId == subscription.SubscriptionId)
                    .ToList();

                    _db.voteSubscriptions.RemoveRange(voteSubscriptions);

                    _db.Subscriptions.Remove(subscription);
                }

                _db.Competitions.Remove(competitions);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Competition competition)
        {
            try
            {
                _db.Competitions.Update(competition);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Competition FindById(int id)
        {
            return _db.Competitions.Find(id);
        }

        private string getAdminId()
        {
            var principal = _httpContextAccessor?.HttpContext?.User;
            if (principal == null)
            {
                return "User not Found";
            }

            string userId = _userManager?.GetUserId(principal);
            if (userId == null)
            {
                return "User not Found";
            }

            return userId;
        }




        Task<Competition> ICompetitionManager.getCompetition()
        {
            throw new NotImplementedException();
        }
    }
}
