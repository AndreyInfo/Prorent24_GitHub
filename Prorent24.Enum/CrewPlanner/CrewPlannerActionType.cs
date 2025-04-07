using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.CrewPlanner
{
    public enum CrewPlannerActionType
    {
        [Description("Mark as available")]
        [EnumMember(Value = "available")]
        Available = 1,
        [Description("Mark as unavailable")]
        [EnumMember(Value = "notAvailable")]
        NotAvailable = 2,
        [Description("Mark as unknown")]
        [EnumMember(Value = "unknown")]
        Unknown = 3,
        [Description("Mark as reserved")]
        [EnumMember(Value = "reserved")]
        Reserved = 4,
        [Description("Invite")]
        [EnumMember(Value = "invite")]
        Invite = 5,
        [Description("Make an appointment")]
        [EnumMember(Value = "appointment")]
        Appointment = 6,

    }
}
