using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cineVote.Models.DTO;

namespace cineVote.Models.Domain
{
    [Table("tblUser")]
    public class User : Person
    {
        [ForeignKey("SubscriptionId")]
        [Column("SubscriptionId")]
        public int SubscriptionId { get; set; }

        [Column("subscriptions")]
        public ICollection<Subscription>? subscritions { get; set; }

        [Column("Votes")]
        public ICollection<Vote>? votes { get; set; }

        public List<Person>? followings { get; set; }

        [NotMapped]
        public List<Dictionary<string, object>> Movies { get; set; }

    }
}
