using Prorent24.Enum.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.CustomerCommunication
{
    public class DocumentTemplateBlockViewModel
    {
        public int Id { get; set; }
        public int DocumentTemplateId { get; set; }
        public string Name { get; set; }
        public DocumentBlockTypeEnum Type { get; set; }
        public bool DisplayHeader { get; set; }
        public int Order { get; set; }
        // public string BlockConfigurationJSON { get; set; }
    }
}
