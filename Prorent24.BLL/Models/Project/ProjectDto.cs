using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string Number { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 7, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public ProjectStatusEnum Status { get; set; }

        public int? ClientContactId { get; set; }

        public virtual ContactDto ClientContact { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public string ClientName { get { return this.ClientContact?.CompanyName; } }

        public int? ClientContactPersonId { get; set; }

        public ContactPersonDto ClientContactPerson { get; set; }

        public int? LocationContactId { get; set; }

        public ContactDto LocationContact { get; set; }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public string LocationName { get { return this.LocationContact?.CompanyName; } }

        public int? LocationContactPersonId { get; set; }

        public ContactPersonDto LocationContactPerson { get; set; }

        #region Details

        [IncludeToGrid(Order = 10 ,IsDisplay = false, IsSystem = true)]
        public int? TypeId { get; set; }

        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.Detail)]
        public string TypeName { get { return this.Type?.Name; } }
        public ProjectTypeDto Type { get; set; }

        public string AccountManagerId { get; set; }

        public CrewMemberDto AccountManager { get; set; }

        [IncludeToGrid(Order = 11, KeyType = "color", ColumnGroup = ColumnGroupEnum.Detail)]
        public string Color { get; set; }

        public string Reference { get; set; }
        #endregion

        public List<ProjectTimeDto> Times { get; set; }

        [IncludeToGrid(Order = 12, IncludeType ="dateshort", ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? StartPlanPeriod { get { return this.Times?.Where(x => x.DefaultPlanPeriod)?.FirstOrDefault()?.From; } }

        [IncludeToGrid(Order = 13, IncludeType = "dateshort", ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? EndPlanPeriod { get { return this.Times?.Where(x => x.DefaultPlanPeriod)?.FirstOrDefault()?.Until; } }

        [IncludeToGrid(Order = 14, IncludeType = "dateshort", ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? StartUsePeriod { get { return this.Times?.Where(x => x.DefaultUsagePeriod)?.FirstOrDefault()?.From; } }

        [IncludeToGrid(Order = 15, IncludeType = "dateshort", ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? EndUsePeriod { get { return this.Times?.Where(x => x.DefaultUsagePeriod)?.FirstOrDefault()?.Until; } }


        public List<NoteDto> Notes { get; set; }

        public List<TagDto> Tags { get; set; }

        public List<TaskDto> Tasks { get; set; }

        public List<FileDto> Files { get; set; }

        public string Description { get; set; }

        [IncludeToGrid(Order = 16, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public bool IsPublic { get; set; }

        [IncludeToGrid(Order = 17, IsDisplay = false, IsSystem = true)]
        public List<CrewMemberShortDto> CrewMembers { get; set; }
    }
}
