using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Configuration;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.Configuration.Financials.Vat
{
    public class VatSchemeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VatSchemeTypeEnum Type { get; set; }

        public List<VatClassSchemeRateViewModel> VatClassSchemeRates { get; set; }
    }
}
