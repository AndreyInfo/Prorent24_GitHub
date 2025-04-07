using Prorent24.BLL.Models.Invoice;
using Prorent24.Entity.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Invoice
{
    public static class InvoiceLineTransfer
    {
        /// <summary>
        /// Transfer from List<InvoiceLineEntity> to List<InvoiceLineDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<InvoiceLineDto> TransferToListDto(this List<InvoiceLineEntity> entities)
        {
            List<InvoiceLineDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InvoiceLineDto to InvoiceLineEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static InvoiceLineDto TransferToDto(this InvoiceLineEntity entity)
        {
            InvoiceLineDto dto = new InvoiceLineDto()
            {
                Id = entity.Id,
                InvoiceId = entity.InvoiceId,
                LedgerId = entity.LedgerId,
                VatClassId = entity.VatClassId,
                Description = entity.Description,
                Amount = entity.Amount,
                Price = entity.Price
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InvoiceLineEntity to InvoiceLineDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InvoiceLineEntity TransferToEntity(this InvoiceLineDto dto)
        {
            InvoiceLineEntity entity = new InvoiceLineEntity()
            {
                Id = dto.Id,
                InvoiceId = dto.InvoiceId,
                LedgerId = dto.LedgerId,
                VatClassId = dto.VatClassId,
                Description = dto.Description,
                Amount = dto.Amount,
                Price = dto.Price
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<InvoiceLineDto> to List<InvoiceLineEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<InvoiceLineEntity> TransferToListEntityModel(this List<InvoiceLineDto> dto)
        {
            List<InvoiceLineEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
