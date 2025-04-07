using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.General
{
    public static class UserPermissionTransfer
    {
        public static UserRoleModulePermissionDto TransferToUserRolePermissionDto(this List<RolePermissionEntity> entities, ModuleTypeEnum module)
        {
            UserRoleModulePermissionDto dto = entities
                //.Where(x => (x.PermissionDirectory?.ParentPermissionId is null || x.PermissionDirectory.ParentPermissionId == 0) && x.PermissionDirectory.ModuleType == module)
                .Where(x => x.PermissionDirectory.ModuleType == module)

                .Select(x => new UserRoleModulePermissionDto()
                {
                    IsAllowed = x.Value == "True",
                    ModuleType = x.PermissionDirectory.ModuleType.Value,
                    Functions = entities.Where(y => y.PermissionDirectory?.ParentPermissionId == x.PermissionDirectoryId)
                    .Select(z => new UserRoleFunctionPermissionDto()
                    {
                        Function = (PermissionFieldEnum)z.PermissionDirectory?.PermissionField.Value,
                        ValueType = (PermissionValueTypeEnum)z.PermissionDirectory?.ValueTypeId,
                        Value = z.Value,
                        Permission = z.TransferToFunctionPermissionDto()
                    }).ToList()
                }).ToList().FirstOrDefault();
            return dto;
        }

        public static List<UserRoleFunctionPermissionDto> TransferToUserRoleFunctionPermissionDtos(this List<PermissionDirectoryEntity> entities)
        {
            List<UserRoleFunctionPermissionDto> dtos = entities.Select(x => x.TransferToUserRoleFunctionPermissionDto()).ToList();
            return dtos;
        }

        public static UserRoleFunctionPermissionDto TransferToUserRoleFunctionPermissionDto(this PermissionDirectoryEntity entity)
        {
            UserRoleFunctionPermissionDto dto = new UserRoleFunctionPermissionDto()
            {
                Function = entity.PermissionField.Value,
                ValueType = entity.ValueTypeId

            };

            return dto;
        }

        public static FunctionPermissionDto TransferToFunctionPermissionDto(this RolePermissionEntity entity) {
            FunctionPermissionDto dto = new FunctionPermissionDto();
            switch ((PermissionValueTypeEnum)entity.PermissionDirectory?.ValueTypeId) {
                case PermissionValueTypeEnum.EntityModificationPermission:
                    {
                        switch (entity.Value) {
                            case "OnlyView":
                                dto.OnlyView();
                                break;
                            case "ViewEdit":
                                dto.ViewEdit();
                                break;
                            case "ViewEditDelete":
                                dto.ViewEditDelete();
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    break;

            }
            return dto;
        }
    }
}
