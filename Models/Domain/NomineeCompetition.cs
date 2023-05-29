using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblNomineeCompetition")]
    public class NomineeCompetition
    {

        [Key]
        public int NomineeCompetitionKey {get;set;}

        [ForeignKey("NomineeId")]
        [Column("NomineeId")]
        public int NomineeId { get; set; }

        [Column("Nominee")]
        public Nominee? Nominee { get; set; }

        [ForeignKey("Competition_Id")]
        [Column("Competition_Id")]
        public int Competition_Id { get; set; }

        [Column("Competition")]
        public Competition? Competition { get; set; }
    }
}
