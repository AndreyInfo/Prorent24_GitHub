using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.General
{
    public enum ConfidentialTypeEnum
    {
        // Visible for everyone
        [EnumMember(Value = "everyone")]
        Everyone  = 1,
        // Visible to account managers only
        [EnumMember(Value = "accountManagersOnly")]
        AccountManagersOnly = 2
    }
}
