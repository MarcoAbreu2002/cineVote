using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cineVote.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace cineVote.Models.Domain
{
    [Table("tblPerson")]
    public class Person : IdentityUser
    {
        [Key]
        [Column("PersonId")]
        public int PersonId { get; set; }

        [Required]
        [Column("FirstName")]
        public string? FirstName { get; set; }

        [Required]
        [Column("LastName")]
        public string? LastName { get; set; }

        [Required]
        [Column("Password")]
        public string? Password { get; set; }

        [Required]
        [Column("Email")]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required]
        [Column("IsAdmin")]
        public bool IsAdmin { get; set; }


        [Required]
        [Column("imageUrl")]
        public byte[] imageUrl { get; set; }

        [NotMapped]
        public IFormFile newImageUrl {get; set;}

    }
}
