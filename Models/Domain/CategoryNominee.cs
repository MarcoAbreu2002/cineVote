using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblCategoryNominee")]
    public class CategoryNominee
    {
        [Key]
        public int CategoryNomineeKey {get;set;}

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


    }
}
