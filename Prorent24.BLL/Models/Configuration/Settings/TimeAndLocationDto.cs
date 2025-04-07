using Prorent24.BLL.Models.General.Location;
using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.Settings
{
    public class TimeAndLocationDto : LocationDto
    {
        public int TimeZone { get; set; }

    }
}
