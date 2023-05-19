using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace cineVote.Models.Domain
{
    public class Person : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
