using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Settings
{
    public class ProjectTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int PackingSlipTemplateId { get; set; }
        [Required]
        public int QuotationTemplateId { get; set; }
        [Required]
        public int ContractTemplateId { get; set; }
        [Required]
        public int InvoiceTemplateId { get; set; }
        [Required]
        public int LetterheadTemplateId { get; set; }
        [Required]
        public int InvoiceMommentId { get; set; }
        [Required]
        public int DefaultAdditionalConditionId { get; set; }
    }
}
