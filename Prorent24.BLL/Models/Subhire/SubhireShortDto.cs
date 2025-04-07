using Prorent24.Enum.Subhire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    public class SubhireShortDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SupplierName { get; set; }

        public SubhireStatusEnum Status { get; set; }

        public DateTime PlanningPeriodStart { get; set; }

        public DateTime PlanningPeriodEnd { get; set; }

        public bool IsAlmostInPeriod { get; set; }
    }
}
