using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_financials")]
    public class ProjectFinancialEntity : BaseEntity
    {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        public decimal Deposit { get; set; }

        //delete, don't use
        public ProjectFinancialDepositStatusEnum DepositStatus { get; set; }

        public int? InvoiceMomentId { get; set; }

        [ForeignKey("InvoiceMomentId")]
        public virtual InvoiceMomentEntity InvoiceMoment { get; set; }

        public int? AdditionalConditionId { get; set; }

        [ForeignKey("AdditionalConditionId")]
        public virtual AdditionalConditionEntity AdditionalCondition { get; set; }

        public string AddittionalConditionFreeText { get; set; }

        public decimal TotalIncVat { get; set; }

        public int VatSchemeId { get; set; }

        [ForeignKey("VatSchemeId")]
        public virtual VatSchemeEntity VatScheme { get; set; }


    }
}
