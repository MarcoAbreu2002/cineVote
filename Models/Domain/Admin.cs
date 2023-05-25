using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cineVote.Models.DTO;

namespace cineVote.Models.Domain
{
    [Table("tblAdmin")]
    public class Admin : Person
    {
        [ForeignKey("Competition_Id")]
        [Column("Competition_Id")]
        public int Competition_Id { get; set; }

        [Column("competitions")]
        public ICollection<Competition>? competitions { get; set; }
    }
}
