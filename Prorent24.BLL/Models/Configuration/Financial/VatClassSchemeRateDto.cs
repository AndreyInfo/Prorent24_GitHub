using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Configuration;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class VatClassSchemeRateDto : BaseDto<int>
    {
        public VatSchemeTypeEnum Type { get; set; }

        public int? VatClassId { get; set; }

        public VatClassDto VatClass { get; set; }

        public int? VatSchemeId { get; set; }

        public VatSchemeDto VatScheme { get; set; }

        public decimal Rate { get; set; }

        public string AccountingCode { get; set; }

        public string EdifactCode { get; set; }
    }
}
