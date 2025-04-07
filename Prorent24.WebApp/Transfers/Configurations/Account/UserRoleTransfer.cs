using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Account;
using Prorent24.WebApp.Models.Configuration.Account;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.Configurations.Account
{
    public static class UserRoleTransfer
    {
        public static UserRoleDto TransferToDto(this UserRoleViewModel viewModel)
        {
            UserRoleDto dto = new UserRoleDto()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Rate = viewModel.Rate,
                RolePermissions = viewModel.ModulePermissions?.Select(x => new RolePermissionDto()
                {
                    Id = x.Id,
                    RoleId = viewModel.Id,
                    PermissionDirectoryId = x.PermissionDirectoryId,
                    Value = x.Value.ToString()

                }).ToList()
            };

            List<PermissionFunctionViewModel> permissionFunctionViewList = viewModel.ModulePermissions?.SelectMany(x => x.FunctionPermissions)?.ToList();
            foreach (PermissionFunctionViewModel el in permissionFunctionViewList)
            {
                dto.RolePermissions.Add(new RolePermissionDto()
                {
                    Id = el.Id,
                    RoleId = viewModel.Id,
                    PermissionDirectoryId = el.PermissionDirectoryId,
                    Value = el.Value
                });
            }
            return dto;
        }

        public static UserRoleViewModel TransferToView(this UserRoleDto dto)
        {
            UserRoleViewModel viewModel = new UserRoleViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Rate = dto.Rate,
                ModulePermissions = dto.RolePermissions?.Where(x => x.PermissionDirectory?.ParentPermissionId is null || x.PermissionDirectory.ParentPermissionId == 0)
                .Select(x => new PermissionViewModel()
                {
                    Id = x.Id,
                    PermissionDirectoryId = x.PermissionDirectoryId,
                    Name = x.PermissionDirectory?.ModuleTypeName,
                    Desctription = x.PermissionDirectory?.ModuleTypeDescription,
                    Value = x.Value == true.ToString(),
                    DependencyBinding = x.PermissionDirectory?.ModuleTypeId?.GetModuleDependency(),
                    FunctionPermissions = dto.RolePermissions.Where(y => y.PermissionDirectory?.ParentPermissionId == x.PermissionDirectoryId)
                    .Select(z => new PermissionFunctionViewModel()
                    {
                        Id = z.Id,
                        PermissionDirectoryId = z.PermissionDirectoryId,
                        Name = z.PermissionDirectory?.PermissionFieldName,
                        Desctription = z.PermissionDirectory?.PermissionFieldDescription,
                        Value = z.Value,
                        EnumName = z.PermissionDirectory?.ValueTypeName,
                        Enum = z.PermissionDirectory?.ValueTypeName.GetEnumList(),
                        DependencyBinding = z.PermissionDirectory?.PermissionField?.GetFunctionDependency(),
                    }).ToList()
                }).ToList()
            };
            return viewModel;
        }

        public static List<RolePermissionViewModel> TransferToPermissionView(this UserRoleDto dto)
        {
            var prepared = dto.TransferToView();

            prepared.ModulePermissions.Where(x => x.Value);

            return null;
        }

        private static List<PermissionDependencyBinding> GetModuleDependency(this ModuleTypeEnum module)
        {
            List<PermissionDependencyBinding> dependency = new List<PermissionDependencyBinding>();
            switch (module)
            {
                case ModuleTypeEnum.CrewPlanner:
                    {
                        PermissionDependencyBinding val = new PermissionDependencyBinding()
                        {
                            Value = true.ToString(),
                            DependencePermissionName = nameof(ModuleTypeEnum.Projects),
                            DependenceValue = true.ToString()
                        };
                        dependency.Add(val);
                        return dependency;
                    }
                default:
                    {
                        return null;
                    }
            }
           
        }

        private static List<PermissionDependencyBinding> GetFunctionDependency(this PermissionFieldEnum function)
        {
            List<PermissionDependencyBinding> dependency = new List<PermissionDependencyBinding>();
            switch (function)
            {
                case PermissionFieldEnum.CreateInvoiceProject:
                    {
                        PermissionDependencyBinding val = new PermissionDependencyBinding()
                        {
                            Value = nameof(BooleanSelectPermissionEnum.Yes),
                            DependencePermissionName = nameof(ModuleTypeEnum.Invoices),
                            DependenceValue = true.ToString()
                        };
                        dependency.Add(val);
                        return dependency;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
