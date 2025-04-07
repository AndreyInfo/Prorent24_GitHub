using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Common.Attributes;
using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.Financial;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.BLL.Models.Configuration.Settings
{
    public class ProjectTypeDto: BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6)]
        public string Color { get; set; }

        public int PackingSlipTemplateId { get; set; }

        public virtual DocumentTemplateDto PackingSlip { get; set; }

        public int QuotationTemplateId { get; set; }

        public virtual DocumentTemplateDto QuotationTemplate { get; set; }

        public int ContractTemplateId { get; set; }

        public DocumentTemplateDto ContractTemplate { get; set; }

        public int InvoiceTemplateId { get; set; }

        public virtual DocumentTemplateDto InvoiceTemplate { get; set; }

        public int LetterheadTemplateId { get; set; }

        public LetterheadDto LetterheadTemplate { get; set; }

        public int InvoiceMommentId { get; set; }

        public virtual InvoiceMomentDto InvoiceMoment { get; set; }

        public int DefaultAdditionalConditionId { get; set; }

        public virtual AdditionalConditionEntity DefaultAdditionalCondition { get; set; }
    }
}
