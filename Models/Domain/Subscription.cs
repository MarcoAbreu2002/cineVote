using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblSubscription")]
    public class Subscription
    {
        [Key]
        [Column("Subscription_id")]
        public int SubscriptionId { get; set; }

        [Column("Subscription_name")]
        public string? Name { get; set; }

        [ForeignKey("Competition")]
        [Column("Competition_Id")]
        public int CompetitionId { get; set; }

        [Column("Competition")]
        public Competition? Competition { get; set; }

        [Column("UsersRegistered")]
        public ICollection<User>? UsersRegistered { get; set; }

        [Column("VotesReceived")]
        public ICollection<Vote>? VotesReceived { get; set; }

        [Column("SubscriptionNotifications")]
        public ICollection<SubscriptionNotifications>? SubscriptionNotifications { get; set; }

        [Column("Result")]
        public Result? Result { get; set; }
    }
}
