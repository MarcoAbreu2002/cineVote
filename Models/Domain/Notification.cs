using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineVote.Models.Domain
{
    [Table("tblNotification")]
    public class Notification
    {
        [Key]
        [Column("notificationId")]
        public int NotificationId { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [ForeignKey("User")]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("SubscriptionNotifications")]
        public ICollection<SubscriptionNotifications>? SubscriptionNotifications { get; set; }
    }
}
