using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using cineVote.Models.DTO;

namespace cineVote.Models.Domain
{
    [Table("tblResult")]
    public class Result
    {
        [Key]
        [Column("ResultID")]
        public int ResultId { get; set; }

        [ForeignKey("FirstPlaceId")]
        [Column("FirstPlaceId")]
        public int FirstPlaceId { get; set; }

        [ForeignKey("SecondPlaceId")]
        [Column("SecondPlaceId")]
        public int? SecondPlaceId { get; set; } // Nullable int

        [ForeignKey("ThirdPlaceId")]
        [Column("ThirdPlaceId")]
        public int? ThirdPlaceId { get; set; } // Nullable int

        [ForeignKey("CategoryId")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("Competition_Id")]
        [Column("Competition_Id")]
        public int Competition_Id { get; set; }

        [Column("Competition")]
        public Competition? Competition { get; set; }

        [Column("TotalParticipants")]
        public int TotalParticipants { get; set; }

        [Column("FirstPlace")]
        public Nominee? FirstPlace { get; set; }

        [Column("SecondPlace")]
        public Nominee? SecondPlace { get; set; } // Nullable Nominee

        [Column("ThirdPlace")]
        public Nominee? ThirdPlace { get; set; } // Nullable Nominee
    }
}
