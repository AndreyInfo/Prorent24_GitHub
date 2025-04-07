using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Maintenance.Inspection
{
    public class InspectionViewModel
    {
        public int Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public InspectionStatusEnum Status { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? PeriodicInspectionId { get; set; }
        //public PeriodicInspectionDto PeriodicInspection { get; set; }
    }
}
