using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Subhire
{
    public enum SubhireStatusEnum
    {
        [EnumMember(Value = "option")]
        Option = 1,
        [EnumMember(Value = "cancelled")]
        Cancelled = 2,
        [EnumMember(Value = "confirmed")]
        Confirmed = 3,
        [EnumMember(Value = "packed")]
        Packed = 4,
        [EnumMember(Value = "onlocation")]
        Onlocation = 5,
        [EnumMember(Value = "returned")]
        Returned = 6,
        [EnumMember(Value = "application")]
        Application = 7,
        [EnumMember(Value = "concept")]
        Concept = 8
    }
}
