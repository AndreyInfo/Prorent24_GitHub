using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Notification
{
    public enum NotificationGroupEnum
    {
        [EnumMember(Value = "invitations")]
        Invitations = 1,
        [EnumMember(Value = "projects")]
        Projects = 2,
        [EnumMember(Value = "financial")]
        Finacials = 3,
        [EnumMember(Value = "tasks")]
        Tasks = 4,
        [EnumMember(Value = "maintenance")]
        Maintenance = 5,
        [EnumMember(Value = "general")]
        General = 6
    }
}
