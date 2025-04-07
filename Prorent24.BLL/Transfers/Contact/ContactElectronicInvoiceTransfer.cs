using Prorent24.BLL.Models.Contact;
using Prorent24.Entity.Contact;
using System;
using System.Collections.Generic;
using System.Text;
namespace Prorent24.BLL.Transfers.Contact
{
    public static class ContactElectronicInvoiceTransfer
    {
        /// <summary>
        /// Transfer from ContactElectronicInvoiceEntity to ContactElectronicInvoiceDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ContactElectronicInvoiceDto TransferToDto(this ContactElectronicInvoiceEntity entity)
        {
            ContactElectronicInvoiceDto dto = new ContactElectronicInvoiceDto()
            {
                ContactId = entity.ContactId,
                Id = entity.Id,
                IdentificationNumber = entity.IdentificationNumber,
                IdentificationScheme = entity.IdentificationScheme,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };
            return dto;
        }

        /// <summary>
        /// Transfer from ContactElectronicInvoiceDto to ContactElectronicInvoiceEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactElectronicInvoiceEntity TransferToEntity(this ContactElectronicInvoiceDto dto)
        {
            ContactElectronicInvoiceEntity entity = new ContactElectronicInvoiceEntity()
            {
                ContactId = dto.ContactId,
                Id = dto.Id,
                IdentificationNumber = dto.IdentificationNumber,
                IdentificationScheme = dto.IdentificationScheme,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
