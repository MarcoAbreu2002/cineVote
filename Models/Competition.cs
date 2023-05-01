using System.ComponentModel.DataAnnotations;

namespace cineVote.Models
{
    public class Competition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string imageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Relatioships

        public List<Nominee> Nomines { get; set; }

    }
}
