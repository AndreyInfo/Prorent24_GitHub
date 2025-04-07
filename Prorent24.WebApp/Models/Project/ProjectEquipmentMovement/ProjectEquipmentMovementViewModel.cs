using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.Project;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.Project.ProjectEquipmentMovement
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectEquipmentMovementViewModel
    {
        public int Id { get; set; }
        
        public int GroupId { get; set; }

        public int? ProjectEquipmentId { get; set; }

        public int? EquipmentId { get; set; }
        
        public string EquipmentName { get; set; }
        
        public int? KitCaseParentEquipmentId { get; set; }

        public int SelectedQuantity { get; set; }
        
        public int TotalQuantity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SetTypeEnum EquipmentType { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectEquipmentMovementStatusEnum MovementStatus { get; set; }
        
        public List<ProjectEquipmentMovementViewModel> KitCaseEquipments { get; set; }

        public int LimitQuantity { get; set; }

        public List<string> SerialNumbers { get; set; }
    }
}
