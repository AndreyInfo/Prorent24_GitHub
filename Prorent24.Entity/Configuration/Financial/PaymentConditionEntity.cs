using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_payment_conditions")]
    public class PaymentConditionEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public string TextOnInvoice { get; set; }

        public string AccountingCode { get; set; }

        public int TermInDays { get; set; }

        public int? PaymentMethodId { get; set; }

        public PaymentMethodEntity PaymentMethod { get; set; }
    }
}
