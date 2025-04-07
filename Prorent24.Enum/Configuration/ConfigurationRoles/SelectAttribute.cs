using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Enum.Configuration.ConfigurationRoles
{
    public class SelectAttribute : Attribute
    {
        public bool isDefaultValue { get; private set; }

        public SelectAttribute(bool isDefaultValue)
        {
            this.isDefaultValue = isDefaultValue;
        }
    }
}
