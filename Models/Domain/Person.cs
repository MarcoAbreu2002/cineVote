using System.ComponentModel.DataAnnotations;

namespace cineVote.Models.Domain
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
