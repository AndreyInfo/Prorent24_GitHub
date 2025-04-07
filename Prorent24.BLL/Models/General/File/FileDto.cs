using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.BLL.Models.General.File
{
    public class FileDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 6, Imported = true, Required = true, DisplayName = "null")]
        public string CheckBox { get; set; }

        [IncludeToGrid(Order = 7, Imported = true, Required = true, DisplayName = "File", ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 8, Imported = true, Required = true, ColumnGroup = ColumnGroupEnum.General)]

        public string Description { get; set; }

        public bool Confidential { get; set; }

        [IncludeToGrid(Order = 12, Imported = true, Required = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public string Path { get; set; }

        [IncludeToGrid(Order = 11, Imported = true, Required = true, ColumnGroup = ColumnGroupEnum.General)]
        public string Author { get; set; }

        public ModuleTypeEnum BelongsTo { get; set; }

        public double? Size { get; set; }

        [IncludeToGrid(Order = 9, Imported = true, Required = true, ColumnGroup = ColumnGroupEnum.General)]
        public string SizeKB
        {
            get
            {
                return Size.ToString() + " KB";
            }
        }

        public bool IsImage { get; set; }

        public string CrewMemberId { get; set; }

        public int? TaskId { get; set; }

        public int? ContactId { get; set; }

        public int? VehicleId { get; set; }

        public int? EquipmentId { get; set; }

        public int? ProjectId { get; set; }
        public int? InspectionId { get; set; }
        public int? SubhireId { get; set; }
        public int? RepairId { get; set; }

        [IncludeToGrid(Order = 10, Imported = true, Required = true, DisplayName = "DateUpload", ColumnGroup = ColumnGroupEnum.General)]
        public DateTime CreationDateUpload
        {
            get
            {
                return CreationDate;
            }
        }
    }
}
