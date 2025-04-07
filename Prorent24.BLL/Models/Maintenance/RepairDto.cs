using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Maintenance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Maintenance
{
    public class RepairDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 11, ColumnGroup = ColumnGroupEnum.System, IsDisplay = false)]
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string EquipmentName { get; set; }
        public virtual EquipmentDto Equipment { get; set; }

        public int? SerialNumberId { get; set; }

        [IncludeToGrid(Order = 6, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public string SerialNumberName { get { return this.SerialNumber?.SerialNumber; } }
        public virtual EquipmentSerialNumberDto SerialNumber { get; set; }

        public string ReportedById { get; set; }
        public CrewMemberShortDto ReportedBy { get; set; }
        public string AssignToId { get; set; }
        public CrewMemberShortDto AssignTo { get; set; }

        public int? ExternalRepairId { get; set; }
        public ContactDto ExternalRepair { get; set; }

        [IncludeToGrid(Order = 12, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int? Quantity { get; set; } // for Kit type

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? From { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? Until { get; set; }

        [IncludeToGrid(Order = 13, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public decimal Cost { get; set; } // 0 as Default

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UsableTypeEnum Usable { get; set; }


        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.General)]
        public string Remark { get; set; }

        public List<NoteDto> Notes { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<FileDto> Files { get; set; }
    }
}
