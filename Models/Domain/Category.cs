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

        [NotMapped]
        public ICollection<Vote>? Votes { get; set; }

        [NotMapped]
        [ForeignKey("CompetitionId")]
        public int CompetitionId { get; set; }

        [NotMapped]
        public Competition? Competition { get; set; }

        [NotMapped]
        public ICollection<CategoryNominee>? CategoryNominees { get; set; }

        [NotMapped]
        public Result? Result { get; set; }
    }
}
