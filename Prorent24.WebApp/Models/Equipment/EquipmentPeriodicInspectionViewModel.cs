using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentPeriodicInspectionViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int PeriodicInspectionId { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnitTypeEnum Frequency { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
