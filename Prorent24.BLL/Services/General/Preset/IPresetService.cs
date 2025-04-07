using Prorent24.BLL.Models.General.Preset;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Folder
{
    public interface IPresetService
    {
        Task<List<PresetDto>> GetPresets(ModuleTypeEnum moduleType);
        Task<PresetDto> GetPreset(int id);
        Task<PresetDto> CreatePerset(PresetDto dto);
        Task<PresetDto> UpdatePerset(PresetDto dto);
        Task<bool> DeletePerset(int id);
    }
}
