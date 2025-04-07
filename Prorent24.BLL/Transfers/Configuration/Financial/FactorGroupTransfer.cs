using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class FactorGroupTransfer
    {
        public static FactorGroupDto TransferToFactorGroupDto(this FactorGroupEntity entity)
        {
            FactorGroupDto dto = new FactorGroupDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                IsSystem = entity.IsSystem,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                FactorGroupOptions = entity.FactorGroupOptions?.Select(x=>x.TransferToFactorGroupOptionDto()).ToList()
            };

            return dto;
        }

        public static FactorGroupEntity TransferToFactorGroupEntity(this FactorGroupDto dto)
        {
            FactorGroupEntity entity = new FactorGroupEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                IsSystem = dto.IsSystem,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
                
            };

            return entity;
        }

    }
}
