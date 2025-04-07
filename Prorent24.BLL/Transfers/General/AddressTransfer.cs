using Prorent24.BLL.Models.General.Address;
using Prorent24.Entity.Directory;
using Prorent24.Entity.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Prorent24.BLL.Transfers.General
{
    public static class AddressTransfer
    {
        /// <summary>
        /// Transfer from AddressDto to AddressEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static AddressEntity TransferToEntity(this AddressDto dto)
        {
            AddressEntity entity = new AddressEntity()
            {
                CountryId = dto.CountryId,
                City = dto.City,
                Address = dto.Address,
                AdditionalAddress = dto.AdditionalAddress,
                Region = dto.Region,
                Alt = dto.Alt,
                Long = dto.Long,
                Lat = dto.Lat,
                PostalCode = dto.PostalCode,
                Number = dto.Number
            };

            if (dto.AddressId != null)
            {
                entity.Id = dto.AddressId.Value;
            }

            return entity;
        }

        /// <summary>
        /// Transfer from AddressEntity to AddressDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static AddressDto TransferToDto(this AddressEntity entity, List<DirectoryEntity> countries = null)
        {
            AddressDto dto = new AddressDto()
            {
                AddressId = entity.Id,
                CountryId = entity.CountryId,
                City = entity.City,
                Address = entity.Address,
                AdditionalAddress = entity.AdditionalAddress,
                Region = entity.Region,
                Alt = entity.Alt,
                Long = entity.Long,
                Lat = entity.Lat,
                PostalCode = entity.PostalCode,
                Number = entity.Number,
                Country = countries?.Where(x => x.Id == entity.CountryId).FirstOrDefault()?.Locs.FirstOrDefault()?.Name
        };


            return dto;
        }

        /// <summary>
        /// Transfer from AddressEntity to AddressDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static AddressEntity TransferFromEntity(this AddressEntity target, AddressEntity source)
        {
            target.CountryId = source.CountryId;
            target.City = source.City;
            target.Address = source.Address;
            target.AdditionalAddress = source.AdditionalAddress;
            target.Region = source.Region;
            target.Alt = source.Alt;
            target.Long = source.Long;
            target.Lat = source.Lat;
            target.PostalCode = source.PostalCode;
            target.Number = source.Number;
            return target;
        }
    }
}
