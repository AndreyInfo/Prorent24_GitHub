using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Equipment
{
    public enum QuantityModeEnum
    {
        [EnumMember(Value = "enterQuantityManually")]
        EnterQuantityManually = 0,
        [EnumMember(Value = "calculateQuantityCountingSerialNumbers")]
        CalculateQuantityCountingSerialNumbers = 1
    }
}
