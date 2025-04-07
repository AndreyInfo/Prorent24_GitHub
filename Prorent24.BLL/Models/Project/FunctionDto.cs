using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class FunctionDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum? GroupType { get; set; }

        [IncludeToGrid(Order = 6, IsDisplay = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum? Type { get; set; }

        [IncludeToGrid(Order = 7)]
        public string InternalName { get; set; }

        [IncludeToGrid(Order = 8)]
        public int? Quantity { get; set; }

        [IncludeToGrid(Order = 9, IsDisplay = false)]
        public bool? IncludeInPrice { get; set; }

        [IncludeToGrid(Order = 10, IsDisplay = false)]
        public bool? ShowInPlaner { get; set; }
    }
}
