using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cineVote.Models.DTO;

namespace cineVote.Models.Domain
{
    [Table("tblRole")]
    public class Role
    {
        [Key]
        [Column("RoleID")]
        public int RoleId { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("Description")]
        public string? description { get; set; }

        [Column("Participants")]
        public List<Participant>? Participants { get; set; }

        [ForeignKey("Movie")]
        [Column("MovieId")]
        public int MovieId {get;set;}

        [Column("Movies")]
        public Movie? Movie { get; set; }
    }
}
