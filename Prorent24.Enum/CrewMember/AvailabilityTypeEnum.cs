using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Enum.CrewMember
{
    public enum AvailabilityCrewMemberTypeEnum
    {
        // Crew member can be scheduled only once simultaneously
        CanBeScheduledOnlyOnceSimultaneously = 1,
        // Can be planned multiple times at the same time (hiring company)
        CanBePlannedMultipleTimes = 2
    }
}
