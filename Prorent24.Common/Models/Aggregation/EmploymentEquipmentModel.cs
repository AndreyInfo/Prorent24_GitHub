using System;

namespace Prorent24.Common.Models.Aggregation
{
    public class AvailableEquipmentModel
    {
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public int OwnEquipment { get; set; }
        public int InCaseEquipment { get; set; }
        public int InPlanningEquipment { get; set; }
        public int InRepairEquipment { get; set; }
        public int Available { get; set; }
        public int Deficiency { get; set; }
        public int RentedEquipment { get; set; }
        public int DeficitAfterSublease { get; set; }
    }
}
