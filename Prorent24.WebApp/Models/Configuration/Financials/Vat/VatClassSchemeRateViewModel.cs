using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Configuration;

namespace Prorent24.WebApp.Models.Configuration.Financials.Vat
{
    public class VatClassSchemeRateViewModel
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VatSchemeTypeEnum Type { get; set; }

        public int? VatClassId { get; set; }

        public VatClassViewModel VatClass { get; set; }

        public int? VatSchemeId { get; set; }

        public VatSchemeViewModel VatScheme { get; set; }

        public decimal Rate { get; set; }

        public string AccountingCode { get; set; }

        public string EdifactCode { get; set; }
    }
}
