using MediatR;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Models.Notification;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Enum.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Handler
{
    public class NotificationHandlerModel :  INotification
    {
        public string UserId { get; set; } //del

        public List<string> UserIds { get; set; }

        public NotificationTypeEnum Type { get; set; }

        public string Theme { get; set; }

        public string Message { get; set; }

        public int? EntityId { get; set; }

    }
}
