using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Prorent24.Enum.Project
{
    public enum PlanningCrewMemberRateEnum
    {
        [EnumMember(Value = "crewRate")]
        CrewRate = 1,
        [EnumMember(Value = "priceAgreement")]
        PriceAgreement = 2
    }
}
