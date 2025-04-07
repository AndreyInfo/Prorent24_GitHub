using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Equipment
{
    public enum SetTypeEnum
    {
        [EnumMember(Value = "item")]
        Item = 0,
        [EnumMember(Value = "kit")]
        Kit = 1,
        [EnumMember(Value = "case")]
        Case = 2
    }
}
