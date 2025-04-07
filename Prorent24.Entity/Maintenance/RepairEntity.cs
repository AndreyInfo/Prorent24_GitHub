using Prorent24.Entity.Base;
using Prorent24.Entity.Contact;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.General;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Maintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Maintenance
{
    [Table("dbo_repairs")]
    public class RepairEntity : BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }

        public int? SerialNumberId { get; set; }
        [ForeignKey("SerialNumberId")]
        public virtual EquipmentSerialNumberEntity SerialNumber { get; set; }

        public string ReportedById { get; set; }
        [ForeignKey("ReportedById")]
        public User ReportedBy { get; set; }
        public string AssignToId { get; set; }
        [ForeignKey("AssignToId")]
        public User AssignTo { get; set; }

        public int? ExternalRepairId { get; set; }
        [ForeignKey("ExternalRepairId")]
        public ContactEntity ExternalRepair { get; set; }

        public int? Quantity { get; set; } // for Kit type

        [Column(TypeName = "DATETIME")]
        public DateTime? From { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? Until { get; set; }

        public decimal Cost { get; set; } // 0 as Default

        public UsableTypeEnum Usable { get; set; }

        public string Remark { get; set; }

        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }
    }
}
