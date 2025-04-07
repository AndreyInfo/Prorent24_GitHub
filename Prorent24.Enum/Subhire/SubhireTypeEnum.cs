using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Subhire
{
    public enum SubhireTypeEnum
    {
        [EnumMember(Value = "collectAtSupplier")]
        CollectAtSupplier = 1,
        [EnumMember(Value = "deliveredInWarehouse")]
        DeliveredInWarehouse = 2,
        [EnumMember(Value = "deliveredOnLocation")]
        DeliveredOnLocation = 3
    }
}
