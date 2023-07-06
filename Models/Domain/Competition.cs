using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cineVote.Repositories.Abstract;

namespace cineVote.Models.Domain
{
    [Table("tblCompetition")]
    public class Competition : IObservable
    {
        private readonly List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(Competition competition, string userName, Subscription subscription)
        {
            observers[0].Update(competition, userName, subscription);
        }

        [Key]
        [Column("Competition_Id")] // Use the exact column name from the database
        public int Competition_Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("IsPublic")]
        public bool IsPublic { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [ForeignKey("CategoryId")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("AdminId")]
        [Column("AdminId")]
        public string AdminId { get; set; }

        [Column("CategoryEntity")]
        public ICollection<Category> Categories { get; set; }

        [Column("NomineeCompetitions")]
        public List<Nominee> Nominees { get; set; }

        [NotMapped]
        public List<Dictionary<string, object>> Movies { get; set; }

        [NotMapped]
        public Dictionary<Category, List<Dictionary<string, object>>> finalWinners { get; set; }
    }
}
