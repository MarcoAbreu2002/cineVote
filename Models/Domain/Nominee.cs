using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblNominee")]
    public class Nominee
    {
        [Key]
        [Column("NomineeId")]
        public int NomineeId { get; set; }

        [Column("TMDBId")]

        public int TMDBId {get;set;}

        [Column("ProfilePictureURL")]
        public string? ProfilePictureURL { get; set; }

        [Column("FullName")]
        public string? FullName { get; set; }

        [Column("description")]
        public string? description { get; set; }

        [Column("CategoryNominees")]
        public ICollection<CategoryNominee>? CategoryNominees { get; set; }

        [Column("NomineeCompetitions")]
        public ICollection<NomineeCompetition>? NomineeCompetitions { get; set; }


    }
}
