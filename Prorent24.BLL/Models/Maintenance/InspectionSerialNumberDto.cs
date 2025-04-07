using Prorent24.BLL.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Maintenance
{
    public class InspectionSerialNumberDto: BaseDto<int>
    {
        public int InspectionId { get; set; }
        public int EquipmentId { get; set; }
        public virtual EquipmentDto Equipment { get; set; }
        public int SerialNumberId { get; set; }
        public virtual EquipmentSerialNumberDto SerialNumber { get; set; }
    }
}
