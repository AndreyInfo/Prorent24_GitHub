using Prorent24.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_vat_classes")]
    public class VatClassEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<VatClassSchemeRateEntity> VatClassSchemeRates { get; set; }
    }
}
