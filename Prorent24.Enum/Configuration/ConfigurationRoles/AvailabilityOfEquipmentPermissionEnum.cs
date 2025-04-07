using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Enum.Configuration.ConfigurationRoles
{
    public enum AvailabilityOfEquipmentPermissionEnum
    {
        [SelectAttribute(isDefaultValue : true)]
        NoPermissions = 1,
        AllQuantities = 2,
        ProjectsAndSubhire = 3
    }
}
