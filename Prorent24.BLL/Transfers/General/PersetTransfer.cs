using Newtonsoft.Json;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Preset;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.General
{
    internal static class PersetTransfer
    {
        /// <summary>
        /// Transfer from List<PresetEntity> to List<PresetDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<PresetDto> TransferToListDto(this List<PresetEntity> entities)
        {
            List<PresetDto> presets = entities.Select(x => new PresetDto()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            presets.Add(new PresetDto()
            {
                Id = -1,
                Name = "Default",
                IsDefault = true
            });

            return presets;
        }

        /// <summary>
        /// Transfer from PresetDto to PresetEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PresetEntity TransferToEntity(this PresetDto dto)
        {
            PresetEntity entity = new PresetEntity()
            {
                Name = dto.Name,
                ModuleType = dto.ModuleType,
                Filters = JsonConvert.SerializeObject(dto.Filters)
            };

            return entity;
        }

        /// <summary>
        /// Transfer from PresetEntity to PresetDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PresetDto TransferToDto(this PresetEntity entity)
        {
            PresetDto dto = new PresetDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                ModuleType = entity.ModuleType,
                Filters = JsonConvert.DeserializeObject<List<SelectedFilter>>(entity.Filters)
            };

            return dto;
        }
    }
}
