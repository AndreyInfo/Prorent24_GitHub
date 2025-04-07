using Newtonsoft.Json;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.Entity.General;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.General
{
    internal static class FilterTransfer
    {
        /// <summary>
        /// Transfer from List<FilterEntity> to List<FilterDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SavedFilterDto> TransferToListDto(this List<SavedFilterEntity> entities)
        {
            List<SavedFilterDto> folders = entities.Select(x => new SavedFilterDto()
            {
                Id = x.Id,
                Text = x.Text
            }).ToList();

            return folders;
        }

        /// <summary>
        /// Transfer from FilterDto to FilterEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SavedFilterEntity TransferToEntity(this SavedFilterDto dto)
        {
            SavedFilterEntity filter = new SavedFilterEntity()
            {
                Id = dto.Id,
                ModuleType = dto.ModuleType,
                FilterGroupType = dto.FilterGroupType,
                FilterType = dto.FilterType,
                Text = dto.Text,
                Value = JsonConvert.SerializeObject(dto.Value),
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return filter;
        }

        /// <summary>
        /// Transfer from FilterEntity to FilterDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SavedFilterDto TransferToDto(this SavedFilterEntity entity)
        {
            SavedFilterDto filter = new SavedFilterDto()
            {
                Id = entity.Id,
                ModuleType = entity.ModuleType,
                FilterGroupType = entity.FilterGroupType,
                FilterType = entity.FilterType,
                Text = entity.Text,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return filter;
        }
    }
}
