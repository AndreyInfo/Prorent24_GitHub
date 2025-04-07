using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.General.Document
{
    public class DocumentStructureDto
    {
        public DocumentTemplateTypeEnum Type { get; set; } = DocumentTemplateTypeEnum.Default;
        public DocumentLayoutDto Layout { get; set; } = new DocumentLayoutDto();
        public DocumentVersionDto Version { get; set; } = new DocumentVersionDto();
        public DocumentOutputDto Output { get; set; } = new DocumentOutputDto();
        public DocumentFinancialDto Financial { get; set; } = new DocumentFinancialDto();
        public DocumentLinkedItemDto LinkedItem { get; set; }
    }
}
