using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using cineVote.Models.DTO;

namespace cineVote.Models.Domain
{
    [Table("tblResult")]
    public class Result
    {
        [Key]
        [Column("ResultID")]
        public int ResultId { get; set; }

        [Column("Competition")]
        public ICollection<Competition>? Competition { get; set; }

        [ForeignKey("NomineeId")]
        [Column("NomineeId")]
        public int NomineeId { get; set; }

        [Column("Nominee")]
        public Nominee? Nominee { get; set; }

        [ForeignKey("CategoryId")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("Category")]
        public Category? Category { get; set; }

        [ForeignKey("SubscriptionId")]
        [Column("SubscriptionId")]
        public int SubscriptionId { get; set; }

        [Column("Subscription")]
        public Subscription? Subscription { get; set; }

       
    }
}
