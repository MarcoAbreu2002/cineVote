using System.ComponentModel.DataAnnotations;

namespace cineVote.Models.Domain
{
    public class Admin : Person
    {

        public int adminId { get; set; }
    }
}
