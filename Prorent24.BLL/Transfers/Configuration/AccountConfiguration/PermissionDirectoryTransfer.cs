using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Entity.Configuration.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.AccountConfiguration
{
    public static class PermissionDirectoryTransfer
    {
        public static PermissionDirectoryDto TransferToDto(this PermissionDirectoryEntity entity)
        {
            PermissionDirectoryDto dto = new PermissionDirectoryDto()
            {
                Id = entity.Id,
                ValueTypeId = entity.ValueTypeId,
                Children = entity.Children?.Select(x=>x.TransferToDto()).ToList(),
                //DependencePermissionId = entity.DependencePermissionId,
                //Dependence = entity.Dependence?.TransferToDto(),
                PermissionName = entity.PermissionName,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                ValueTypeName = entity.ValueTypeId.ToString(),
                ModuleTypeId = entity.ModuleType,
                ModuleTypeName = entity.ModuleType?.ToString(),
                PermissionField = entity.PermissionField,
                PermissionFieldName = entity.PermissionField?.ToString(),
                ParentPermissionId = entity.ParentPermissionId

            };

            return dto;
        }

        public static PermissionDirectoryEntity TransferToEntity(this PermissionDirectoryDto dto)
        {
            PermissionDirectoryEntity entity = new PermissionDirectoryEntity()
            {
                Id = dto.Id,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
