using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Maintenance.Inspection
{
    public class InspectionSerialNumberViewModel
    {
        public int Id { get; set; }
        public int InspectionId { get; set; }
        public int EquipmentId { get; set; }
        public string Equipment { get; set; }
        public int SerialNumberId { get; set; }
        public string SerialNumber { get; set; }
    }
}
