using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Maintenance;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.CrewMember;
using Prorent24.WebApp.Models.Equipment;
using Prorent24.WebApp.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Maintenance.Repair
{
    public class RepairViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        //public string EquipmentName { get { return this.Equipment?.Name; } }
        public virtual SelectViewModel Equipment { get; set; }

        public int? SerialNumberId { get; set; }
        //public string SerialNumberName { get { return this.SerialNumber?.SerialNumber; } }
        //public virtual EquipmentSerialNumberViewModel SerialNumber { get; set; }

        public string ReportedById { get; set; }
        public SelectViewModel ReportedBy { get; set; }
        public string AssignToId { get; set; }
        public SelectViewModel AssignTo { get; set; }

        public int? ExternalRepairId { get; set; }
        public SelectViewModel ExternalRepair { get; set; }

        public int? Quantity { get; set; } // for Kit type
        public DateTime? From { get; set; }
        public DateTime? Until { get; set; }

        public decimal Cost { get; set; } // 0 as Default

        [JsonConverter(typeof(StringEnumConverter))]
        public UsableTypeEnum Usable { get; set; }
        public string Remark { get; set; }
    }
}
