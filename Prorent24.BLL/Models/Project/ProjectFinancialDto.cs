using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Configuration;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectFinancialDto : BaseDto<int>
    {
        public int ProjectId { get; set; }
        public decimal Deposit { get; set; }

        //delete, don't use
        //[JsonConverter(typeof(StringEnumConverter))]
        //public ProjectFinancialDepositStatusEnum DepositStatus { get; set; }

        public int? InvoiceMomentId { get; set; }
    
        public  InvoiceMomentDto InvoiceMoment { get; set; }

        public int? AdditionalConditionId { get; set; }

        public  AdditionalConditionDto AdditionalCondition { get; set; }

        public string AddittionalConditionFreeText { get; set; }
      

        public decimal TotalIncVat { get; set; }

        public int VatSchemeId { get; set; }

        public VatSchemeDto VatScheme { get; set; }

    }
}
