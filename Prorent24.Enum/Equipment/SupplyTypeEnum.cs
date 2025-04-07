using System.Runtime.Serialization;

namespace Prorent24.Enum.Equipment
{
    public enum SupplyTypeEnum
    {
        [EnumMember(Value = "rental")]
        Rental = 0,
        [EnumMember(Value = "sale")]
        Sale = 1,
        [EnumMember(Value = "rentalWithSale")]
        RentalWithSale = 2
    }
}
