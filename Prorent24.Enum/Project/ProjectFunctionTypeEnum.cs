using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Project
{
    public enum ProjectFunctionTypeEnum
    {
        [EnumMember(Value = "crew")]
        Crew = 1,
        [EnumMember(Value = "transport")]
        Transport = 2
    }
}
