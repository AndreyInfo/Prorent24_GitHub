using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Configuration.ConfigurationRoles
{
    public enum BooleanSelectPermissionEnum
    {
        [EnumMember(Value = "no")]
        No = 1,
        [EnumMember(Value = "yes")]
        Yes = 2
    }
}
