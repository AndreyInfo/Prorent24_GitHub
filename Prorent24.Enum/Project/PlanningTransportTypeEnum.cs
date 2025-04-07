using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Project
{
    public enum PlanningTransportTypeEnum
    {
        [EnumMember(Value = "noTransport")]
        NoTransport = 1,
        [EnumMember(Value = "roundTrip")]
        RoundTrip = 2,
        [EnumMember(Value = "onlyWayThere")]
        OnlyWayThere = 3,
        [EnumMember(Value = "onlyWayBack")]
        OnlyWayBack = 4
    }
}
