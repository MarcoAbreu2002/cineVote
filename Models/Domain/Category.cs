using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblCategory")]
    public class Category
    {
        [Key]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("CategoryName")]
        public string Name { get; set; }

        [Column("CategoryDescription")]
        public string Description { get; set; }

        [Column("Nominee")]
        public ICollection<Vote>? Votes { get; set; }

        [ForeignKey("CompetitionId")]
        [Column("CompetitionId")]
        public int CompetitionId { get; set; }

        [Column("Competition")]
        public Competition? Competition { get; set; }

        [Column("CategoryNominees")]
        public ICollection<CategoryNominee>? CategoryNominees { get; set; }

        [Column("Result")]
        public Result? Result { get; set; }
    }
}
