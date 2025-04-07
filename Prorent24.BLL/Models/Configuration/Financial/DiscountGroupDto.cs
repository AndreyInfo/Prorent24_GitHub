using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class DiscountGroupDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }
    }
}
