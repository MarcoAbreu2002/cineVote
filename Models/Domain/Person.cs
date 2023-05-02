using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace cineVote.Models.Domain
{
    public class Person : IdentityUser
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
