using Prorent24.Entity.Base;
using Prorent24.Enum.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Notification
{
    [Table("dbo_notification_user_settings")]
    public class NotificationUserSettingsEntity:BaseEntity
    {
        public NotificationTypeEnum Type { get; set; }

        [ForeignKey("Type")]
        public NotificationBaseConfigurationEntity BaseConfigurationEntity { get; set; }

        public bool IsWeb { get; set; } = true;
        public bool IsMobile { get; set; } = false;
        public bool IsMail { get; set; } = true;
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
