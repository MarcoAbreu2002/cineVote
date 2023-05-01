using System.ComponentModel.DataAnnotations;

namespace cineVote.Models
{
    public class User:Person
    {
        [Key]
        public int Id { get; set; }

        public string userName { get; set; }

    }
}
