using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Prorent24.BLL.Models.General.Preset;
using Prorent24.BLL.Transfers.General;
using Prorent24.DAL.Storages.General.Filter;
using Prorent24.DAL.Storages.General.Preset;
using Prorent24.Entity.General;
using Prorent24.Enum.General;

namespace Prorent24.BLL.Services.General.Folder
{
    internal class PresetService : IPresetService
    {
        private readonly ISavedFilterStorage _savedFilterStorage;
        private readonly IPresetStorage _presetStorage;

        public PresetService(IPresetStorage presetStorage, ISavedFilterStorage savedFilterStorage)
        {
            _presetStorage = presetStorage;
            _savedFilterStorage = savedFilterStorage;
        }

        public async Task<List<PresetDto>> GetPresets(ModuleTypeEnum moduleType)
        {
            List<PresetEntity> entities = await _presetStorage.GetPresets(moduleType);
            List<PresetDto> listDtos = entities.TransferToListDto();
            return listDtos;
        }

        public async Task<PresetDto> GetPreset(int id)
        {
            if (id == -1)
            {
                return null;
            }

            PresetEntity entity = await _presetStorage.GetById(id);
            PresetDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<PresetDto> CreatePerset(PresetDto dto)
        {
            PresetEntity entity = dto.TransferToEntity();
            entity = await _presetStorage.Create(entity);
            dto = entity.TransferToDto();
            return dto;
        }

        public async Task<PresetDto> UpdatePerset(PresetDto dto)
        {
            PresetEntity entity = await _presetStorage.GetById(dto.Id);
            entity.Name = dto.Name;
            entity = await _presetStorage.Update(entity);
            dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> DeletePerset(int id)
        {
            PresetEntity preset = await _presetStorage.GetById(id);
           // await _savedFilterStorage.Delete(JsonConvert.DeserializeObject<List<int>>(preset.SelectedFilters));
            bool result = await _presetStorage.Delete(id);
            return result;
        }


    }
}
