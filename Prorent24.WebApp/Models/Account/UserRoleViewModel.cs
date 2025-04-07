using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Account
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public decimal? Rate { get; set; }

        public List<PermissionViewModel> ModulePermissions { get; set; }
    }
}
