using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblFavorite")]
    public class Favorite
    {
        [Key]
        [Column("FavoriteId")]
        public int FavoriteId { get; set; }

        [Column("TMDBId")]

        public int TMDBId { get; set; }
        
        [ForeignKey("userName")]
        [Column("userName")]
        public string userName { get; set; }

        [Column("User")]
        public User User { get; set; }


    }
}
