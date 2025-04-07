using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Account
{
    public class PermissionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PermissionDirectoryId { get; set; }

        public string Desctription { get; set; }

        public bool Value { get; set; }

        public List<PermissionDependencyBinding> DependencyBinding { get; set; }

        public List<PermissionFunctionViewModel> FunctionPermissions { get; set; }
    }

    public class PermissionFunctionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PermissionDirectoryId { get; set; }

        public string Desctription { get; set; }

        public string Value { get; set; }

        public string EnumName { get; set; }

        public List<KeyValuePair<string, int>> Enum { get; set; }

        public List<PermissionDependencyBinding> DependencyBinding { get; set; }
    }

    public class PermissionDependencyBinding
    {
        public string Value { get; set; }
        
        public string DependencePermissionName { get; set; }

        public string DependenceValue { get; set; }

    }
}
