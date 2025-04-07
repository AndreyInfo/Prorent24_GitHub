using Prorent24.Enum.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Notification
{
    [Table("sys_notification_configurations")]
    public class NotificationBaseConfigurationEntity
    {
        [Key]
        public NotificationTypeEnum Type { get; set; }

        public bool IsWebDefault { get; set; } = true;

        public bool IsMobileDefault { get; set; } = false;

        public bool IsMailDefault { get; set; } = true;

        public bool IsActive { get; set; } = true;
        
    }
}
