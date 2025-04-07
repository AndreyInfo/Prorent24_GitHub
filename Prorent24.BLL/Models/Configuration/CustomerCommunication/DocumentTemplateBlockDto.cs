using Prorent24.Enum.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.CustomerCommunication
{
    public class DocumentTemplateBlockDto: BaseDto<int>
    {
        public int DocumentTemplateId { get; set; }
        public string Name { get; set; }
        public DocumentBlockTypeEnum Type { get; set; }
        public bool DisplayHeader { get; set; }
        public int Order { get; set; }
        public string BlockConfigurationJSON { get; set; }
    }
}
