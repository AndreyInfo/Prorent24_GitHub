using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Subhire
{
    public class SubhireShortViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SupplierName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SubhireStatusEnum Status { get; set; }

        public DateTime PlanningPeriodStart { get; set; }
        
        public DateTime PlanningPeriodEnd { get; set; }

        public bool IsAlmostInPeriod { get; set; } 
    }
}
