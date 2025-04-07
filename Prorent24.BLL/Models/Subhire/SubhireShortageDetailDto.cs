using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    public class SubhireShortageDetailDto
    {
        public ProjectDto Project { get; set; }

        public EquipmentDto Equipment { get; set; }

        public List<SubhireDto> Subhires { get; set; }
    }
}
