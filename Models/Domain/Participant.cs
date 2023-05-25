using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cineVote.Models.DTO;

namespace cineVote.Models.Domain
{
    [Table("tblParticipant")]
    public class Participant : Nominee
    {
        [Column("Name")]
        public string? Name { get; set; }

        [Column("Gender")]
        public string? Gender { get; set; }

        [Column("Nationality")]
        public string? Nationality { get; set; }

        [Column("Age")]
        public int? Age { get; set; }

        [Column("Roles")]
        public ICollection<Role>? Roles { get; set; }
    }
}
