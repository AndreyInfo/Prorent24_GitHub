using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
    public class ProjectGroupMovementViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ProjectGroupMovementViewModel()
        {
            Equipments = new List<ProjectEquipmentMovementViewModel>();
        }

        /// <summary>
        /// GroupId
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// GroupName
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectEquipmentMovementStatusEnum MovementStatus { get; set; }

        /// <summary>
        /// List Equipments
        /// </summary>
        public List<ProjectEquipmentMovementViewModel> Equipments { get; set; }
    }
}
