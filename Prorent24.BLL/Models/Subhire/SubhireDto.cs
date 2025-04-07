
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    public class SubhireDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string Number { get; set; }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SubhireStatusEnum Status { get; set; }

        public int? SupplierContactId { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public string SupplierContactCompany { get { return SupplierContact?.CompanyName; } }

        public ContactDto SupplierContact { get; set; }

        public int? SupplierContactPersonId { get; set; }

        public ContactPersonDto SupplierContactPerson { get; set; }

        public int? LocationContactId { get; set; }

        public ContactDto LocationContact { get; set; }

        public int? LocationContactPersonId { get; set; }

        public ContactPersonDto LocationContactPerson { get; set; }

        #region Details
        public string AccountManagerId { get; set; }

        public CrewMemberDto AccountManager { get; set; }

        [IncludeToGrid(Order = 9, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string Reference { get; set; }

        [IncludeToGrid(Order = 10, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public decimal EquipmentCost { get; set; }

        [IncludeToGrid(Order = 11, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public decimal AdditionalCost { get; set; }

        [IncludeToGrid(Order = 12, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public decimal TotalCost { get; set; }

        [IncludeToGrid(Order = 13, ColumnGroup = ColumnGroupEnum.General)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SubhireTypeEnum Type { get; set; }

        [IncludeToGrid(Order = 14, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string Remark { get; set; }
        #endregion

        #region TimeSchedule

        [IncludeToGrid(Order = 15, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public DateTime? UsagePeriodStart { get; set; }

        [IncludeToGrid(Order = 16, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public DateTime? UsagePeriodEnd { get; set; }

        [IncludeToGrid(Order = 17, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public DateTime PlanningPeriodStart { get; set; }

        [IncludeToGrid(Order = 18, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public DateTime PlanningPeriodEnd { get; set; }

        [IncludeToGrid(Order = 19, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public DateTime? DeliveryCollectionStart { get; set; }

        [IncludeToGrid(Order = 20, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public DateTime? DeliveryCollectionEnd { get; set; }
        #endregion

        public List<NoteDto> Notes { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<FileDto> Files { get; set; }

        //public int? ProjectId { get; set; }
        //public ProjectDto Project { get; set; }

        public List<ProjectDto> Projects { get; set; }

        [IncludeToGrid]
        public string ProjectName
        {
            get
            {
                if (Projects?.Count() > 0)
                {
                    return string.Join(",", Projects?.Select(y => y.Name).ToList());
                }
                else return string.Empty;
            }
        }
    }
}
