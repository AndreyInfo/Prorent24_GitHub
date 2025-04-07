using System.Runtime.Serialization;

namespace Prorent24.Enum.Equipment
{
    public enum StockManagementEnum
    {
        [EnumMember(Value = "excludeFromStockTracking")]
        ExcludeFromStockTracking = 0,
        [EnumMember(Value = "trackStock")]
        TrackStock = 1
    }
}
