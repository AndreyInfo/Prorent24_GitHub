using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Vehicle
{
    public class VehicleDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, Imported = true, Required = true, IsDisplay = true, ColumnName = "mark", TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }

        public int FolderId { get; set; }

        [IncludeToGrid(Order = 6, Imported = true, IsDisplay = true, ColumnName = "folderName", TreeColumn = true, TreeColumnOrder = 2, Required = true, ColumnGroup = ColumnGroupEnum.General)]
        public string FolderName
        {
            get
            {
                return Folder?.Name;
            }
        }

        public FolderDto Folder { get; set; }

        public bool DeployedMultipleTimesSimultaneously { get; set; }

        // Technical information

        [IncludeToGrid(Order = 7, Imported = true, IsDisplay = true, ColumnName = "registersign", TreeColumn = true, TreeColumnOrder = 2, Required = true, ColumnGroup = ColumnGroupEnum.General)]
        public string RegistrationNumber { get; set; }

        [IncludeToGrid(Order = 7, Imported = true, IsDisplay = true, TreeColumn = true, TreeColumnOrder = 1, Required = true, ColumnGroup = ColumnGroupEnum.Detail)]
        public decimal LoadCapacity { get; set; } // in KG

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 8, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType ="enum")]
        public WeightUnitEnum? LoadCapacityUnit { get; set; }  //kg

        //New
        [IncludeToGrid(Order = 9, Imported = true, IsDisplay = true, ColumnName = "dateTO", TreeColumn = true, TreeColumnOrder = 3, IncludeType = "dateshort", ColumnGroup = ColumnGroupEnum.General)]
        //public DateTime ? MOTDate { get; set; }
        public string MOTDate { get; set; }

        // FINACIAL
        [IncludeToGrid(Order = 10, Imported = true, IsDisplay = true, ColumnGroup = ColumnGroupEnum.Financial)]
        public decimal DayilCosts { get; set; }

        [IncludeToGrid(Order = 11, Imported = true, IsDisplay = true, ColumnGroup = ColumnGroupEnum.Financial)]
        public decimal VariableCosts { get; set; }

        // DETAILS
        [IncludeToGrid(Order = 12, IsDisplay = true, ColumnGroup = ColumnGroupEnum.Detail)]
        public bool DisplayInPlanner { get; set; }

        [IncludeToGrid(Order = 13, Imported = true, IsDisplay = true, ColumnName = "bodycapacity", TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.Detail)]
        public string LoadingSurface { get; set; } // in ??

        [IncludeToGrid(Order = 14, Imported = true, IsDisplay = true, TreeColumn = true, TreeColumnOrder = 6, ColumnGroup = ColumnGroupEnum.Detail)]
        public decimal LengthDim { get; set; } // cm

        [IncludeToGrid(Order = 15, Imported = true,IsDisplay = true, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.Detail)]
        public decimal WidthDim { get; set; } // cm

        [IncludeToGrid(Order = 16, Imported = true, IsDisplay = true, TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.Detail)]
        public decimal HeightDim { get; set; } // cm

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 17, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public LenghtUnitEnum? LengthUnit { get; set; }  // sm

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 18, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public LenghtUnitEnum? WidthUnit { get; set; }  // cm

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 19, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public LenghtUnitEnum? HeightUnit { get; set; }  // cm 

        [IncludeToGrid(Order = 19, IsDisplay = true, ColumnName = "numberofpassengerseats", TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.Detail)]
        public int Seats { get; set; }

        [IncludeToGrid(Order = 20, IsDisplay = true, TreeColumn = true, TreeColumnOrder = 7, ColumnGroup = ColumnGroupEnum.Detail)]
        public string Description { get; set; } // HTML

        public NoteDto Note { get; set; }


        public List<NoteDto> Notes { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<FileDto> Files { get; set; }
       
        //New
        [IncludeToGrid(Order = 20, IsDisplay = true, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.General)]
        //public DateTime? InsuranceDate { get; set; }
        public string InsuranceDate { get; set; }


        [IncludeToGrid(IsDisplay = false)]
        public bool IsPublic { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public List<CrewMemberShortDto> CrewMembers { get; set; }
    }
}
