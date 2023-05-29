using cineVote.Models.Domain;
using cineVote.Models.DTO;
using cineVote.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace cineVote.Repositories.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Person> _userManager;

        public AdminService(AppDbContext db,UserManager<Person> userManager)
        {
            _db = db;
            _userManager = userManager;
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
