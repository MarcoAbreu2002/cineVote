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

        [ForeignKey("Competition_Id")]
        [Column("Competition_Id")]
        public int Competition_Id { get; set; }

        [ForeignKey("CategoryId")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("Competition")]
        public Competition? Competition { get; set; }

        [Column("Category")]
        public Category? Category { get; set; }
    }
}
