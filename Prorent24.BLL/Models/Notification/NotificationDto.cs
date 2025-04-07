using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.General;
using Prorent24.Enum.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Notification
{
    public class NotificationDto : BaseDto<int>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(IsDisplay = false)]
        public NotificationTypeEnum Type { get; set; }

        [IncludeToGrid(IsDisplay = true)]
        public string Theme { get; set; }

        [IncludeToGrid(IsDisplay = true)]
        public string Message { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public int? EntityId { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public bool IsRead { get; set; }

        [IncludeToGrid(IsDisplay = true)]
        public string IsReadIcon { get { return String.Empty; } }

       [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(IsDisplay = false)]
        public ModuleTypeEnum? ModuleType { get; set; }

        //public TaskDto Task { get; set; }

        //public int? ProjectId { get; set; }

        //public ProjectDto Project { get; set; }

        //public int? RepairId { get; set; }

        //public RepairDto Repair { get; set; }

    }
}
