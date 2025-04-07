using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Maintenance
{
    [Table("dbo_inspect_equipment_requests")]
    public class InspectEqupmentRequestEntity: BaseEntity
    {
        public string Name { get; set; }
        public RequestStatusEnum Status { get; set; }

        public int SerialNumberId { get; set; }
        [ForeignKey("SerialNumberId")]
        public EquipmentSerialNumberEntity SerialNumber { get; set; }
        public int InspectionId { get; set; }
        [ForeignKey("InspectionId")]
        public PeriodicInspectionEntity Inspection { get; set; }
        public DateTime? LastInspectedAt { get; set; }
        public DateTime InspectionDate { get; set; }

    }
}
