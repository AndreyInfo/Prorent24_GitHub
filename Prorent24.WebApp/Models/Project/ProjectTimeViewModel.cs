using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectTimeViewModel
    {
        public int Id { get; set; }

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
