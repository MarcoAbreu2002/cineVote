using Microsoft.EntityFrameworkCore;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Models.DTO;
using Microsoft.Extensions.Options;
using cineVote.Repositories.Abstract;
using System.Security.Claims;
using Newtonsoft.Json;

namespace cineVote.Repositories.Implementation
{
    public class CompetitionManager : ICompetitionManager
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Person> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

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
            var subscriptions = _db.Subscriptions
                .Where(s => s.Competition_Id == competition.Competition_Id)
                .ToList();

            foreach(var subscription in subscriptions)
            {
                subscription.Notify(subscription,subscription.userName);
            }

            return "Começou a competição"; 
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
                var data = this.FindById(competitionId);
                _db.Competitions.Remove(data);
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
