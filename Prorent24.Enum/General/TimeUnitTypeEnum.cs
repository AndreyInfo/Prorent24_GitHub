using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Prorent24.Enum.General
{
    public enum TimeUnitTypeEnum
    {
        [EnumMember(Value = "minutes")]
        Minutes = 1,
        [EnumMember(Value = "hours")]
        Hours = 2,
        [EnumMember(Value = "days")]
        Days = 3,
        [EnumMember(Value = "years")]
        Years = 4,
        [EnumMember(Value = "months")]
        Months = 5,
        [EnumMember(Value = "weeks")]
        Weeks = 6
    }
}
