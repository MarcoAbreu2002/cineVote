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

        [Column("isRead")]
        public Boolean isRead {get;set;}

        [Column("Name")]
        public string? Name { get; set; }

        [Column("User")]
        public User? User { get; set; }

        [ForeignKey("userName")]
        [Column("userName")]
        public string userName { get; set; }

        [ForeignKey("SubscriptionId")]
        [Column("SubscriptionId")]
        public int SubscriptionId { get; set; }

        [Column("Subscription")]
        public Subscription? Subscription { get; set; }

        [Column("SubscriptionNotifications")]
        public ICollection<SubscriptionNotifications>? SubscriptionNotifications { get; set; }
    }
}
