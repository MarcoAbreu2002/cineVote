using cineVote.Models.Domain;
using cineVote.Models.DTO;
namespace cineVote.Repositories.Abstract
{
    public interface ICompetitionManager
    {
        Task<Status> createCompetition(createCompetitionModel createCompetitionModel);
        Task<Status> removeCompetition(createCompetitionModel createCompetitionModel);
        Task<Competition> getCompetition();
    }
}
