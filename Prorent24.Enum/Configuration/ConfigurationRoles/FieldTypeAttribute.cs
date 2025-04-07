using System;

namespace Prorent24.Enum.Configuration.ConfigurationRoles
{
    public class FieldValueTypeAttribute : Attribute
    {
        public PermissionValueTypeEnum Permission { get; private set; }

        public string DefaultValue { get; private set; }

        public string FreelancertValue { get; private set; }

        public string PowerUserValue { get; private set; }

        public string OfficeValue { get; private set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public FieldValueTypeAttribute(PermissionValueTypeEnum selectPermission, string defaultRole = "",
            string freelancerRole = "", string powerUserRole = "", string officeRole = "")
        {
            this.Permission = selectPermission;
            this.DefaultValue = defaultRole;
            this.FreelancertValue = freelancerRole;
            this.PowerUserValue = powerUserRole;
            this.OfficeValue = officeRole;
        }
    }
}