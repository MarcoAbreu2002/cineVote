using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblVotes")]
    public class Vote
    {
        [Key]
        [Column("Vote_id")]
        public int VoteId { get; set; }

        [ForeignKey("UserId")]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("User")]
        public User User { get; set; }

        [ForeignKey("SubscriptionId")]
        [Column("SubscriptionId")]
        public int SubscriptionId { get; set; }

        [ForeignKey("CategoryId")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("NomineeId")]
        [Column("NomineeId")]
        public int NomineeId { get; set; }

        [Column("Category")]
        public Category Category { get; set; }

        [Column("Nominee")]
        public Nominee Nominee { get; set; }

        [Column("Subscription")]
        public Subscription Subscription { get; set; }

    }
}
