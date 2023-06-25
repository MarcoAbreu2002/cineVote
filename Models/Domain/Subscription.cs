using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cineVote.Repositories.Abstract;

namespace cineVote.Models.Domain
{
    [Table("tblSubscription")]
    public class Subscription : IObservable
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(Subscription subscription)
        {
            foreach (var observer in observers)
            {
                observer.Update(subscription);
            }
        }


        [Key]
        [Column("SubscriptionId")]
        public int SubscriptionId { get; set; }


        [Column("Subscription_name")]
        public string? Name { get; set; }

        [ForeignKey("Competition_Id")]
        [Column("Competition_Id")]
        public int Competition_Id { get; set; }

        [Column("Competition")]
        public Competition? Competition { get; set; }

        [Column("User")]
        public User? User { get; set; }

        [ForeignKey("userName")]
        [Column("userName")]
        public string userName { get; set; }

        [ForeignKey("VoteId")]
        [Column("VoteId")]
        public int? VoteId { get; set; }

        [Column("SubscriptionNotifications")]
        public ICollection<SubscriptionNotifications>? SubscriptionNotifications { get; set; }

        [Column("Result")]
        public Result? Result { get; set; }
    }
}
