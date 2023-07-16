using cineVote.Models.Domain;
using cineVote.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace cineVote.Repositories.Abstract
{
    public interface ICompetitionManager
    {
        Task<Status> createCompetition(createCompetitionModel createCompetitionModel);
        bool removeCompetition(int competitionId);
        Competition FindById(int id);
        bool Edit(Competition competition);
        Task<Competition> getCompetition();

        string startCompetition (Competition competition);

        string finishCompetition (Competition competition);

        Task<Status> generateResults(dynamic topNominees, int numberOfParticipants, int competition_id);

        Task<Status> addToFavorites (int movieId);

        Task<Status> removeFavorites (int movieId);
        
    }
}
