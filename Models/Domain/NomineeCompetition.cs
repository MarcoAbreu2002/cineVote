using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblNomineeCompetition")]
    public class NomineeCompetition
    {

        [Key]
        public int NomineeCompetitionKey {get;set;}

        [ForeignKey("Nominee")]
        [Column("NomineeId")]
        public int NomineeId { get; set; }

        [Column("Nominee")]
        public Nominee? Nominee { get; set; }

        [ForeignKey("Competition")]
        [Column("CompetitionId")]
        public int CompetitionId { get; set; }

        [Column("Competition")]
        public Competition? Competition { get; set; }
    }
}
