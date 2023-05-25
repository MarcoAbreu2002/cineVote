using Microsoft.EntityFrameworkCore;
using cineVote.Models.Domain;
using Microsoft.AspNetCore.Identity;
using cineVote.Models.DTO;
using Microsoft.Extensions.Options;
using cineVote.Repositories.Abstract;
using System.Security.Claims;

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


        public Task<Status> createCompetition(createCompetitionModel createCompetitionModel)
        {
            var status = new Status();
            Competition competition = new Competition()
            {
                Name = createCompetitionModel.Name,
                IsPublic = createCompetitionModel.isPublic,
                StartDate = createCompetitionModel.startDate,
                EndDate = createCompetitionModel.endDate,
                CategoryId = createCompetitionModel.category,
                AdminId = getAdminId()
            };

            _db.Add(competition);
            _db.SaveChanges();  

            status.StatusCode = 1;
            status.Message = "Account created succefully";
            return Task.FromResult(status);
        }

        public Task<Competition> getCompetition()
        {
            throw new NotImplementedException();
        }

        public Task<Status> removeCompetition(int competitionId)
        {
            throw new NotImplementedException();
        }


        private string getAdminId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

        Task<Competition> ICompetitionManager.getCompetition()
        {
            throw new NotImplementedException();
        }

        Task<Status> ICompetitionManager.removeCompetition(createCompetitionModel createCompetitionModel)
        {
            throw new NotImplementedException();
        }
    }
}