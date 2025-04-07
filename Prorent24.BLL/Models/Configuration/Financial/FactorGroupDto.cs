using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class FactorGroupDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        public bool IsSystem { get; set; }

        public List<FactorGroupOptionDto> FactorGroupOptions { get; set; }
    }
}
