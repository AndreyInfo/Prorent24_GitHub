using Prorent24.Enum.Invoice;

namespace Prorent24.BLL.Models.General.Document
{
    public class DocumentOutputDto
    {
        public string Subject { get; set; }
        public OpenKitsAndCasesTypeEnum OpenKitsAndCases { get; set; }
        public string FileName { get; set; }
        public bool UseLetterhead { get; set; } = true;
    }
}