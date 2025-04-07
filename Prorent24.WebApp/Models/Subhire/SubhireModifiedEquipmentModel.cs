using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Subhire
{
    public class SubhireModifiedEquipmentModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public List<ShortageEquipmentViewModel> Equipments { get; set; }
    }
}
