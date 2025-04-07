using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.Contact;
using Prorent24.Entity.General;
using Prorent24.Entity.Project;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Invoice;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Invoice
{
    [Table("dbo_invoices")]
    public class InvoiceEntity : BaseEntity
    {
        //
        public InvoiceStateEnum? State { get; set; }
        public int? ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual ContactEntity Client { get; set; }
        public int? ContactPersonId { get; set; }
        [ForeignKey("ContactPersonId")]
        public virtual ContactPersonEntity ContactPerson { get; set; }
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }
        // public int OwnerId = CreateBy

        // GENERAL
        public string AccountingCode { get; set; }
        public string Name { get; set; } // Authogenerated name of Invoice
        public decimal? CreditDebit { get; set; }
        public string FileName { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? GeneratedOn { get; set; }
        // public OpenKitsAndCasesTypeEnum OpenKitsAndCases { get; set; }

        public int? PaymentConditionId { get; set; } // Default
        [ForeignKey("PaymentConditionId")]
        public virtual PaymentConditionEntity PaymentCondition { get; set; }

        public int? PaymentMethodId { get; set; } // System default
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethodEntity PaymentMethod { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool PriceCalculationBasedOnProject { get; set; }

        //public string Subject { get; set; }
        //public string Text { get; set; }
        //[Column(TypeName = "BOOLEAN")]
        //public bool UseLetterhead { get; set; }
        public int? VatSchemeId { get; set; } // VATCode
        [ForeignKey("VatSchemeId")]
        public virtual VatSchemeEntity VatScheme { get; set; }

        // FINANCIAL
        public decimal? TotalNewPrice { get; set; }

        // AMOUNTS
        public decimal? PercentagePartialInvoice { get; set; }
        public decimal? PriceExcludeVAT { get; set; }
        public decimal? PriceIncludeVAT { get; set; }
        public decimal? TotalSeparateInvoiceLines { get; set; }
        public decimal? VAT { get; set; }

        // public OpenKitsAndCasesTypeEnum? OpenKitsAndCases { get; set; }
        // DATES
        [Column(TypeName = "DATETIME")]
        public DateTime? Date { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? PaymentDate { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? SendAT { get; set; }
        // може бути декілька відправок, що робити якщо були зміни після відправки інвойсу

        // TOTALS
        public decimal? TotalPower { get; set; }
        public decimal? TotalPowerConsumption { get; set; }
        public decimal? TotalVolume { get; set; }
        public decimal? TotalWeight { get; set; }

        public virtual DocumentEntity Document { get; set; }
        public OpenKitsAndCasesTypeEnum? OpenKitsAndCases { get; set; }

        public virtual ICollection<InvoiceLineEntity> InvoiceLines { get; set; }
        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }
    }
}
