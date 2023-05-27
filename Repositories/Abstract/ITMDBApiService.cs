using System.Collections.Generic;
using System.Threading.Tasks;
namespace cineVote.Repositories.Abstract
{
    public interface ITMDBApiService
    {
        Task<List<Dictionary<string, object>>> GetPopularMovies();
    }
}
