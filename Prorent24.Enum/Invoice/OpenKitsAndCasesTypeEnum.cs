using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Invoice
{
    public enum OpenKitsAndCasesTypeEnum
    {
        [EnumMember(Value = "defaultKitOrCase")]
        DefaultKitOrCase = 0,
        [EnumMember(Value = "displayAllContent")]
        DisplayAllContent = 1,
        [EnumMember(Value = "hideContent")]
        HideContent = 2
    }
}
