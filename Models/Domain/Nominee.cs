using System.ComponentModel.DataAnnotations;

namespace cineVote.Models.Domain
{
    public class Nominee
    {
        [Key]
        public Guid NomineeId { get; set; }
        public string ProfilePictureURL { get; set; }

        public string FullName { get; set; }

        public string description { get; set; }

        public string category { get; set; }

    }
}
