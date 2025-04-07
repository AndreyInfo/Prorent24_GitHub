using Prorent24.BLL.Models.Configuration;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class InvoiceMomentTransfer
    {
        public static InvoiceMomentDto TransferToInvoiceMomentDto(this InvoiceMomentEntity entity)
        {
            InvoiceMomentDto method = new InvoiceMomentDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Text = entity.Text,
                AfterAgreement = entity.AfterAgreement,
                BeforeFirstDay = entity.BeforeFirstDay,
                Afterwards = entity.Afterwards,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate

            };

            return method;
        }

        public static InvoiceMomentEntity TransferToInvoiceMomentEntity(this InvoiceMomentDto dto)
        {
            InvoiceMomentEntity entity = new InvoiceMomentEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Text = dto.Text,
                AfterAgreement = dto.AfterAgreement,
                BeforeFirstDay = dto.BeforeFirstDay,
                Afterwards = dto.Afterwards,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }

    }
}
