using cineVote.Models.Domain;

namespace cineVote.Repositories.Abstract
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(Subscription subscription, string userName);
    }
}