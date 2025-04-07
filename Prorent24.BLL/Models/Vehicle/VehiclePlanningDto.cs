using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Vehicle
{
    public class VehiclePlanningDto
    {
        [IncludeToGrid(Order = 1 , IsDisplay = false)]
        public int Id { get; set; }

        [IncludeToGrid(Order = 2)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 3)]
        public string FolderName { get; set; }

    }
}
