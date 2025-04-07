using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.General.Document
{
    public class DocumentGroupdDto
    {
        public DocumentTemplateTypeEnum Type { get; set; } // Entity
        public List<DocumentDto> Data { get; set; }
    }
}