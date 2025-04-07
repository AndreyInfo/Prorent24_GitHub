using System;

namespace Prorent24.Enum.General
{
    public class RoleManageAttribute : Attribute
    {
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsMenuItem { get; set; } = true;
        public bool IsAlwaysTrue { get; set; } = false;
        public string RequiredFunction { get; set; }
        public bool IsRoleModule { get; set; } = true;
    }
}