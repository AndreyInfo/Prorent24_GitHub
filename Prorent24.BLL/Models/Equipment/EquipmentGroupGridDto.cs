using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
   public class EquipmentGroupGridDto
    {
        [IncludeToGrid(Order = 1, IsDisplay = false, IsKey = true)]
        public int Id { get; set; }

        [IncludeToGrid(Order = 2, IsDisplay = true)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 3, IsDisplay = true)]
        public string Category { get; set; }
    }
}
