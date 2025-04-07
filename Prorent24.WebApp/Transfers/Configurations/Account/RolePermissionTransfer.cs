using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.WebApp.Models.Account;
using System.Linq;

namespace Prorent24.WebApp.Transfers.Configurations.Account
{
    public static class RolePermissionTransfer
    {
        public static PermissionViewModel TransferToView(this PermissionDirectoryDto dto)
        {
            PermissionViewModel viewModel = new PermissionViewModel()
            {
                PermissionDirectoryId = dto.Id,
                Name = dto.ModuleTypeName,
                FunctionPermissions = dto.Children.Select(z => new PermissionFunctionViewModel()
                {
                    PermissionDirectoryId = z.Id,
                    Name = z.PermissionFieldName,
                    EnumName = z.ValueTypeName,
                    Enum = z.ValueTypeName?.GetEnumList(),
                    Value = z.ValueTypeName?.GetEnumList()?.FirstOrDefault().Key

                }).ToList()
            };
            return viewModel;
        }
    }
}
