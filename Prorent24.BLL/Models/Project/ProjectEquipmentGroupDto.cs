using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectEquipmentGroupDto:BaseDto<int>
    {
        public int ProjectId { get; set; }
        
        public string Name { get; set; }

        public DateTime? StartPlanPeriod { get; set; }
        
        public DateTime? EndPlanPeriod { get; set; }
        
        public DateTime? StartUsePeriod { get; set; }
        
        public DateTime? EndUsePeriod { get; set; }

        public ICollection<ProjectEquipmentDto> Equipments { get; set; }
    }
}
