using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Project
{
    public enum TimeTypeEnum
    {
        [EnumMember(Value = "minutes")]
        Minutes = 1,

        [EnumMember(Value = "hours")]
        Hours = 2,

        [EnumMember(Value = "days")]
        Days = 3
    }
}
