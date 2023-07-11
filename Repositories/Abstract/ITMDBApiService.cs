using System.Collections.Generic;
using System.Threading.Tasks;
namespace cineVote.Repositories.Abstract
{
    public interface ITMDBApiService
    {
        Task<List<Dictionary<string, object>>> GetPopularMovies();

        Task<List<Dictionary<string, object>>> GetMovieByName(string name);

        Task<List<Dictionary<string, object>>> GetMovieById(List<int> nomineeIds);

        Task<List<Dictionary<string, object>>> GetSingleMovieById(int nomineeId);
    }
}
