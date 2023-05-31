using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblVoteSubscription")]
    public class VoteSubscription
    {

        [Key]
        public int VoteSubscriptionKey {get;set;}

        [ForeignKey("SubscriptionId")]
        [Column("SubscriptionId")]
        public int SubscriptionId { get; set; }

        [Column("Subscription")]
        public Subscription? Subscription { get; set; }

        [ForeignKey("VoteId")]
        [Column("VoteId")]
        public int VoteId { get; set; }

        [Column("Vote")]
        public Vote? Vote { get; set; }
    }
}
