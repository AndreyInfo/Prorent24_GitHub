using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Configuration;

namespace Prorent24.WebApp.Models.Configuration.CustomerCommunication
{
    public class DocumentTemplateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DocumentTemplateTypeEnum Type { get; set; }

        public DocumentTemplateTypeEnum TypeId { get; set; }

        public int CountryId { get; set; }

        public int Country { get; set; }

        public int LanguageId { get; set; }

        public int Language { get; set; }

        public string CSS { get; set; }

        public string Html { get; set; }

        public string FooterText { get; set; }

        public string HeaderText { get; set; }

        public bool IsHidden { get; set; }

        public bool IsSystemTemplate { get; set; }
    }
}
