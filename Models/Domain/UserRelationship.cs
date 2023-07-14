using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblUserRelationship")]
    public class UserRelationship
    {
        [Key]
        [Column("UserRelationshipId")]
        public int UserRelationshipId { get; set; }
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }

        public Person Follower { get; set; }
        public Person Followee { get; set; }
    }
}
