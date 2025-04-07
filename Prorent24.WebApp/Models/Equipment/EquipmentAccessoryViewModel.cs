using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentAccessoryViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string Equipment { get; set; }
        public int AccessoryId { get; set; }
        public string AccessoryName { get; set; }
        public int Quantity { get; set; }
        public bool Automatic { get; set; }
        public bool SkipIfAlreadyPresent { get; set; }
        public bool Free { get; set; } = false;
    }
}
