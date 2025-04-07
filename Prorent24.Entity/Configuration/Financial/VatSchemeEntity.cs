using Prorent24.Entity.Base;
using Prorent24.Enum.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_vat_schemes")]
    public class VatSchemeEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public VatSchemeTypeEnum Type { get; set; }

        public List<VatClassSchemeRateEntity> VatClassSchemeRates { get; set; }
    }
}
