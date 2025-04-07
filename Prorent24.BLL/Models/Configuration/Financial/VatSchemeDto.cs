using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Configuration;
using System.Collections.Generic;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class VatSchemeDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }
        public VatSchemeTypeEnum Type { get; set; }
        public List<VatClassSchemeRateDto> VatClassSchemeRates { get; set; }
    }

}
