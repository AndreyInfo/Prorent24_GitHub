using Prorent24.Entity.Base;
using Prorent24.Entity.Project;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Invoice
{
    [Table("dbo_invoice_requests")]
    public class InvoiceRequestEntity : BaseEntity
    {
        public string Name { get; set; }
        public RequestStatusEnum Status { get; set; }
        public int? ProjectId { get; set; }
        public virtual ProjectEntity Project { get; set; }
        public int? InvoiceId { get; set; } // created Invoice from request
        [ForeignKey("InvoiceId")]
        public InvoiceEntity Invoice { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public DateTime InvoiceAt { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool IsIvoiceAfterConfirmation { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool IsInvoiceAfterwards { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool IsInvoiceInAdvance { get; set; }
        public int NumberOfInvoices { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? PreviouslyBilled { get; set; }
    }
}
