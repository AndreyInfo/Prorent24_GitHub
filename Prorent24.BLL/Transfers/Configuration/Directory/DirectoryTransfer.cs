using Prorent24.BLL.Models.Directory;
using Prorent24.Entity.Directory;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Configuration.Directory
{
    public static class DirectoryTransfer
    {
        /// <summary>
        /// Transfer from List<DirectoryEntity> to List<DirectoryDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<DirectoryDto> TransferToListDto(this List<DirectoryEntity> entities)
        {
            List<DirectoryDto> directoryDtos = entities.Select(x => x.TransferToDto()).ToList();

            return directoryDtos;
        }

        /// <summary>
        /// Transfer from List<DirectoryLocEntity> to List<DirectoryLocDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<DirectoryLocDto> TransferToListDto(this List<DirectoryLocEntity> entities)
        {
            List<DirectoryLocDto> directoryDtos = entities.Select(x => x.TransferToDto()).ToList();

            return directoryDtos;
        }

        /// <summary>
        /// Transfer from DirectoryDto to DirectoryEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DirectoryDto TransferToDto(this DirectoryEntity entity)
        {
            DirectoryDto directoryDto = new DirectoryDto()
            {
                Id = entity.Id,
                Type = entity.Type,
                IsActive = entity.IsActive,
                Locs = entity.Locs != null ? entity.Locs.ToList().TransferToListDto() : null,
                Key = entity.Key
                //Name = entity.Name,
            };

            return directoryDto;
        }

        /// <summary>
        /// Transfer from DirectoryEntity to DirectoryDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static DirectoryEntity TransferToEntity(this DirectoryDto dto)
        {
            DirectoryEntity directoryEntity = new DirectoryEntity()
            {
                Id = dto.Id,
                Type = dto.Type,
                Key = dto.Key
                //Name = dto.Name,
            };

            return directoryEntity;
        }

        /// <summary>
        /// Transfer from DirectoryDto to DirectoryEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DirectoryLocDto TransferToDto(this DirectoryLocEntity entity)
        {
            DirectoryLocDto directoryLocDto = new DirectoryLocDto()
            {
                DirectoryId = entity.DirectoryId,
                Lang = entity.Lang,
                Name = entity.Name
            };

            return directoryLocDto;
        }

        /// <summary>
        /// Transfer from DirectoryEntity to DirectoryDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static DirectoryLocEntity TransferToEntity(this DirectoryLocDto dto)
        {
            DirectoryLocEntity directoryLocEntity = new DirectoryLocEntity()
            {
                DirectoryId = dto.DirectoryId,
                Lang = dto.Lang,
                Name = dto.Name
            };

            return directoryLocEntity;
        }
    }
}
