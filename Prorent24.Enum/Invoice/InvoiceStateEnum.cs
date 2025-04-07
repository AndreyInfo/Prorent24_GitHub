using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Invoice
{
    public enum InvoiceStateEnum
    {
        [EnumMember(Value = "new")]
        New = 0,
        [EnumMember(Value = "opened")]
        Opened = 1,
        [EnumMember(Value = "complete")]
        Complete = 3,
        [EnumMember(Value = "rejected")]
        Rejected = 4,
        [EnumMember(Value = "deleted")]
        Deleted = 5
    }
}
