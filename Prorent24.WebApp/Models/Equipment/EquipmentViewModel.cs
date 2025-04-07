using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Equipment;
using Prorent24.WebApp.Models.Equipment.Supplier;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SupplyTypeEnum SupplyType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SetTypeEnum SetType { get; set; }

        // for Kit and Item Only!!  when sale
        [JsonConverter(typeof(StringEnumConverter))]
        public StockManagementEnum StockManagement { get; set; }

        public int? CriticalStock { get; set; }

        // IF SetType Kit OR Case
        [JsonConverter(typeof(StringEnumConverter))]
        public QuantityModeEnum QuantityMode { get; set; }
        public int Quantity { get; set; }
        //================
        public int? FolderId { get; set; }
        public string FolderName { get; set; }

        public string LocationInWarehouse { get; set; }
        public string Code { get; set; }

        public bool DisplayInPlanner { get; set; }
        public string MeasuringUnit { get; set; }
        public string InternalRemark { get; set; }
        public string ExternalRemark { get; set; }

        // FINACIAL
        public int? DiscountGroupId { get; set; } // NULLABLE
        // FOR ental only
        public int? FactorGroupId { get; set; }

        // if SupplyType == SupplyType.Rental
        public decimal RentalPrice { get; set; }
        public decimal SubhirePrice { get; set; }
        public decimal NewPrice { get; set; }

        // if SupplyType == SupplyType.Sale
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }

        // for both  SupplyType == SupplyType.Sale OR SupplyType == SupplyType.Sale
        public decimal MarginPrice { get; set; }


        public int? VATClassId { get; set; }
        //[ForeignKey("VATClassId")]
        //public VatClassEntity VatClass { get; set; }

        // SPECIFIACTIONS
        // for Case and Item
        public decimal? Length { get; set; } = 0; // cm
        public decimal? Width { get; set; } = 0; // cm
        public decimal? Height { get; set; } = 0; // cm 
        public decimal? Weight { get; set; } = 0; //kg
        public decimal? Volume { get; set; } = 0;// m^2
        public decimal? Power { get; set; } = 0; // W
        public decimal? Current { get; set; } = 0;// A

        public LenghtUnitEnum? LengthUnit { get; set; }// sm
        public LenghtUnitEnum? WidthUnit { get; set; }  // cm
        public LenghtUnitEnum? HeightUnit { get; set; }  // cm 
        public WeightUnitEnum? WeightUnit { get; set; }  //kg
        public VolumeUnitEnum? VolumeUnit { get; set; } // m^3
        public PowerUnitEnum? PowerUnit { get; set; }  // W
        public CurrentUtitEnum? CurrentUnit { get; set; } // A


        // for Item only
        public bool SurfaceItem { get; set; } = false;

        public string ProducerCode { get; set; } //артикул виробника

        public string Model { get; set; } //модель товара

        public string Brand { get; set; } //бренд

        public string Colour { get; set; }
        public bool MaintenanceRequired { get; set; }


        public TransportationType TransportationType { get; set; }
        public KitType KitType { get; set; }
        public string Storage { get; set; }
        public string PowerConnector { get; set; }

        public List<SupplierInfoViewModel> SuppliersInfo { get; set; }
    }

}
