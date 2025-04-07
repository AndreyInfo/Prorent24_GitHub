using Prorent24.Entity.Base;
using Prorent24.Entity.Directory;
using Prorent24.Enum.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.Settings
{
    public class ProjectTemplateDto : BaseDto<int>
    {
        public string Name { get; set; }
        public int ProgectId { get; set; }
    }
}
