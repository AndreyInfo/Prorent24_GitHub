using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.Financial;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Settings
{
    [Table("sys_project_types")]
    public class ProjectTypeEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public string Color { get; set; }

        public int PackingSlipTemplateId { get; set; }

        [ForeignKey("PackingSlipTemplateId")]
        public virtual DocumentTemplateEntity PackingSlip { get; set; }

        public int QuotationTemplateId { get; set; }

        [ForeignKey("QuotationTemplateId")]
        public virtual DocumentTemplateEntity QuotationTemplate { get; set; }

        public int ContractTemplateId { get; set; }

        [ForeignKey("ContractTemplateId")]
        public virtual DocumentTemplateEntity ContractTemplate { get; set; }

        public int InvoiceTemplateId { get; set; }

        [ForeignKey("InvoiceTemplateId")]
        public virtual DocumentTemplateEntity InvoiceTemplate { get; set; }

        public int LetterheadTemplateId { get; set; }

        [ForeignKey("LetterheadTemplateId")]
        public virtual LetterheadEntity LetterheadTemplate { get; set; }

        public int InvoiceMommentId { get; set; }

        [ForeignKey("InvoiceMommentId")]
        public virtual InvoiceMomentEntity InvoiceMoment { get; set; }

        public int DefaultAdditionalConditionId { get; set; }

        [ForeignKey("DefaultAdditionalConditionId")]
        public virtual AdditionalConditionEntity DefaultAdditionalCondition { get; set; }
    }
}
