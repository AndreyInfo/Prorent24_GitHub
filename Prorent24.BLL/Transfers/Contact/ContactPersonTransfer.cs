using Prorent24.BLL.Models.Contact;
using Prorent24.Entity.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.ContactPerson
{
    public static class ContactPersonTransfer
    {
        /// <summary>
        /// Transfer from List<ContactPersonEntity> to List<ContactPersonDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ContactPersonDto> TransferToListDto(this List<ContactPersonEntity> entities)
        {
            List<ContactPersonDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ContactPersonEntity to ContactPersonDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ContactPersonDto TransferToDto(this ContactPersonEntity entity)
        {
            ContactPersonDto dto = new ContactPersonDto()
            {
                ContactId = entity.ContactId,
                Id = entity.Id,
                SalutationId = entity.SalutationId,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                Function = entity.Function,
                Salutation = entity.Salutation,
                Address = entity.Address,
                HouseNumber = entity.HouseNumber,
                PostalCode = entity.PostalCode,
                City = entity.City,
                State = entity.State,
                Province = entity.Province,
                CountryId = entity.CountryId,
                Phone = entity.Phone,
                Mobile = entity.Mobile,
                Email = entity.Email,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };
            return dto;
        }

        /// <summary>
        /// Transfer from ContactPersonDto to ContactPersonEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactPersonEntity TransferToEntity(this ContactPersonDto dto)
        {
            ContactPersonEntity entity = new ContactPersonEntity()
            {
                ContactId = dto.ContactId,
                Id = dto.Id,
                SalutationId = dto.SalutationId,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Function = dto.Function,
                Salutation = dto.Salutation,
                Address = dto.Address,
                HouseNumber = dto.HouseNumber,
                PostalCode = dto.PostalCode,
                City = dto.City,
                State = dto.State,
                Province = dto.Province,
                CountryId = dto.CountryId,
                Phone = dto.Phone,
                Mobile = dto.Mobile,
                Email = dto.Email,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
