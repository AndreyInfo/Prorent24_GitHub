using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.Project;
using System.Collections.Generic;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectEquipmentMovementDto : BaseDto<int>
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int? ProjectEquipmentId { get; set; }

        public int? EquipmentId { get; set; }

        public int? KitCaseEquipmentId { get; set; }//like ParentId

        public string EquipmentName { get; set; }

        public int SelectedQuantity { get; set; }

        public int TotalQuantity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectEquipmentMovementStatusEnum MovementStatus { get; set; }

        public List<ProjectEquipmentMovementDto> KitCaseEquipments { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SetTypeEnum EquipmentType { get; set; }

        public int LimitQuantity { get; set; }

        public List<string> SerialNumbers { get; set; }
    }
}
