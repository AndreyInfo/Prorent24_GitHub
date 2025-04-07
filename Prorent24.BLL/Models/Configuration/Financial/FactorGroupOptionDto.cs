using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class FactorGroupOptionDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public int FactorGroupId { get; set; }
        
        [IncludeToGrid(Order = 6)]
        public int NumberOfDaysFrom { get; set; }

        [IncludeToGrid(Order = 7)]
        public int NumberOfDaysTo { get; set; }

        [IncludeToGrid(Order = 8)]
        public decimal Factor { get; set; }
    }
}
