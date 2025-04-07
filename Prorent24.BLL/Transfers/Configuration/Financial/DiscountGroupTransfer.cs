using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class DiscountGroupTransfer
    {
        public static DiscountGroupDto TransferToDiscountGroupDto(this DiscountGroupEntity entity)
        {
            DiscountGroupDto method = new DiscountGroupDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return method;
        }

        public static DiscountGroupEntity TransferToDiscountGroupEntity(this DiscountGroupDto dto)
        {
            DiscountGroupEntity entity = new DiscountGroupEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }

    }
}
