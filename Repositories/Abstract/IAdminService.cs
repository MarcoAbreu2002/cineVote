using cineVote.Models.Domain;
using cineVote.Models.DTO;
namespace cineVote.Repositories.Abstract
{
    public interface IAdminService
    {
        Task<Status> createCategory(CreateCategoryModel createCategoryModel);
        Task<Status> createNominee(createNomineeModel createNomineeModel);
    }
}
