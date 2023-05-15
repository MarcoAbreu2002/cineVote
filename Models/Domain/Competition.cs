using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    public class Competition 
    {
        [Key]
        public Guid Competition_Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Category")]
        [Column("Category")]
        public Guid? CategoryId { get; set; }
        public Category? CategoryEntity { get; set; }
    }
}
