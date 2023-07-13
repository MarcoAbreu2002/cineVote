using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblComments")]
    public class Comments
    {
        [Key]
        [Column("CommentsId")]
        public int CommentsId { get; set; }

        [Column("Content")]
        public string Content { get; set; }

        [ForeignKey("PostsId")]
        [Column("PostsId")]
        public int PostsId { get; set; }

        [Column("Posts")]
        public Posts Post { get; set; }


    }
}
