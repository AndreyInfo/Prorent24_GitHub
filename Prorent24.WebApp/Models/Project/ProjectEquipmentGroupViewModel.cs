using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectEquipmentGroupViewModel
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime? StartPlanPeriod { get; set; }

        public DateTime? EndPlanPeriod { get; set; }

        public DateTime? StartUsePeriod { get; set; }

        public DateTime? EndUsePeriod { get; set; }
        
        public virtual ICollection<ProjectEquipmentViewModel> Equipments { get; set; }
    }
}
