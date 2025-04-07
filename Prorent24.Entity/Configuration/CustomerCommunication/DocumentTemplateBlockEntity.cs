using Prorent24.Entity.Base;
using Prorent24.Enum.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Configuration.CustomerCommunication
{
    [Table("sys_document_template_blocks")]
    public class DocumentTemplateBlockEntity : BaseEntity
    {
        public string Name { get; set; }
        public DocumentBlockTypeEnum Type { get; set; }
        public bool DisplayHeader { get; set; }
        public int Order { get; set; }
        public int DocumentTemplateId { get; set; }
        [ForeignKey("DocumentTemplateId")]
        public virtual DocumentTemplateEntity DocumentTemplate { get; set; }
        // тут попрацювати над налаштуваннями блоків
        // можливо в json але це не точно
        public string BlockConfigurationJSON { get; set; }
    }
}