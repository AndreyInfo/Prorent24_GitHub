using System.Runtime.Serialization;

namespace Prorent24.Enum.Maintenance
{
    public enum InspectionStatusEnum
    {
        [EnumMember(Value = "inspectionPending")]
        InspectionPending = 0,
        [EnumMember(Value = "processing")]
        Processing = 1,
        [EnumMember(Value = "refused")]
        Refused = 2,
        [EnumMember(Value = "approved")]
        Approved = 3
    }
}
