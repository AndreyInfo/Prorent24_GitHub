using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.AccountConfiguration
{
    public static class UserRoleTransfer
    {

        /// <summary>
        /// Transfer from List<Role> to List<UserRoleDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<UserRoleDto> TransferToListDto(this List<Role> entities)
        {
            List<UserRoleDto> list = entities.Select(x => new UserRoleDto()
            {
                Id = x.Id,
                Name = x.Name,

            }).ToList();

            return list;
        }


        public static UserRoleDto TransferToDto(this Role entity)
        {
            UserRoleDto dto = new UserRoleDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Rate = entity.Rate,
                RolePermissions = entity.RolePermissions?.Select(x => x.TransferToDto()).ToList()
            };

            return dto;
        }

        public static Role TransferToEntity(this UserRoleDto dto)
        {
            Role entity = new Role()
            {
                Id = dto.Id,
                Name = dto.Name,
                Rate = dto.Rate,
                NormalizedName = dto.Name.ToUpper(),
                RolePermissions = dto.RolePermissions?.Select(x => x.TransferToEntity()).ToList()
            };

            return entity;
        }
    }
}
