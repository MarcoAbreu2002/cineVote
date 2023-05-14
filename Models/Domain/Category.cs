using System.ComponentModel.DataAnnotations;

namespace cineVote.Models.Domain
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public string Description {get;set;}

    }
}
