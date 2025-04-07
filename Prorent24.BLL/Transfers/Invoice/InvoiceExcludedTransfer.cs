using Prorent24.BLL.Models.Invoice;
using Prorent24.Entity.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Invoice
{
    public static class InvoiceExcludedTransfer
    {
        /// <summary>
        /// Transfer from List<InvoiceExcludedEntity> to List<InvoiceExcludedDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<InvoiceExcludedDto> TransferToListDto(this List<InvoiceExcludedEntity> entities)
        {
            List<InvoiceExcludedDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InvoiceExcludedDto to InvoiceExcludedEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static InvoiceExcludedDto TransferToDto(this InvoiceExcludedEntity entity)
        {
            InvoiceExcludedDto dto = new InvoiceExcludedDto()
            {
                Id = entity.Id,
                InvoiceId = entity.InvoiceId,
                InvoiceExcludedId = entity.InvoiceExcludedId,
                InvoiceExcluded = entity.InvoiceExluded?.TransferToDto()
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InvoiceExcludedEntity to InvoiceExcludedDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InvoiceExcludedEntity TransferToEntity(this InvoiceExcludedDto dto)
        {
            InvoiceExcludedEntity entity = new InvoiceExcludedEntity()
            {
                Id = dto.Id,
                InvoiceId = dto.InvoiceId,
                InvoiceExcludedId = dto.InvoiceExcludedId
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<InvoiceExcludedDto> to List<InvoiceExcludedEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<InvoiceExcludedEntity> TransferToViewModel(this List<InvoiceExcludedDto> dto)
        {
            List<InvoiceExcludedEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
