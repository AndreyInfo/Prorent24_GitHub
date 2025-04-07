using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class VatTransfer
    {
        public static VatClassDto TransferToVatClassDto(this VatClassEntity entity)
        {
            VatClassDto dto = new VatClassDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate

            };

            return dto;
        }

        public static VatClassEntity TransferToVatClassEntity(this VatClassDto dto)
        {
            VatClassEntity entity = new VatClassEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }


        public static VatSchemeDto TransferToVatSchemeDto(this VatSchemeEntity entity)
        {
            VatSchemeDto dto = new VatSchemeDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                VatClassSchemeRates = entity.VatClassSchemeRates?.Select(x=>x.TransferToVatClassSchemeRateDto()).ToList()
            };

            return dto;
        }

        public static VatSchemeEntity TransferToVatSchemeEntity(this VatSchemeDto dto)
        {
            VatSchemeEntity entity = new VatSchemeEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                VatClassSchemeRates = dto.VatClassSchemeRates?.Select(x => x.TransferToVatClassSchemeRateEntity()).ToList()
            };

            return entity;
        }

        public static VatClassSchemeRateDto TransferToVatClassSchemeRateDto(this VatClassSchemeRateEntity entity)
        {
            VatClassSchemeRateDto dto = new VatClassSchemeRateDto()
            {
                Id = entity.Id,
                Type = entity.Type,
                VatClassId = entity.VatClassId,
                VatSchemeId = entity.VatSchemeId,
                VatClass = entity.VatClass?.TransferToVatClassDto(),
                //VatScheme = entity.VatScheme.TransferToVatSchemeDto(),
                Rate = entity.Rate,
                AccountingCode = entity.AccountingCode,
                EdifactCode = entity.EdifactCode,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return dto;
        }

        public static VatClassSchemeRateEntity TransferToVatClassSchemeRateEntity(this VatClassSchemeRateDto dto)
        {
            VatClassSchemeRateEntity entity = new VatClassSchemeRateEntity()
            {
                Id = dto.Id,
                Type = dto.Type,
                VatClassId = dto.VatClassId,
                VatSchemeId = dto.VatSchemeId,
                Rate = dto.Rate,
                AccountingCode = dto.AccountingCode,
                EdifactCode = dto.EdifactCode,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
