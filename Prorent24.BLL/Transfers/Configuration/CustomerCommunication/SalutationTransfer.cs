using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.CustomerCommunication;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Configuration.CustomerCommunication
{
    internal static class SalutationTransfer
    {
        /// <summary>
        /// Transfer from List<SalutationEntity> to List<SalutationDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SalutationDto> TransferToListDto(this List<SalutationEntity> entities)
        {
            List<SalutationDto> salutationDtos = entities.Select(x => new SalutationDto()
            {
                Id = x.Id,
                Name = !string.IsNullOrWhiteSpace(x.View) ? x.View : x.Name,
                DisplayView = x.View
            }).ToList();

            return salutationDtos;
        }

        /// <summary>
        /// Transfer from SalutationDto to SalutationEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SalutationDto TransferToDto(this SalutationEntity entity)
        {
            SalutationDto salutationDto = new SalutationDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                DisplayView = entity.View
            };

            return salutationDto;
        }

        /// <summary>
        /// Transfer from SalutationEntity to SalutationDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SalutationEntity TransferToEntity(this SalutationDto dto)
        {
            SalutationEntity salutationEntity = new SalutationEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                View = dto.DisplayView
            };

            return salutationEntity;
        }
    }
}
