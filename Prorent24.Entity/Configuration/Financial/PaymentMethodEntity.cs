using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_payment_methods")]
    public class PaymentMethodEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public string AccountingCode { get; set; }
    }
}
