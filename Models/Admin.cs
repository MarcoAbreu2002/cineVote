using System.ComponentModel.DataAnnotations;

namespace cineVote.Models
{
    public class Admin:Person
    {
        [Key]
        public int adminId { get; set; }
    }
}
