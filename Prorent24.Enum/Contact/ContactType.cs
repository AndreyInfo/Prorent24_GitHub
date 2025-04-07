using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Contact
{
    public enum ContactType
    {
        [EnumMember(Value = "company")]
        Company = 1,
        [EnumMember(Value = "privateIndividual")]
        PrivateIndividual = 2
    }
    
}
