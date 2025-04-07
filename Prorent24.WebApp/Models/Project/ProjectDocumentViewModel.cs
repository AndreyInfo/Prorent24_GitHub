using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectDocumentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DocumentTemplateTypeEnum DocumentType { get; set; }
        public string Description { get; set; }
        // public int ReferenceId { get; set; }
        public DateTime? GeneratedOn { get; set; }
        public DateTime? Date { get; set; }
    }
}
