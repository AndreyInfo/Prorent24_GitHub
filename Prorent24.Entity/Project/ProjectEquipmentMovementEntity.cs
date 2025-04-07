using Prorent24.Entity.Base;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.Project;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_equipment_movement")]
    public class ProjectEquipmentMovementEntity : BaseEntity
    {
        public int ProjectId { get; set; }

        public int GroupId { get; set; }
        
        public int? ProjectEquipmentId { get; set; }

        public int? EquipmentId { get; set; }

        public int? KitCaseEquipmentId { get; set; } //like ParentId

        public int SelectedQuantity { get; set; }

        public int TotalQuantity { get; set; }

        public ProjectEquipmentMovementStatusEnum MovementStatus { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        [ForeignKey("GroupId")]
        public virtual ProjectEquipmentGroupEntity ProjectEquipmentGroup { get; set; }

        [ForeignKey("ProjectEquipmentId")]
        public virtual ProjectEquipmentEntity ProjectEquipment { get; set; }

        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        
        [ForeignKey("KitCaseEquipmentId")]
        public ICollection<ProjectEquipmentMovementEntity> KitCaseEquipments { get; set; }

        public bool? IsRemoved { get; set; }

    }
}
