using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project.ProjectEquipmentMovement
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectEquipmentMovementListViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ProjectEquipmentMovementListViewModel()
        {
            Data = new Dictionary<ProjectEquipmentMovementStatusEnum, List<ProjectGroupMovementViewModel>>();

            ProjectEquipmentMovementStatusEnum[] statuses = (ProjectEquipmentMovementStatusEnum[])System.Enum.GetValues(typeof(ProjectEquipmentMovementStatusEnum));
            foreach (ProjectEquipmentMovementStatusEnum status in statuses)
            {
                Data.Add(status, new List<ProjectGroupMovementViewModel>());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<ProjectEquipmentMovementStatusEnum, List<ProjectGroupMovementViewModel>> Data { get; set; }
    }
}
