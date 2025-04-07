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
    public class ProjectDocumentGroupViewModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DocumentTemplateTypeEnum Type { get; set; }
        public List<ProjectDocumentViewModel> Data { get; set; }
    }
}
