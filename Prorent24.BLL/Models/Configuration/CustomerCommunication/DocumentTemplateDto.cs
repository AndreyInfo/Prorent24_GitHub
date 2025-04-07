using Prorent24.BLL.Models.Directory;
using Prorent24.Common.Attributes;
using Prorent24.Entity.Base;
using Prorent24.Entity.Directory;
using Prorent24.Enum.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.BLL.Models.Configuration.CustomerCommunication
{
    public class DocumentTemplateDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6)]
        public string TypeName => Type.ToString();

        public DocumentTemplateTypeEnum Type { get; set; }

        public int CountryId { get; set; }

        [IncludeToGrid(Order = 7)]
        public string CountryName { get; set; }

        public int LanguageId { get; set; }

        [IncludeToGrid(Order = 8)]
        public string LanguageName { get; set; }

        public string CSS { get; set; }

        public string Html { get; set; }

        public string FooterText { get; set; }

        public string HeaderText { get; set; }

        [IncludeToGrid(Order = 9)]
        public bool IsHidden { get; set; }

        [IncludeToGrid(Order = 10)]
        public bool IsSystemTemplate { get; set; }

        public List<DocumentTemplateBlockDto> Blocks { get; set; }
    }
}
