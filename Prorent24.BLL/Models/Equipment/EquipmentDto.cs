using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Equipment.Supplier;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Common.Extentions;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentDto : BaseDto<int>
    {
        Localize localize = null;
        public EquipmentDto()
        {
            localize = new Localize();
        }

        [IncludeToGrid(Order = 6, Imported = true, Required = true, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 6, Required = true, Imported = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public SupplyTypeEnum SupplyType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 7, Imported = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public SetTypeEnum SetType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 8, Imported = true, ColumnGroup = ColumnGroupEnum.General)] // , IsDisplay = false, ColumnGroup = ColumnGroupEnum.General
        public StockManagementEnum StockManagement { get; set; }

        public int? CriticalStock { get; set; }

        // IF SetType Kit OR Case
        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 9, Imported = true, ColumnGroup = ColumnGroupEnum.General)] // , IsDisplay = false, ColumnGroup = ColumnGroupEnum.General
        public QuantityModeEnum QuantityMode { get; set; }

        [IncludeToGrid(Order = 10, Required = true, Imported = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public int Quantity { get; set; }
        //================
        //[IncludeToGrid(ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int? FolderId { get; set; }

        // [IncludeToGrid(Order = 1, TreeColumn = true, TreeColumnOrder = 1, IsDisplay = true, ColumnName = "folderName", ColumnGroup = ColumnGroupEnum.General)]
        [IncludeToGrid(Order = 11, Imported = true, IsDisplay = true, ColumnName = "folderName", ColumnGroup = ColumnGroupEnum.General)] //  Required = true, Imported = true, ColumnGroup = ColumnGroupEnum.General
        public string FolderName
        {
            get
            {
                return Folder?.Name;
            }
        }
        //[IncludeToGrid(ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        //public string Folder { get; set; }

        [IncludeToGrid(Order = 12, Imported = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string LocationInWarehouse { get; set; }

        //[IncludeToGrid(IsDisplay = false, IsEntity = true, EntityKey = "FolderEntity", ColumnGroup = ColumnGroupEnum.General)]
        public FolderDto Folder { get; set; }

        [IncludeToGrid(Order = 5, Imported = true, ColumnGroup = ColumnGroupEnum.General)]
        public string Code { get; set; }

        [IncludeToGrid(Order = 13, Imported = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public bool DisplayInPlanner { get; set; }

        [IncludeToGrid(Order = 14, Imported = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string MeasuringUnit { get; set; }

        [IncludeToGrid(Order = 15, Imported = true, ColumnGroup = ColumnGroupEnum.Detail)]
        public string InternalRemark { get; set; }

        [IncludeToGrid(Order = 16, Imported = true, ColumnGroup = ColumnGroupEnum.Detail)]
        public string ExternalRemark { get; set; }

        // FINACIAL
        public int? DiscountGroupId { get; set; } // NULLABLE

        [IncludeToGrid(Order = 17)] // , ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false
        public string DiscountGroup { get; set; }

        public int? FactorGroupId { get; set; }

        [IncludeToGrid(Order = 18)] // , ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false
        public string FactorGroup { get; set; }

        [IncludeToGrid(Order = 19, Imported = true, ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false)]
        public decimal RentalPrice { get; set; }

        [IncludeToGrid(Order = 20, Imported = true, ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false)]
        public decimal SubhirePrice { get; set; }

        [IncludeToGrid(Order = 21, Imported = true, ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false)]
        public decimal NewPrice { get; set; }

        [IncludeToGrid(Order = 22, Imported = true, ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false)]
        public decimal SalePrice { get; set; }

        [IncludeToGrid(Order = 23, Imported = true, ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false)]
        public decimal PurchasePrice { get; set; }

        [IncludeToGrid(Order = 24, Imported = true, ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false)]
        public decimal MarginPrice { get; set; }


        public int? VATClassId { get; set; }

        [IncludeToGrid(Order = 25)] // , ColumnGroup = ColumnGroupEnum.Financial, IsDisplay = false
        public string VATClass { get; set; }

        // SPECIFIACTIONS
        // for Case and Item
        [IncludeToGrid(Order = 26, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false)]
        public decimal? LengthDim { get; set; } = 0; // cm

        [IncludeToGrid(Order = 27, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false)]
        public decimal? WidthDim { get; set; } = 0; // cm

        [IncludeToGrid(Order = 28, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false)]
        public decimal? HeightDim { get; set; } = 0; // cm 

        [IncludeToGrid(Order = 29, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false)]
        public decimal? WeightDim { get; set; } = 0; //kg

        [IncludeToGrid(Order = 30, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false)]
        public decimal? Volume { get; set; } = 0;// m^2

        [IncludeToGrid(Order = 31, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false)]
        public decimal? Power { get; set; } = 0; // W


        [IncludeToGrid(Order = 32, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false)]
        public decimal? Current { get; set; } = 0;// A

        [IncludeToGrid(Order = 33, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public LenghtUnitEnum? LengthUnit { get; set; }  // sm

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 34, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public LenghtUnitEnum? WidthUnit { get; set; }  // cm

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 35, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public LenghtUnitEnum? HeightUnit { get; set; }  // cm 

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 36, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public WeightUnitEnum? WeightUnit { get; set; }  //kg

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 37, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public VolumeUnitEnum? VolumeUnit { get; set; } // m^3

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 38, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public PowerUnitEnum? PowerUnit { get; set; }  // W

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 39, Imported = true, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false, KeyType = "enum")]
        public CurrentUtitEnum? CurrentUnit { get; set; } // A

        // for Item only
        [IncludeToGrid(Order = 40, ColumnGroup = ColumnGroupEnum.Specification, IsDisplay = false)]
        public bool SurfaceItem { get; set; }

        public List<NoteDto> Notes { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<FileDto> Files { get; set; }

        [IncludeToGrid(Order = 41, Imported = true, ColumnGroup = ColumnGroupEnum.Detail, IsDisplay = false)]
        public string ProducerCode { get; set; } //артикул виробника

        [IncludeToGrid(Order = 42, Imported = true, ColumnGroup = ColumnGroupEnum.Detail, IsDisplay = false)]
        public string Model { get; set; } //модель товара

        [IncludeToGrid(Order = 43, Imported = true, ColumnGroup = ColumnGroupEnum.Detail, IsDisplay = false)]
        public string Brand { get; set; } //бренд

        [IncludeToGrid(Order = 44, Imported = true, ColumnGroup = ColumnGroupEnum.Detail, IsDisplay = false)]
        public string Colour { get; set; }

        [IncludeToGrid(Order = 45, Imported = true, ColumnGroup = ColumnGroupEnum.Detail, IsDisplay = false)]
        public bool MaintenanceRequired { get; set; }

        #region ADDITIONAL FIELDS FOR GRID

        [IncludeToGrid(Order = 46, ColumnGroup = ColumnGroupEnum.General)]
        public decimal PriceEquipment
        {
            get
            {
                if (SupplyType == SupplyTypeEnum.Rental)
                {
                    return RentalPrice;
                }
                else
                {
                    return SalePrice;
                }
            }
        }

        [IncludeToGrid(Order = 47, ColumnGroup = ColumnGroupEnum.General)]
        public long AvailableQuantity
        {
            get
            {
                return Quantity;
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 50, Imported = true, ColumnGroup = ColumnGroupEnum.Detail, KeyType = "enum")]
        public TransportationType TransportationType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 51, Imported = true, ColumnGroup = ColumnGroupEnum.Detail, KeyType = "enum")]
        public KitType KitType { get; set; }

        [IncludeToGrid(Order = 52, Imported = true, ColumnGroup = ColumnGroupEnum.Detail)]
        public string Storage { get; set; }

        [IncludeToGrid(Order = 53, Imported = true, ColumnGroup = ColumnGroupEnum.Detail)]
        public string PowerConnector { get; set; }
        // public string SuppliersInfo { get; set; }

        //[IncludeToGrid(Order = 54, ColumnGroup = ColumnGroupEnum.Detail, IsDisplay = false)]
        public string SuppliersInfoJson { get; set;}
        public List<SupplierInfoDto> SuppliersInfo
        {
            get
            {
                if (this.SuppliersInfoJson != null)
                    return JsonConvert.DeserializeObject<List<SupplierInfoDto>>(this.SuppliersInfoJson);
                else
                    return null;
            }
        }
        #endregion

        #region ADDITIONAL FIELDS FRO DETAILS

        // [IncludeToGrid(Order = 55, ColumnGroup = ColumnGroupEnum.Detail, IsDisplay = false)] // 
        public decimal? VatClassSchemeRate { get; set; }

        #endregion

    }
}
