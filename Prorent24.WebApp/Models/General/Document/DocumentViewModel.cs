using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Invoice;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;
using Prorent24.WebApp.Models.Invoice;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.General.Document
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DocumentTemplateTypeEnum DocumentType { get; set; }
        public string Description { get; set; }
        public bool Confidential { get; set; }
        public string Path { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        // public int? FileId { get; set; }
        // public virtual FileViewModel File { get; set; }
        public DateTime? GeneratedOn { get; set; } // repeat 
        public string GeneratedById { get; set; }
        // public UserDto GenerationBy { get; set; }
        // ========

        public int? TemplateId { get; set; } // беремо при створенні по замовчуванню
        // public DocumentTemplateViewModel Template { get; set; }
        public int? LetterheadId { get; set; } // беремо при створенні по замовчуванню
        // public LetterheadViewModel Letterhead { get; set; }

        public bool UseLetterhead { get; set; }
        public string Subject { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }

        public int? ProjectId { get; set; }
        public ProjectViewModel Project { get; set; }
        public int? InvoiceId { get; set; }
        public InvoiceViewModel Invoice { get; set; }

        public int? PaymentConditionId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OpenKitsAndCasesTypeEnum? OpenKitsAndCases { get; set; } = OpenKitsAndCasesTypeEnum.DisplayAllContent;
    }
}
