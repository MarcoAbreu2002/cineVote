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
        public int SecondPlaceId { get; set; }

        [ForeignKey("ThirdPlaceId")]
        [Column("ThirdPlaceId")]
        public int ThirdPlaceId { get; set; }

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
        public CategoryNominee? FirstPlace { get; set; }

        [Column("SecondPlace")]
        public CategoryNominee? SecondPlace { get; set; }

        [Column("ThirdPlace")]
        public CategoryNominee? ThirdPlace { get; set; }
    }
}
