using Prorent24.Enum.Configuration.ConfigurationRoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.AccountConfiguration
{
    public class UserRoleFunctionPermissionDto
    {
        public PermissionFieldEnum Function { get; set; }
        public PermissionValueTypeEnum ValueType { get; set; } // перетворити в набір read|write etc
        public string Value { get; internal set; }
        public FunctionPermissionDto Permission { get; internal set; }
    }
}
