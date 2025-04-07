using Prorent24.BLL.Models.General.Folder;
using Prorent24.Entity.General;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.General
{
    internal static class FolderTransfer
    {
        /// <summary>
        /// Transfer from List<FolderEntity> to List<FolderDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<FolderDto> TransferToListDto(this List<FolderEntity> entities)
        {
            List<FolderDto> folders = entities.Select(x => new FolderDto()
            {
                Id = x.Id,
                Name = x.Name,
                ModuleType = x.ModuleType,
                Order = x.Order,
                ParentId = x.ParentId,
                Childs = x.Childs?.ToList().TransferToListDto(),
                CreationDate = x.CreationDate,
                LastModifiedDate = x.LastModifiedDate
            }).ToList();

            return folders;
        }

        /// <summary>
        /// Transfer from FolderEntity to FolderDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static FolderDto TransferToDto(this FolderEntity entity)
        {
            FolderDto folder = new FolderDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                ModuleType = entity.ModuleType,
                Order = entity.Order,
                ParentId = entity.ParentId,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return folder;
        }

        /// <summary>
        /// Transfer from FolderDto to FolderEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static FolderEntity TransferToEntity(this FolderDto dto)
        {
            FolderEntity folder = new FolderEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                ModuleType = dto.ModuleType,
                Order = dto.Order,
                ParentId = dto.ParentId,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return folder;
        }
    }
}
