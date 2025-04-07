using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectTimeDto:BaseDto<int>
    {
        public int ProjectId { get; set; }
        
        public string Name { get; set; }
        
        public DateTime From { get; set; }
        
        public DateTime Until { get; set; }

        public bool DisplayQuotation { get; set; }

        public bool DisplayContract { get; set; }

        public bool DisplayPackSlip { get; set; }

        public bool DefaultUsagePeriod { get; set; }

        public bool DefaultPlanPeriod { get; set; }
    }
}
