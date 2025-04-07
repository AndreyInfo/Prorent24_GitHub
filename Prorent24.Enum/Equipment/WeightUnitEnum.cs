using System.Runtime.Serialization;

namespace Prorent24.Enum.Equipment
{
    public enum WeightUnitEnum
    {
        [EnumMember(Value = "kg")]
        Kg = 0,
        [EnumMember(Value = "g")]
        G = 1,
        [EnumMember(Value = "ton")]
        Ton = 2
    }
}
