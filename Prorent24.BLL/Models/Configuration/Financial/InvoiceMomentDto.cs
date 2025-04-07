using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration
{
    public class InvoiceMomentDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6)]
        public string Text { get; set; }

        [IncludeToGrid(Order = 7)]
        public decimal AfterAgreement { get; set; }

        [IncludeToGrid(Order = 8)]
        public decimal BeforeFirstDay { get; set; }

        [IncludeToGrid(Order = 9)]
        public decimal Afterwards { get; set; }
    }
}
