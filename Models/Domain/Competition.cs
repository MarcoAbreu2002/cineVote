using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblCompetition")]
    public class Competition
    {
        [Key]
        [Column("Competition_Id")] // Use the exact column name from the database
        public int Competition_Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("IsPublic")]
        public bool IsPublic { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [ForeignKey("CategoryId")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("AdminId")]
        [Column("AdminId")]
        public string AdminId { get; set; }

        [Column("CategoryEntity")]
        public ICollection<Category> Categories { get; set; }

        [Column("NomineeCompetitions")]
        public List<Nominee> Nominees { get; set; }

        [Column("Results")]
        public Result? Results { get; set; }
    }
}
