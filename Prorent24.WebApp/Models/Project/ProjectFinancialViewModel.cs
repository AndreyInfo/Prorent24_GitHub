using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Models.Configuration.Financials.AdditionalCondition;
using Prorent24.WebApp.Models.Configuration.Financials.Vat;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectFinancialViewModel
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public decimal Deposit { get; set; }
    
        //public List<ProjectEquipmentViewModel> RentEquipmentGroups { get; set; }

        //public List<ProjectEquipmentViewModel> SaleEquipmentGroups { get; set; }

        //public ProjectFinancialDepositStatusEnum DepositStatus { get; set; }

        public int? InvoiceMomentId { get; set; }

        public InvoiceMomentViewModel InvoiceMoment { get; set; }

        public int? AdditionalConditionId { get; set; }

        public AdditionalConditionViewModel AdditionalCondition { get; set; }

        public string AddittionalConditionFreeText { get; set; }

        public decimal TotalIncVat { get; set; }

        public int VatSchemeId { get; set; }

        public VatSchemeViewModel VatScheme { get; set; }
    }
}
