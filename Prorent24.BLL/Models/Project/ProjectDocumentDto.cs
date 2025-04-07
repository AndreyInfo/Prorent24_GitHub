using Prorent24.Enum.Configuration;
using System;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectDocumentDto: BaseDto<int>
    {
        public string Name { get; set; }
        public DocumentTemplateTypeEnum DocumentType { get; set; }
        public string Description { get; set; }
        public int ReferenceId { get; set; } // invoice ??
        public DateTime? GeneratedOn { get; set; }
    }
}
