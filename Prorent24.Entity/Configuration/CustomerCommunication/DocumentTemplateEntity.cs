using Prorent24.Entity.Base;
using Prorent24.Entity.Directory;
using Prorent24.Enum.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.CustomerCommunication
{
    [Table("sys_document_templates")]
    public class DocumentTemplateEntity: BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public DocumentTemplateTypeEnum Type { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual DirectoryEntity Country { get; set; }

        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual DirectoryEntity Language { get; set; }

        public string CSS { get; set; }

        public string Html { get; set; }

        public string FooterText { get; set; }

        public string HeaderText { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool IsHidden { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool IsSystemTemplate { get; set; }

        public virtual ICollection<DocumentTemplateBlockEntity> Blocks { get; set; }
    }
}
