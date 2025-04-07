using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Equipment
{
    public enum TransportationType
    {
        [EnumMember(Value = "inCase")]
        InCase = 0,
        [EnumMember(Value = "withoutCase")]
        WithoutCase = 1
    }
}
