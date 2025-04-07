using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Entity.Configuration.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.AccountConfiguration
{
    public static class RolePermissionTransfer
    {
        public static RolePermissionDto TransferToDto(this RolePermissionEntity entity)
        {
            RolePermissionDto dto = new RolePermissionDto()
            {
                Id = entity.Id,
                RoleId = entity.RoleId,
                PermissionDirectory = entity.PermissionDirectory?.TransferToDto(),
                PermissionDirectoryId = entity.PermissionDirectoryId,
                Value = entity.Value
            };

            return dto;
        }

        public static RolePermissionEntity TransferToEntity(this RolePermissionDto dto)
        {
            RolePermissionEntity entity = new RolePermissionEntity()
            {
                Id = dto.Id,
                RoleId = dto.RoleId,
                PermissionDirectory = dto.PermissionDirectory?.TransferToEntity(),
                PermissionDirectoryId = dto.PermissionDirectoryId,
                Value = dto.Value
            };

            return entity;
        }
    }
}
