using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Equipment
{
    public enum VolumeUnitEnum
    {
        [EnumMember(Value = "m3")]
        M3 = 0,
        [EnumMember(Value = "cm3")]
        Cm3 = 1

    }
}
