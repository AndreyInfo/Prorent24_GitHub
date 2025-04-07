using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class LedgerTransfer
    {
        public static LedgerDto TransferToLedgerDto(this LedgerEntity entity)
        {
            LedgerDto method = new LedgerDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                AccountingCode = entity.AccountingCode,
                IsSystem = entity.IsSystem,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate

            };

            return method;
        }

        public static LedgerEntity TransferToLedgerEntity(this LedgerDto dto)
        {
            LedgerEntity entity = new LedgerEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                AccountingCode = dto.AccountingCode,
                IsSystem = dto.IsSystem,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }

    }
}
