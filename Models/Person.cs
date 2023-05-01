using System.ComponentModel.DataAnnotations;

namespace cineVote.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string password { get; set; }
        public EmailAddressAttribute EmailAddress { get; set; }
    }
}
