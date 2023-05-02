using System.ComponentModel.DataAnnotations;

namespace cineVote.Models.Domain
{
    public class User : Person
    {
        public string userName { get; set; }

    }
}
