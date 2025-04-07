using Prorent24.Enum.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.General.Modules
{
    public class ModuleDetailDto
    {
        public DetailsEntityEnum Entity { get; set; }
        public object Data { get; set; }
    }
}
