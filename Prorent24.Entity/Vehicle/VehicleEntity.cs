using Prorent24.Entity.Base;
using Prorent24.Entity.General;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Equipment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Vehicle
{
    [Table("dbo_vehicles")]
    public class VehicleEntity: BaseEntity
    {
        public string Name { get; set; }
        public int FolderId { get; set; }
        [ForeignKey("FolderId")]
        public virtual FolderEntity Folder { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool DeployedMultipleTimesSimultaneously { get; set; }

        // Technical information
        public string RegistrationNumber { get; set; }

        [Column(TypeName = "DECIMAL")]
        public decimal LoadCapacity { get; set; } // in KG

        public WeightUnitEnum? LoadCapacityUnit { get; set; } = 0; //kg

        [Column(TypeName = "DATE")]
        public DateTime ? MOTDate { get; set; }

        // FINACIAL
        [Column(TypeName = "DECIMAL")]
        public decimal DayilCosts { get; set; }

        [Column(TypeName = "DECIMAL")]
        public decimal VariableCosts { get; set; }

        // DETAILS
        [Column(TypeName = "BOOLEAN")]
        public bool DisplayInPlanner { get; set; }
        public string LoadingSurface { get; set; } // in ??
        [Column(TypeName = "DECIMAL")]
        public decimal Length { get; set; } // cm
        [Column(TypeName = "DECIMAL")]
        public decimal Width { get; set; } // cm

        [Column(TypeName = "DECIMAL")]
        public decimal Height { get; set; } // cm

        public LenghtUnitEnum? LengthUnit { get; set; } = 0; // sm
        public LenghtUnitEnum? WidthUnit { get; set; } = 0; // cm
        public LenghtUnitEnum? HeightUnit { get; set; } = 0; // cm 


        public int Seats { get; set; }
        
        public string Description { get; set; } // HTML

        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime? InsuranceDate { get; set; }
        public bool? IsPublic { get; set; }
        // vehicle visible for
        public virtual ICollection<VehicleVisibilityCrewMemberEntity> CrewMembers { get; set; }

    }
}
