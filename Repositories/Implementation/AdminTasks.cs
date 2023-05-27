using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace cineVote.Repositories.Implementation
{
    public class AdminTasks : IAdminTasks
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Person> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public AdminTasks(AppDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<Person> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _httpClient = new HttpClient();
        }

        public async Task<Status> createCategory(CreateCategoryModel createCategoryModel)
        {
            var status = new Status();
            Category category = new Category()
            {
                Name = createCategoryModel.CategoryName,
                Description = createCategoryModel.CategoryDescription
            };

            _db.Add(category);
            await _db.SaveChangesAsync();

            status.StatusCode = 1;
            status.Message = "Category created successfully";
            return status;
        }

        public async Task<Status> createNominee(createNomineeModel createNomineeModel)
        {
            var status = new Status();
            Category category = new Category()
            {

            };

            _db.Add(category);
            await _db.SaveChangesAsync();

            status.StatusCode = 1;
            status.Message = "Category created successfully";
            return status;
        }
    }

}
