using System.Runtime.Serialization;

namespace Prorent24.Enum.Configuration
{
    public enum DocumentTemplateTypeEnum
    {
        Default = 0,

        [EnumMember(Value = "reminder")]
        Reminder = 1,

        [EnumMember(Value = "packingslip")]
        PackingSlip = 2,

        [EnumMember(Value = "contract")]
        Contract = 3,

        [EnumMember(Value = "invoice")]
        Invoice = 4,

        [EnumMember(Value = "subhireslip")]
        SubhireSlip = 5,

        [EnumMember(Value = "equipmentslip")]
        EquipmentSlip = 6,

        [EnumMember(Value = "crewmembernote")]
        CrewMemberNote = 7,

        [EnumMember(Value = "quotation")]
        Quotation = 8,

        [EnumMember(Value = "repairslip")]
        RepairSlip = 9,

        [EnumMember(Value = "vehicleslip")]
        VehicleSlip = 10
    }
}
