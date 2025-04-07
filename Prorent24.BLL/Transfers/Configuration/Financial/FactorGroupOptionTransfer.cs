using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class FactorGroupOptionTransfer
    {
        public static FactorGroupOptionDto TransferToFactorGroupOptionDto(this FactorGroupOptionEntity entity)
        {
            FactorGroupOptionDto dto = new FactorGroupOptionDto()
            {
                Id = entity.Id,
                NumberOfDaysFrom = entity.NumberOfDaysFrom,
                NumberOfDaysTo = entity.NumberOfDaysTo,
                Factor = entity.Factor,
                FactorGroupId = entity.FactorGroupId,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return dto;
        }

        public static FactorGroupOptionEntity TransferToFactorGroupOptionEntity(this FactorGroupOptionDto dto)
        {
            FactorGroupOptionEntity entity = new FactorGroupOptionEntity()
            {
                Id = dto.Id,
                NumberOfDaysFrom = dto.NumberOfDaysFrom,
                NumberOfDaysTo = dto.NumberOfDaysTo,
                Factor = dto.Factor,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                FactorGroupId = dto.FactorGroupId,
            };

            return entity;
        }

    }
}
