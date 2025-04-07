using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Invoice;
using Prorent24.Entity.Project;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using Prorent24.Enum.Invoice;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.General
{
    [Table("dbo_documents")]
    public class DocumentEntity : BaseEntity
    {
        public string Name { get; set; }

        public DocumentTemplateTypeEnum DocumentType { get; set; } 

        public string Description { get; set; }

        public bool Confidential { get; set; }

        public string Path { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public OpenKitsAndCasesTypeEnum? OpenKitsAndCases { get; set; }


        // фізично існуючий файл може бути згенерований в будь який момент - фіксуємо цей момент
        public int? FileId { get; set; }
        [ForeignKey("FileId")]
        public virtual FileEntity File { get; set; }

        public DateTime? GeneratedOn { get; set; } // repeat 
        public string GeneratedById { get; set; }
        [ForeignKey("GeneratedById")]
        public virtual User GenerationBy { get; set; }
        // ========

        public int? TemplateId { get; set; } // беремо при створенні по замовчуванню
        [ForeignKey("TemplateId")]
        public virtual DocumentTemplateEntity Template { get; set; }
        public int? LetterheadId { get; set; } // беремо при створенні по замовчуванню
        [ForeignKey("LetterheadId")]
        public virtual LetterheadEntity Letterhead { get; set; }

        public ModuleTypeEnum? BelongsTo { get; set; }

        public bool UseLetterhead { get; set; }
        public string Subject { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }

        //public string CrewMemberId { get; set; }
        //[ForeignKey("CrewMemberId")]
        //public virtual CrewMemberEntity CrewMembers { get; set; }

        //public int? TaskId { get; set; }
        //[ForeignKey("TaskId")]
        //public virtual TaskEntity Task { get; set; }

        //public int? ContactId { get; set; }
        //[ForeignKey("ContactId")]
        //public virtual ContactEntity Contact { get; set; }

        //public int? VehicleId { get; set; }
        //[ForeignKey("VehicleId")]
        //public virtual VehicleEntity Vehicle { get; set; }

        //public int? EquipmentId { get; set; }
        //[ForeignKey("EquipmentId")]
        //public virtual EquipmentEntity Equipment { get; set; }

        //public int? InspectionId { get; set; }
        //[ForeignKey("InspectionId")]
        //public virtual InspectionEntity Inspection { get; set; }

        //public int? RepairId { get; set; }
        //[ForeignKey("RepairId")]
        //public virtual RepairEntity Repair { get; set; }

        //public int? SubhireId { get; set; }
        //[ForeignKey("SubhireId")]
        //public virtual SubhireEntity Subhire { get; set; }

        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        public int? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual InvoiceEntity Invoice { get; set; }
    }
}
