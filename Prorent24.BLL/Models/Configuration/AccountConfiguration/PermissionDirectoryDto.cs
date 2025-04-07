using Prorent24.Enum.General;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using System;
using System.Collections.Generic;
using System.Text;
using Prorent24.Common.Extentions;

namespace Prorent24.BLL.Models.Configuration.AccountConfiguration
{
    public class PermissionDirectoryDto : BaseDto<int>
    {
        public string PermissionName { get; set; }

        public int? ParentPermissionId { get; set; }

        public PermissionValueTypeEnum ValueTypeId { get; set; }

        //public int? DependencePermissionId { get; set; } //activate module

        //public PermissionDirectoryDto Dependence { get; set; }

        public List<PermissionDirectoryDto> Children { get; set; }

        public string ValueTypeName { get; set; }

        public ModuleTypeEnum? ModuleTypeId { get; set; }

        public string ModuleTypeName { get; set; }

        public string ModuleTypeDescription { get { return this.ModuleTypeId?.GetAttributeValue<RoleManageAttribute, string>(x => x.Description); } } 

        public PermissionFieldEnum? PermissionField { get; set; }

        public string PermissionFieldName { get; set; }

        public string PermissionFieldDescription { get { return this.PermissionField?.GetAttributeValue<FieldValueTypeAttribute, string>(x => x.Description); } }

    }
}
