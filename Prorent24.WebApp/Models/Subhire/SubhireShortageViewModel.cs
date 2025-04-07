using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Subhire
{
    public class SubhireShortageViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public int PlannedQuantity { get; set; }

        public int OwnStockQuantity { get; set; }

        public int SubhiredQuantity { get; set; }

        public int ShortageQuantity { get; set; }

        public string Explanation { get; set; }

    }
}
