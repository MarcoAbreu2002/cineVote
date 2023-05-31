using cineVote.Models.Domain;
using cineVote.Models.DTO;
namespace cineVote.Repositories.Abstract
{
    public interface ICompetitionManager
    {
        Task<Status> createCompetition(createCompetitionModel createCompetitionModel);
        bool removeCompetition(int competitionId);
        Competition FindById(int id);
        bool Edit(Competition competition);
        Task<Competition> getCompetition();

        Task<Status> generateResults(dynamic topNominees);
    }
}
