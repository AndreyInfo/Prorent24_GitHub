using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Equipment
{
    public enum LenghtUnitEnum
    {
        [EnumMember(Value = "cm")]
        Cm = 0,
        [EnumMember(Value = "m")]
        M = 1,
        [EnumMember(Value = "km")]
        Km = 2,
        [EnumMember(Value = "mm")]
        Mm = 3
    }
}
