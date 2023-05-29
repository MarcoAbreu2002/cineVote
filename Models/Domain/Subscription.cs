using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblSubscription")]
    public class Subscription
    {
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

        [ForeignKey("user_Id")]
        [Column("user_Id")]
        public string user_Id { get; set; }

        [ForeignKey("VoteId")]
        [Column("VoteId")]
        public int VoteId { get; set; }

        [Column("SubscriptionNotifications")]
        public ICollection<SubscriptionNotifications>? SubscriptionNotifications { get; set; }

        [Column("Result")]
        public Result? Result { get; set; }
    }
}
