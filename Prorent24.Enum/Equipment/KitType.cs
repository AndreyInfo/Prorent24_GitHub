using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Equipment
{
    public enum KitType
    {
        [EnumMember(Value = "kit")]
        Kit = 0,
        [EnumMember(Value = "withoutKit")]
        WithoutKit = 1
    }
}
