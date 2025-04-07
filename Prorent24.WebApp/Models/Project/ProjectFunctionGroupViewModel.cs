using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectFunctionGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? PlanFrom { get; set; }

        public DateTime? PlanUntil { get; set; }


        public DateTime? UseFrom { get; set; }

        public DateTime? UseUntil { get; set; }
        

    }
}
