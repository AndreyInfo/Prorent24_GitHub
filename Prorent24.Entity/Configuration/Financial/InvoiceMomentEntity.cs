using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_invoice_moments")]
    public class InvoiceMomentEntity: BaseEntity
    {
        public string Name { get; set; }

        public string Text { get; set; }
         
        public decimal AfterAgreement { get; set; }

        public decimal BeforeFirstDay { get; set; }

        public decimal Afterwards { get; set; }
    }
}
