using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.Enum.CrewPlanner;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.CrewPlanner
{
    public class CrewPlannerDto : BaseDto<int>
    {
        //public string CrewMemberId { get; set; }
        
        //public virtual CrewMemberDto CrewMember { get; set; }

        //public int? VehicleId { get; set; }

        //public virtual VehicleDto Vehicle { get; set; }

        public List<string> FunctionIds { get; set; }

        public ProjectFunctionTypeEnum Type { get; set; }


        public CrewPlannerActionType Action { get; set; }
        
        public DateTime From { get; set; }
        
        public DateTime Until { get; set; }
        public string Comment { get; set; }
    }
}
