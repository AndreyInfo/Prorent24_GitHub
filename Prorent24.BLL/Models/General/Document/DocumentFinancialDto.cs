using System;

namespace Prorent24.BLL.Models.General.Document
{
    public class DocumentFinancialDto
    {
        public bool DisplayVAT { get; set; } = true;
        public int PaymentConditionId { get; set; }
        public DateTime DueDate { get; set; }
    }
}