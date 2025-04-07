using Prorent24.Entity.Base;
using Prorent24.Enum.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_vat_class_schemes_rates")]
    public class VatClassSchemeRateEntity : BaseEntity
    {
        public VatSchemeTypeEnum Type { get; set; }
      
        public int? VatClassId { get; set; }

        [ForeignKey("VatClassId")]
        public VatClassEntity VatClass { get; set; }

        public int? VatSchemeId { get; set; }

        [ForeignKey("VatSchemeId")]
        public VatSchemeEntity VatScheme { get; set; }
        
        public decimal Rate { get; set; }

        public string AccountingCode { get; set; }

        public string EdifactCode { get; set; }     
    }
}
