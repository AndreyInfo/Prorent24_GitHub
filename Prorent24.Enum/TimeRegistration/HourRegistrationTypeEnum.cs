using System.Runtime.Serialization;

namespace Prorent24.Enum.TimeRegistration
{
    public enum HourRegistrationTypeEnum
    {
        [EnumMember(Value = "worked")]
        Worked = 1,
        [EnumMember(Value = "holiday")]
        Holiday = 2,
        [EnumMember(Value = "correction")]
        Correction = 3,
        [EnumMember(Value = "sickLeave")]
        SickLeave = 4,
        [EnumMember(Value = "compensation")]
        Compensation = 5 //Compensation (Not included in totals)
    }
}
