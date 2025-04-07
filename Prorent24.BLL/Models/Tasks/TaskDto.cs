using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.Common.Attributes;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Models.Tasks
{
    public class TaskDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public string FirstName { get; set; }

        [IncludeToGrid(Order = 6, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string CompletedBy { get; set; }

        [IncludeToGrid(Order = 7, IsDisplay = false, TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }

        //[IncludeToGrid(Order = 8, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        //New
        //public DateTime DeadLine { get; set; }
        public string DeadLine { get; set; }

        [IncludeToGrid(Order = 17, IsDisplay = true, TreeColumn = true, TreeColumnOrder = 6, ColumnGroup = ColumnGroupEnum.General)]
        //[IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? CompletedIn { get; set; }

        [IncludeToGrid(Order = 10, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public bool ExpiredTime
        {
            get
            {
                //New
                return DateTime.UtcNow > DateTime.Parse(DeadLine) && !CompletedIn.HasValue;
            }
        }

        //[IncludeToGrid(Order = 11,  ColumnGroup = ColumnGroupEnum.General)]
        public string CompletedInDateName => CompletedIn.HasValue ? CompletedIn.Value.GetDateNameWithShortDate() : null;

        [IncludeToGrid(Order = 12, IsDisplay =false, TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.General)]
        //New
        public string DeadLineGroupName => DateTime.Parse(DeadLine).GetDateNameWithShortDate();

        [IncludeToGrid(Order = 13, IsDisplay = false, TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.General)]
        public bool IsPublic { get; set; }

        [IncludeToGrid(Order = 14, TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string Description { get; set; }

        public List<CrewMemberShortDto> CrewMembers { get; set; }

        [IncludeToGrid(Order = 15, ColumnGroup = ColumnGroupEnum.General)]
        public string CrewMemberView
        {
            get
            {
                return String.Join(",,, <br>", this.CrewMembers?.Select(x => $"{x.FirstName} {x.LastName}"));
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 16, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public ModuleTypeEnum BelongsTo { get; set; }
        public string CrewMemberId { get; set; }
        public string CreationUserId { get; set; }
        public int? ContactId { get; set; }
        public int? VehicleId { get; set; }
        public int? EquipmentId { get; set; }
        public int? ProjectId { get; set; }
        public int? InspectionId { get; set; }
        public int? RepairId { get; set; }
        public int? InvoiceId { get; set; }
        public int? SubhireId { get; set; }

        public List<TagDto> Tags { get; set; }
        public List<NoteDto> Notes { get; set; }
        public List<FileDto> Files { get; set; }

        public CrewMemberDto CrewMember { get; set; }

        public virtual ContactDto Contact { get; set; }

        public VehicleDto Vehicle { get; set; }

        public EquipmentDto Equipment { get; set; }

        public ProjectDto Project { get; set; }
        public InspectionDto Inspection { get; set; }
        public RepairDto Repair { get; set; }
        public InvoiceDto Invoice { get; set; }


        //[IncludeToGrid(Order = 17, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public List<CrewMemberShortDto> Executors { get; set; }

        [IncludeToGrid(Order = 17, IsDisplay = false, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.General)]
        public string ExecutorView
        {
            get
            {
                return String.Join(",  ",this.Executors?.Select(x => $"{x.LastName} {x.FirstName}"));
            }
        }

    }
}
