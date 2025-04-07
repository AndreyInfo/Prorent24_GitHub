using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using Prorent24.Enum.Maintenance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Maintenance
{
    public class InspectionDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5 , ColumnGroup = ColumnGroupEnum.General)]
        [JsonConverter(typeof(StringEnumConverter))]
        public InspectionStatusEnum Status { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime Date { get; set; }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        public string Description { get; set; }

        [IncludeToGrid(Order = 8, IsSystem = true, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int? PeriodicInspectionId { get; set; }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public string PeriodicInspectionName { get { return this.PeriodicInspection?.Name; } }

        [IncludeToGrid(Order = 10, IsSystem = true, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public PeriodicInspectionDto PeriodicInspection { get; set; }

        public List<NoteDto> Notes { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<FileDto> Files { get; set; }
    }
}
