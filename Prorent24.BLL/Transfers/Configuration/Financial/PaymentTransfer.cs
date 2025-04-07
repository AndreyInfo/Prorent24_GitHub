using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class PaymentTransfer
    {
        public static PaymentMethodDto TransferToMethodDto(this PaymentMethodEntity entity)
        {
            PaymentMethodDto method = new PaymentMethodDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                AccountingCode = entity.AccountingCode,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return method;
        }

        public static PaymentMethodEntity TransferToMethodEntity(this PaymentMethodDto  method)
        {
            PaymentMethodEntity entity = new PaymentMethodEntity()
            {
                Id = method.Id,
                Name = method.Name,
                AccountingCode = method.AccountingCode,
                CreationDate = method.CreationDate,
                LastModifiedDate = method.LastModifiedDate
            };

            return entity;
        }


        public static PaymentConditionDto TransferToConditionDto(this PaymentConditionEntity entity)
        {
            PaymentConditionDto  condition = new PaymentConditionDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                AccountingCode = entity.AccountingCode,
                TermInDays = entity.TermInDays,
                TextOnInvoice = entity.TextOnInvoice,
                PaymentMethod = entity.PaymentMethod?.TransferToMethodDto(),
                PaymentMethodId = entity.PaymentMethodId,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return condition;
        }

        public static PaymentConditionEntity  TransferToConditionEntity(this PaymentConditionDto dto)
        {
            PaymentConditionEntity entity = new PaymentConditionEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                AccountingCode = dto.AccountingCode,
                TermInDays = dto.TermInDays,
                TextOnInvoice = dto.TextOnInvoice,
                //PaymentMethod = dto.PaymentMethod.TransferToMethodEntity(),
                PaymentMethodId = dto.PaymentMethodId,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }

        /// <summary>
        /// Transfer from PaymentConditionDefaultDto to SystemSettingEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns type="SystemSettingEntity"></returns>
        public static SystemSettingEntity TransferToEntity(this PaymentConditionDefaultDto dto)
        {
            SystemSettingEntity entity = new SystemSettingEntity();
            entity.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            return entity;
        }
    }
}
