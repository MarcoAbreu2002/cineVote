using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblCompetitionCategory")]
    public class CompetitionCategory
    {
        [Key]
        [Column("CompetitionCategoryId")]
        public int CompetitionCategoryId { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Competition Competition { get; set; }
        public Category Category { get; set; }
    }
}
