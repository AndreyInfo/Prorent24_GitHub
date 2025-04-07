using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.General;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Equipment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipments")]
    public class EquipmentEntity : BaseEntity
    {
        public string Name { get; set; }
        // Case not Sale!!
        public SupplyTypeEnum SupplyType { get; set; }
        public SetTypeEnum SetType { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool Archived { get; set; }
        // чи використати статус - New, Active, Archived, Deleted

        // if SupplyType.Sale!!
        public StockManagementEnum StockManagement { get; set; }
        public int? CriticalStock { get; set; }

        // IF SetType Kit OR Case
        public QuantityModeEnum QuantityMode { get; set; }
        public int? Quantity { get; set; }
        //================
        public int? FolderId { get; set; }
        [ForeignKey("FolderId")]
        public virtual FolderEntity Folder { get; set; }

        public string LocationInWarehouse { get; set; }
        public string Code { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool DisplayInPlanner { get; set; }
        public string MeasuringUnit { get; set; }
        public string InternalRemark { get; set; } //характеристики для документів
        public string ExternalRemark { get; set; } //характеристики для комерційних або контраків

        // FINACIAL
        public int? DiscountGroupId { get; set; } // NULLABLE
        [ForeignKey("DiscountGroupId")]
        public DiscountGroupEntity DiscountGroup { get; set; }
        public int? FactorGroupId { get; set; }
        [ForeignKey("FactorGroupId")]
        public FactorGroupEntity FactorGroup { get; set; }

        public decimal? RentalPrice { get; set; }
        public decimal? SubhirePrice { get; set; }
        public decimal? NewPrice { get; set; }
        public decimal? MarginPrice { get; set; }

        public decimal? SalePrice { get; set; }
        public decimal? PurchasePrice { get; set; }

        public int? VATClassId { get; set; }
        [ForeignKey("VATClassId")]
        public VatClassEntity VatClass { get; set; }

        // SPECIFIACTIONS
        // for Case and Item
        public decimal? Length { get; set; } = 0; // cm
        public decimal? Width { get; set; } = 0; // cm
        public decimal? Height { get; set; } = 0; // cm 
        public decimal? Weight { get; set; } = 0; //kg
        public decimal? Volume { get; set; } = 0;// m^3
        public decimal? Power { get; set; } = 0; // W
        public decimal? Current { get; set; } = 0;// A

        public LenghtUnitEnum? LengthUnit { get; set; } = 0; // sm
        public LenghtUnitEnum? WidthUnit { get; set; } = 0; // cm
        public LenghtUnitEnum? HeightUnit { get; set; } = 0; // cm 
        public WeightUnitEnum? WeightUnit { get; set; } = 0; //kg
        public VolumeUnitEnum? VolumeUnit { get; set; } = 0;// m^3
        public PowerUnitEnum? PowerUnit { get; set; } = 0; // W
        public CurrentUtitEnum? CurrentUnit { get; set; } = 0;// A

        // for Item only
        [Column(TypeName = "BOOLEAN")]
        public bool SurfaceItem { get; set; } = false;

        public virtual ICollection<EquipmentStockEntity> EquipmentStock { get; set; }

        public virtual ICollection<EquipmentContentEntity> EquipmentContents { get; set; }
        public virtual ICollection<EquipmentAlternativeEntity> EquipmentAlternatives { get; set; }
        public virtual ICollection<EquipmentAccessoryEntity> EquipmentAccessories { get; set; }
        public virtual ICollection<EquipmentPeriodicInspectionEntity> EquipmentPeriodicInspections { get; set; }
        public virtual ICollection<EquipmentSerialNumberEntity> EquipmentSerialNumbers { get; set; }

        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }

        public virtual ICollection<ProjectEquipmentEntity> ProjectEquipments { get; set; }
        
        public string ProducerCode { get; set; } //артикул виробника

        public string Model { get; set; } //модель товара

        public string Brand { get; set; } //бренд

        public string Colour { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool? MaintenanceRequired { get; set; } = false;

        public TransportationType TransportationType { get; set; }
        public KitType KitType { get; set; }
        
        public string Storage { get; set; }
        public string PowerConnector { get; set; }
        public string SuppliersInfo { get; set; }

    }
}
