using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectDocumentGroupDto
    {
        public DocumentTemplateTypeEnum Type { get; set; }
        public List<ProjectDocumentDto> Data { get; set; }

        //public List<ProjectDocumentDto> Quotations { get; set; } = new List<ProjectDocumentDto>();
        //public List<ProjectDocumentDto> Contracts { get; set; } = new List<ProjectDocumentDto>();
        //public List<ProjectDocumentDto> Invoices { get; set; } = new List<ProjectDocumentDto>();
    }
}
