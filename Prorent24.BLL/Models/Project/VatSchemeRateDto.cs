using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Enum.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class VatSchemeRateDto
    {
        public int VatSchemeId { get; set; }

        public VatSchemeTypeEnum Type { get; set; }

        public decimal Rate { get; set; }

        public virtual VatSchemeDto VatScheme { get; set; } 
    }
}
