using Prorent24.Entity.Base;
using Prorent24.Entity.Maintenance;
using Prorent24.Entity.Project;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Notification
{
    [Table("dbo_notifications")]
    public class NotificationEntity : BaseEntity
    {
        public NotificationTypeEnum Type { get; set; }

        public string Theme { get; set; }

        public string Message { get; set; }

        public int? TaskId { get; set; }

        [ForeignKey("TaskId")]
        public TaskEntity Task { get; set; }

        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public ProjectEntity Project { get; set; }

        public int? RepairId { get; set; }
        [ForeignKey("RepairId")]
        public RepairEntity Repair { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public bool? IsRead { get; set; }
    }
}
