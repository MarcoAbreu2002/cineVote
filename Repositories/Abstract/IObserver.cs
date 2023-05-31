using cineVote.Models.Domain;

namespace cineVote.Repositories.Abstract
{
    public interface IObserver
    {
        void Update(Subscription subscription);
    }
}
