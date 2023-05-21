using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cineVote.Models.DTO;

namespace cineVote.Models.Domain
{
    [Table("tblMovie")]
    public class Movie : Nominee
    {
        [Column("Title")]
        public string? Title { get; set; }

        [Column("Genre")]
        public string? Genre { get; set; }

        [Column("Plot")]
        public string? Plot { get; set; }

        [Column("Rating")]
        public int? Rating { get; set; }

        [Column("Director")]
        public string? Director { get; set; }

        [Column("Roles")]
        public ICollection<Role>? Roles { get; set; }
    }
}
