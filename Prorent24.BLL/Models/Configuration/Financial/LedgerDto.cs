using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class LedgerDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6)]
        public string AccountingCode { get; set; }

        [IncludeToGrid(Order = 7)]
        public bool IsSystem { get; set; }
    }
}
